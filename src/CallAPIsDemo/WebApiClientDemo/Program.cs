namespace WebApiClientDemo
{
    using System;
    using WebApiClient;

    class Program
    {
        static void Main(string[] args)
        {            
            var config = new HttpApiConfig
            {                
                HttpHost = new Uri("http://localhost:9999"),
            };
            using(var client = HttpApiClient.Create<IPersonApiClient>(config))
            {
                var persons = client.GetPersonsAsync().GetAwaiter().GetResult();

                Console.WriteLine("GetPersonsAsync result:");
                foreach (var item in persons)
                {
                    Console.WriteLine($"{item.Id}-{item.Name}");
                }

                var person = client.GetPersonAsync(1000).GetAwaiter().GetResult();
                Console.WriteLine("GetPersonAsync result:");
                Console.WriteLine($"{person.Id}-{person.Name}");


                var newPerson = new Person { Id = 999, Name = "999" };
                var postResult = client.AddPersonAsync(newPerson).GetAwaiter().GetResult();
                Console.WriteLine("AddPersonAsync result:");
                Console.WriteLine($"{postResult.Id}-{postResult.Name}");


                var editResult = client.EditPersonAsync(1).GetAwaiter().GetResult();
                Console.WriteLine("EditPersonAsync result:");
                Console.WriteLine($"{editResult}");

                var delResult = client.DeletePersonAsync(1).GetAwaiter().GetResult();
                Console.WriteLine("DeletePersonAsync result:");
                Console.WriteLine($"{delResult}");

                var retry = client.RetryTestAsync().Retry(3).WhenCatch<Exception>().GetAwaiter().GetResult();
                Console.WriteLine("RetryTestAsync result:");
                Console.WriteLine($"{retry}");
            }

            Console.ReadKey();
        }
    }
}
