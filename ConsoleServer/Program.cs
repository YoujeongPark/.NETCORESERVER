using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ConsoleServer
{
    internal class Program
    {
        public static HttpListener listener;
        //public static string url = "http://localhost:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;

        static void Main(string[] args)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8000/");
            //listener.Prefixes.Add("http://localhost:8000/rosy/");
            listener.Start();
            Console.WriteLine("Listening for connections");

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();
                HttpListenerRequest req = ctx.Request; // 요청 
                HttpListenerResponse resp = ctx.Response; // 나의 응답 

                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine(req.Url.AbsoluteUri);
                Console.WriteLine(req.Url.Port);
                Console.WriteLine(req.Url.AbsolutePath);
                Console.WriteLine();

                string absoultePath = req.Url.AbsolutePath;
                switch (absoultePath)
                {
                    case "/reqestDate/":
                        var json = new JObject();
                        json.Add("CurrentTime", DateTime.Now.ToString());
                        byte[] data = Encoding.UTF8.GetBytes(json.ToString()); //현재시간 응답
                        await resp.OutputStream.WriteAsync(data, 0, data.Length);
                        break;
                    case "/postFolderFiles/":
                        var body = new StreamReader(ctx.Request.InputStream).ReadToEnd();
                        Console.WriteLine(body);

                        // 성공 Signal 보내기 
                        byte[] data2 = Encoding.UTF8.GetBytes("Success");
                        ctx.Response.StatusCode = 200;
                        ctx.Response.KeepAlive = false;
                        ctx.Response.ContentLength64 = data2.Length;
                        await resp.OutputStream.WriteAsync(data2, 0, data2.Length);
                        break; 
                    default:
                        Console.WriteLine("다시 재 선택 하시오. ");
                        break;


                }

                
                resp.Close();

            }
        }
    }
}
