using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;


namespace WebAPI
{
    internal class Program
    {
        public class MockProduct
        {
            public int ID { get; set; }
            public string ProductName { get; set; }
        }

        static void Main(string[] args)
        {
            MockProduct mockProduct;


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




        }


    }

}
