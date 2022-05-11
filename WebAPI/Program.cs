using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace WebAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {   

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7130/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            while (true)
            {
                HttpResponseMessage response = httpClient.GetAsync("api/Product/GetProductList").Result;  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    
                    Console.WriteLine("Request Message Information:- \n\n" + response.RequestMessage + "\n");
                    Console.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
                Thread.Sleep(2000);
            }


        }
    }
}
