
using Common.Models.Grpc.Protos;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace gRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
          
           
            await MakeCall();
            
        }


        public static async Task MakeCall()
        {

            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new MemberData.MemberDataClient(channel);


            TimeSpan totalTime = new TimeSpan();
            List<TimeSpan> timeSpanList = new List<TimeSpan>();
            //while(Console.ReadKey().Key == ConsoleKey.Y)
            for (int i = 0; i < 20000; i++)
            {
                System.Diagnostics.Stopwatch timer = new Stopwatch();
                timer.Start();


                var response = await client.GetMemberDataAsync(
                new GetMemberRequest
                {
                    MemberId = "696969"
                });
                Console.WriteLine($"The Response is{response.Member}");


                timer.Stop();
                TimeSpan timeTaken = timer.Elapsed;

                timeSpanList.Add(timeTaken);
            }
            double doubleAverageTicks = timeSpanList.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);


            Console.WriteLine($"Average Time: {new TimeSpan(longAverageTicks)}");
            Console.ReadKey();

        }
    }
}
