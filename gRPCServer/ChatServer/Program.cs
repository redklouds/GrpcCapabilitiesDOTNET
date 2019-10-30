using Common.Models.Grpc.Protos;
using Grpc.Core;
using System;

namespace ChatServer
{
    class Program
    {

        const string HOST = "localhost";

        const int PORT = 8080;

        static void Main(string[] args)
        {

            //build server

            Server server = new Server
            {
                Services =
                {
                    ChatService.BindService(new ChatServiceImplementation())
                },
                Ports =
                {
                    new ServerPort(HOST, PORT, ServerCredentials.Insecure)
                }
            };

            //start the server

            server.Start();

            Console.WriteLine("Starting the server...");

            Console.WriteLine("Press any key to quit the server");

            Console.ReadLine();

            //gracfully shutdown
            server.ShutdownAsync().Wait();

            Console.WriteLine("Hello World!");
        }
    }
}
