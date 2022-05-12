using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI
{
    internal class Program
    {
        public class MockProduct
        {
            public int ID { get; set; }
            public string ProductName { get; set; }
        }

        static async Task ClientConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync("http://localhost:8000/rosy/");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                Thread.Sleep(5000);

            }

        }

        static readonly HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Task clientTask = ClientConnections();
            //clientTask.Wait();
            clientTask.GetAwaiter().GetResult();    


            // Server 
            /**
            MockProduct mockProduct;
            httpClient.BaseAddress = new Uri("https://localhost:8000/");
            
            while (true)
            {
                Thread.Sleep(3000);
                //HttpResponseMessage response = httpClient.GetAsync("api/Product/GetProductList").Result;  // Blocking call!
                HttpResponseMessage response = httpClient.GetAsync("api/Product/GetProductList").Result;
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



                mockProduct = new MockProduct();
                mockProduct.ID = 123;
                mockProduct.ProductName = "Cidar";
                var responsePost = httpClient.PostAsJsonAsync("api/Product/UpdateProduct", mockProduct).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Error");
                }

                Thread.Sleep(5000);

            }
            **/




        }


    }

}
