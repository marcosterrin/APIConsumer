using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //HTTPGET
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://jsonplaceholder.typicode.com") };
            var response = await client.GetAsync("users");
            var content = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<Users[]>(content);

            foreach (var item in users)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Email);
            }


            //HTTPPOST
            var newPost = new Post()
            {
                Title = "Test post",
                Body = "Hello World",
                UserId = 44
            };
            var newPostJson = JsonConvert.SerializeObject(newPost);
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            
            var responsePost = client.PostAsync("posts", payload).Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine();
            Console.WriteLine("HTTP Post response");
            Console.WriteLine(responsePost);
        }
    }
}
