using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PingApp.Utils;
using PingPong.Shared.Models.Responses;

namespace PingApp
{
    public class MainApp
    {
        private readonly PongAppClient _pongAppClient;

        public MainApp(PongAppClient pongAppClient)
        {
            _pongAppClient = pongAppClient;
        }

        public void Execute()
        {
            while (true)
            {
                PrintUsage();

                var keyInfo = Console.ReadKey(true);
                Console.WriteLine("\n");

                if (keyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }
                switch (keyInfo.Key)
                {
                    case ConsoleKey.S:
                        GetApiStatus();
                        break;
                    case ConsoleKey.A:
                        AddMessage();
                        break;
                    case ConsoleKey.L:
                        ListMessages();
                        break;
                    case ConsoleKey.D:
                        DeleteMessafe();
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
            var result = await _pongAppClient.GetServiceStatus();
            if (result.Status == 0)
            {
                var errorResponse = result as ErrorResponse;
                Console.WriteLine("Get api status error.");
                Console.WriteLine(errorResponse?.ErrorMessage);
            }

            var 
            Console.WriteLine(result.Status);

        }
    }
}
