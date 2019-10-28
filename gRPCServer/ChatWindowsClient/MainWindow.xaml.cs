using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common.Models.Grpc.Protos;
using Grpc.Core;

namespace ChatWindowsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string HOST = "localhost";
        private const int PORT = 8080;

        //the protobuf generated code for the client
        private ChatService.ChatServiceClient _chatClient;
        //GRPC duplex streaming
        //used for bi directional streaming calls, first type is request, and second type is response.
        private AsyncDuplexStreamingCall<ChatMessage, ChatMessageFromServer> _call;



        public MainWindow()
        {
            InitializeComponent();
            InitalizeGRPC();
        }

        public bool InitalizeGRPC()
        {
            //create the channel
            Channel channel = new Channel(HOST + ":" + PORT, ChannelCredentials.Insecure);

            //create a client with the channel
            _chatClient = new ChatService.ChatServiceClient(channel);

            return true;
        }

        private async void MainWindow_Loaded(object sender, EventArgs e)
        {
            // Open a connection to the server
            try
            {
                using (_call = _chatClient.chat())
                {
                    // Read messages from the response stream
                    while (await _call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var serverMessage = _call.ResponseStream.Current;
                        var otherClientMessage = serverMessage.Message;
                        var displayMessage = string.Format("{0}:{1}{2}", otherClientMessage.From, otherClientMessage.Message, Environment.NewLine);
                        chatMessages.Text += displayMessage;
                    }
                    // Format and display the message
                }
            }
            catch (RpcException)
            {
                _call = null;
                throw;
            }
        }


        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var message = new ChatMessage
            {
                From = textBox.Text,
                Message = MessageBox.Text
            };

            if (_call != null)
            {
                await _call.RequestStream.WriteAsync(message);
            }
        }



        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Open a connection to the server
            try
            {
                using (_call = _chatClient.chat())
                {
                    // Read messages from the response stream
                    while (await _call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var serverMessage = _call.ResponseStream.Current;
                        var otherClientMessage = serverMessage.Message;
                        var displayMessage = string.Format("{0}:{1}{2}", otherClientMessage.From, otherClientMessage.Message, Environment.NewLine);
                        chatMessages.Text += displayMessage;
                    }
                    // Format and display the message
                }
            }
            catch (RpcException)
            {
                _call = null;
                throw;
            }
        }
    }
}
