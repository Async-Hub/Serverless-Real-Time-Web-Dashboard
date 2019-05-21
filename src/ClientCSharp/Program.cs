using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ClientCSharp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Thread.Sleep(3000);

            var builder = new HubConnectionBuilder();
            builder.WithUrl("http://localhost/api/");

            var connection = builder.Build();

            connection.On<string>("ReceiveMessage", Console.WriteLine);

            connection.Closed += async (error) =>
            {
                Console.WriteLine(error);
                await connection.StartAsync();
            };

            await connection.StartAsync();
            Console.WriteLine($"SignalR connection state: {connection.State}");

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.X)
                {
                    break;
                }
            }
        }
    }
}
