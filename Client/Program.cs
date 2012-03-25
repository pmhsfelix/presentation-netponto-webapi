using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var res = 
                    client.GetAsync("https://api.github.com/users/pmhsfelix/repos")
                    .Result;
                var content = res.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);

                var json = res.Content.ReadAsAsync<JsonValue>().Result.AsDynamic();
                foreach (var repo in json)
                {
                    Console.WriteLine(repo.git_url.ReadAs<string>());
                }
            }
        }
    }
}
