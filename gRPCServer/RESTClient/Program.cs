
using Common.Models;
using Common.Models.Grpc.Protos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RESTClient
{
    class Program
    {
        public static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {

            await MakeCall();
            Console.WriteLine("Hello World!");
        }

        public static async Task MakeCall()
        {

            List<TimeSpan> timeSpanList = new List<TimeSpan>();
            //TimeSpan totalTime = new TimeSpan();

           //while(Console.ReadKey().Key == ConsoleKey.Y)
            for(int i = 0; i < 20000; i++)
            {
                System.Diagnostics.Stopwatch timer = new Stopwatch();
                timer.Start();
           
            
            

                DataRequest dr = new DataRequest
                {
                    MemberID = "69696"
                };
                var data = JsonConvert.SerializeObject(dr);
       
                var buffer = System.Text.Encoding.UTF8.GetBytes(data);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("Application/json");
                var response = await client.PostAsync("http://localhost:5000/api/MemberData/GetMemberData", byteContent);

                var ddr = await response.Content.ReadAsStringAsync();
                DataResponse ds = JsonConvert.DeserializeObject<DataResponse>(ddr);
                Member _member = JsonConvert.DeserializeObject<Member>(ds.ResponseBody);
                //List<Case> cases = JsonConvert.DeserializeObject<List<Case>>(_member.Cases);

                Console.WriteLine(_member);


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
