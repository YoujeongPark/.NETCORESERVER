using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPI
{
    internal class Program
    {
        public class MockProduct
        {
            public int ID { get; set; }
            public string ProductName { get; set; }
        }

        static readonly HttpClient httpClient = new HttpClient();

        static async Task ClientConnections()
        {
            bool runServer = true;
            HttpResponseMessage response;
            string resourcePath;
            string responseBody;

            while (runServer)
            {
                Console.WriteLine("해당 작업을 선택하시오");
                Console.WriteLine("1. 현재 시간 받아오기 " +
                                  "2. 파일 목록 json으로 보내기 " +
                                  "3. Txt 파일 전송해서 Server에 자동으로 다운로드 ");
                string inputNumber = Console.ReadLine();


                switch (inputNumber)
                {
                    case "1":
                        resourcePath = "reqestDate/";
                        response = await httpClient.GetAsync("http://localhost:8000/" + resourcePath);
                        response.EnsureSuccessStatusCode(); // Exception을 주기 때문에 필수 

                        responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                        break;
                    case "2":
                        Console.WriteLine("폴더 위치를 입력 하시오. ");
                        string folderAddress = Console.ReadLine();
                        System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(folderAddress);
                        JObject jobject = new JObject();
                        jobject.Add("Folder", directoryInfo.Name);

                        JArray jArray = new JArray(); 
                        foreach(System.IO.FileInfo File in directoryInfo.GetFiles()) { 
                            jArray.Add(File.FullName); 
                        }
                        jobject.Add("Files", jArray);
                        Console.WriteLine(jobject);

                        // Json to HttpContent
                        var stringPayload = JsonConvert.SerializeObject(jobject);
                        var jsonHttpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        //Connection 
                        resourcePath = "postFolderFiles/";
                        response = await httpClient.PostAsync("http://localhost:8000/" + resourcePath, jsonHttpContent);
                        response.EnsureSuccessStatusCode(); // Exception을 주기 때문에 필수 

                        responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);

                        break;
                    case "3":
                        Console.WriteLine("3번을 선택하셨습니다.");
                        break;
                    default:
                        Console.WriteLine("다시 재 선택 하시오. ");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine();



                //try
                //{
                //    HttpResponseMessage response = await httpClient.GetAsync("http://localhost:8000/rosy/");
                //    response.EnsureSuccessStatusCode();
                //    string responseBody = await response.Content.ReadAsStringAsync();
                //    // Above three lines can be replaced with new helper method below
                //    // string responseBody = await client.GetStringAsync(uri);

                //    Console.WriteLine(responseBody);
                //}
                //catch (HttpRequestException e)
                //{
                //    Console.WriteLine("\nException Caught!");
                //    Console.WriteLine("Message :{0} ", e.Message);
                //}

                //Thread.Sleep(5000);

            }

        }

      

        static void Main(string[] args)
        {
            // Console Server Connection 
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
