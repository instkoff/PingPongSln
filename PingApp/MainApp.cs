using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PingApp.Models.Settings;
using PingApp.Utils;
using PingPong.Shared.Models.Requests;

namespace PingApp
{
    public class MainApp
    {
        private readonly PongAppClient _pongAppClient;
        private readonly AppSettings _settings;

        public MainApp(PongAppClient pongAppClient, IOptions<AppSettings> settings)
        {
            _pongAppClient = pongAppClient;
            _settings = settings.Value;
        }

        public async Task Execute()
        {
            while (true)
            {
                PrintUsage();

                var keyInfo = Console.ReadKey(true);
                Console.WriteLine("\n");

                if (keyInfo.Key == ConsoleKey.Q) break;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.S:
                        await GetApiStatus();
                        break;
                    case ConsoleKey.A:
                        await AddMessage();
                        break;
                    case ConsoleKey.L:
                        await ListMessages();
                        break;
                    case ConsoleKey.D:
                        await DeleteMessage();
                        break;
                }
            }
        }

        private void PrintUsage()
        {
            Console.WriteLine("\nPress one of the following keys:");
            Console.WriteLine("\ts - Pong service status");
            Console.WriteLine("\ta - Add message");
            Console.WriteLine("\tl - List messages");
            Console.WriteLine("\td - Delete message");
            Console.WriteLine("\tq - Quit");
            Console.Write(":");
        }

        private async Task GetApiStatus()
        {
            try
            {
                var response = await _pongAppClient.GetServiceStatus();
                Console.WriteLine($"Overall status: {response?.Status}");
                if (response?.Entries != null)
                    foreach (var responseEntry in response?.Entries)
                    {
                        Console.WriteLine($"\t {responseEntry.Key}:");
                        Console.WriteLine($"\t Status: {responseEntry.Value.Status}");
                        Console.WriteLine($"\t Description: {responseEntry.Value.Description}");
                    }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"{e.StatusCode} - {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task AddMessage()
        {
            try
            {
                Console.WriteLine("Enter the message:");
                var messageText = Console.ReadLine();
                var request = new AddMessageRequest
                {
                    Message = messageText,
                    User = _settings.Username
                };
                var response = await _pongAppClient.AddMessageCommand(request);
                Console.WriteLine($"Status: {response.Status}");
                Console.WriteLine($"Message Id: {response.Id}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"{e.StatusCode} - {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task ListMessages()
        {
            try
            {
                Console.WriteLine("Enter message Id (Guid) or press Enter for print all your messages:");
                bool parseResult;
                var messageGuid = Guid.Empty;

                do
                {
                    var messageId = Console.ReadLine();
                    if (string.IsNullOrEmpty(messageId)) break;
                    parseResult = Guid.TryParse(messageId, out messageGuid);
                    if (!parseResult) Console.WriteLine("Guid parsing error! Try again or press Enter.");
                } while (!parseResult);

                var request = new GetMessageRequest
                {
                    MessageId = messageGuid,
                    User = _settings.Username
                };

                var response = await _pongAppClient.ListCommand(request);

                Console.WriteLine($"Status: {response.Status}");

                if (!response.Messages.Any())
                {
                    Console.WriteLine("Messages not found!");
                    return;
                }

                foreach (var message in response.Messages)
                {
                    Console.WriteLine($"Message Id: {message.Id}");
                    Console.WriteLine($"Message text: {message.MessageText}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"{e.StatusCode} - {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task DeleteMessage()
        {
            try
            {
                Console.WriteLine("Enter message Id:");
                var idStr = Console.ReadLine();
                if (string.IsNullOrEmpty(idStr))
                {
                    Console.WriteLine("Message Id cannot be empty!.");
                    return;
                }

                var request = new GetMessageRequest
                {
                    MessageId = Guid.Parse(idStr),
                    User = _settings.Username
                };

                var response = await _pongAppClient.DeleteMessageCommand(request);
                Console.WriteLine($"Status: {response.Status}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"{e.StatusCode} - {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}