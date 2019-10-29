using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Grpc.Protos;
using Grpc.Core;

namespace ChatServer
{
    public class ChatServiceImplementation : ChatService.ChatServiceBase
        //name of the base class will always be the service name
    {
        //the HashSet holds the "writable stream of messages that is used in server-side handlers" perhaps this means that the hashset is holding,
        //some 'database' of messages that come in as a stream?, more reseach needs to be done.
        //we can also think of the below line as a collection of the 'handles' or streams for EACH CLIENT connected, however we need a way to remove the client if they leave?
        public static HashSet<IServerStreamWriter<ChatMessageFromServer>> responseStreams = new HashSet<IServerStreamWriter<ChatMessageFromServer>>();


        public static Dictionary<string, IServerStreamWriter<ChatMessageFromServer>> SessionUserStreams = new Dictionary<string, IServerStreamWriter<ChatMessageFromServer>>();
        public override  Task<RequestStatus> AddUser(User user, ServerCallContext context)
        {
            if (SessionUserStreams.ContainsKey(user.Name))
            {
                return Task.FromResult(new RequestStatus
                {
                    Status = STATUS.Bad
                });
            }

            //SessionUserStreams.Add()
            return Task.FromResult(new RequestStatus
            {
                Status = STATUS.Ok
            }
            );
        }


        private void addUserHelper()
        {

        }

        //time to override the service method of the base class and implement the implementation for that signature
        public override async Task chat(
            IAsyncStreamReader<ChatMessage> requestStream, //this is a 'handle' allows asyncchronus reading from channels
            //IAsyncStreamReader implements a MoveNExt() token that returns false if no more messages IE CLOSED connection.
            //
            IServerStreamWriter<ChatMessageFromServer> responseStream, //this is a 'handle' that allows me to write to communicate
            ServerCallContext context)
        {
            //The purpose that the server is have an IAsyncStreamReader of ChatMessage is that the server acts as a middle hub that clients will
            //send ChatMessages to, and it will then broadcast those messages to all registered clients on its registration list.

            //this will add a client to the response streams or clients here
            
            responseStreams.Add(responseStream);

            //we can think of the StreamReader as a array of bytes (Messagges) or linked list that we are reading from
            //however maybe the hashset is made to be efficent handling of unique messages?
            while(await requestStream.MoveNext(CancellationToken.None))
            {

                //get the client Message from the request Stream
                ChatMessage messageFromClient = requestStream.Current;

                //lets create a message to broadcast to our clients
                ChatMessageFromServer message = new ChatMessageFromServer
                {
                    Message = messageFromClient,
                };

                //at this point this means that the clients will have to send us the chat history?

                //we are throwing the messages that we get back to ALL CLIENTS connected

                //send the message
                foreach(IServerStreamWriter<ChatMessageFromServer> stream in responseStreams)
                {
                    //OH! so the responseStreams, hashset, is holding INDIVIDUAL STREAMS PER CLIENT, each client has its own stream, this could result
                    //in slower responses where the first client gets the first message and so fourth if there are many client streams?
                    
                    //write async to each stream(client)
                    await stream.WriteAsync(message);

                }
            

            
            }

        }
    }
 
}
