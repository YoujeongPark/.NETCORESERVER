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
                Console.WriteLine(req.UserHostAddress);
                Console.WriteLine(req.Headers); // [::1]:8000
                Console.WriteLine(req.ServiceName); // [::1]:8000
                Console.WriteLine(req.Headers);

                Console.WriteLine("KeepAlive: {0}", req.KeepAlive);
                Console.WriteLine("Local end point: {0}", req.LocalEndPoint.ToString());
                Console.WriteLine("Remote end point: {0}", req.RemoteEndPoint.ToString());
                Console.WriteLine("Is local? {0}", req.IsLocal);
                Console.WriteLine("HTTP method: {0}", req.HttpMethod);
                Console.WriteLine("Protocol version: {0}", req.ProtocolVersion);
                Console.WriteLine("Is authenticated: {0}", req.IsAuthenticated);
                Console.WriteLine("Is secure: {0}", req.IsSecureConnection);

                // get Client IP 
                string clientIP = ctx.Request.RemoteEndPoint.ToString();
                Console.WriteLine(clientIP);

                //get User Host Address 
                Console.WriteLine(req.UserHostAddress);


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
                    case "/postTxtFile/":
                        //그대로 Txt 파일 받아오기 
                        var body2 = new StreamReader(ctx.Request.InputStream).ReadToEnd();
                        FileStream fileOut = new FileStream("d:\\file.txt", FileMode.Create, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fileOut);
                        sw.Write(body2);
                        sw.Close();
                        fileOut.Close();

                        await resp.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("Success"), 0, Encoding.UTF8.GetBytes("Success").Length);
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
