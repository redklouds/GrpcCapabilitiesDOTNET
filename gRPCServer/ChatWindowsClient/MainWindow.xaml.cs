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
        private bool loggedIn = false;
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

        

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            
            var message = new ChatMessage
            {
                From = UserNameTxtBox.Text,
                Message = MessageBox.Text
            };

            if (_call != null)
            { 
                await _call.RequestStream.WriteAsync(message);
            }
        }

        private async Task StartClientChatService()
        {
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

                //throw;
            }
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //SendMessage.IsEnabled = loggedIn;
            // Open a connection to the server
            //await StartClientChatService();
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
            catch (RpcException rpcEx)
            {
                _call = null;

                //throw;
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if(_call != null && UserNameTxtBox.Text != string.Empty)
            {
                RequestStatus result =  await _chatClient.AddUserAsync(new User
                {
                    Name = UserNameTxtBox.Text
                });

                if(result.Status is STATUS.Ok)
                {
                    LoginButton.Content = "Log Out";
                }
                else
                {
                    chatMessages.AppendText("\nError signing in with the respective userName");
                }
            }
          
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            if(UserNameTxtBox.Text != string.Empty)
            {
                //enable the send message button if there is a Username!
                SendMessage.IsEnabled = true;
            }
            else
            {
                //grey out the sendMessage button if there is no UserName 
                SendMessage.IsEnabled = true;
            }
            */
        }
    }
}
