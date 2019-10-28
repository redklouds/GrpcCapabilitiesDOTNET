using Grpc.Core;
using System;
using Common.Models.Grpc.Protos;
using System.Threading.Tasks;
using System.Threading;

namespace chatClientTest
{
    class Program
    {


        static string HOST = "localhost";
        static int PORT = 8080;

        //the protobuf generated code for the client
        public static ChatService.ChatServiceClient _chatClient = null;
        //GRPC duplex streaming
        //used for bi directional streaming calls, first type is request, and second type is response.
        public static AsyncDuplexStreamingCall<ChatMessage, ChatMessageFromServer> _call;


        //create the channel
        public static Channel channel = new Channel(HOST + ":" + PORT, ChannelCredentials.Insecure);

   



        static async Task StartListening()
        {

            try
            {
                using (_call = _chatClient.chat())
                {
                    while (await _call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var serverMessage = _call.ResponseStream.Current;
                        var otherClientMessage = serverMessage.Message;
                        var displayMessage = string.Format("{0}:{1}{2}", otherClientMessage.From, otherClientMessage.Message, Environment.NewLine);
                        Console.WriteLine(displayMessage);
                    }
                }
            }
            catch (RpcException)
            {
                Console.WriteLine("We got that RPCException YO!");
                //i believe this is done when you close connection or the server goes down, read more documentation please.
                _call = null;
                throw;
            }
        }

        public static async Task Main(string[] args)
        {

            //create a client with the channel
            _chatClient = new ChatService.ChatServiceClient(channel);
            string HOST = "localhost";
              int PORT = 8080;

            //the protobuf generated code for the client
             ChatService.ChatServiceClient _chatClient;
            //GRPC duplex streaming
            //used for bi directional streaming calls, first type is request, and second type is response.
             AsyncDuplexStreamingCall<ChatMessage, ChatMessageFromServer> _call;


            //create the channel
            Channel channel = new Channel(HOST + ":" + PORT, ChannelCredentials.Insecure);

            //create a client with the channel
            _chatClient = new ChatService.ChatServiceClient(channel);

            //InitalizeGRPC();


            Console.WriteLine("Hello World!");
            
        }
    }
}
