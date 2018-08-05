namespace RefitBasicDemo
{
    using System;
    using Refit;
    
    class Program
    {
        static void Main(string[] args)
        {
            var personsApi = RestService.For<IPersonsApi>("http://localhost:9999");

            var persons = personsApi.GetPersonsAsync().GetAwaiter().GetResult();

            Console.WriteLine("GetPersonsAsync result:");
            foreach (var item in persons)
            {
                Console.WriteLine($"{item.Id}-{item.Name}");
            }

            var person = personsApi.GetPersonAsync(1).GetAwaiter().GetResult();
            Console.WriteLine("GetPersonAsync result:");
            Console.WriteLine($"{person.Id}-{person.Name}");


            var newPerson = new Person { Id = 999, Name = "999" };
            var postResult = personsApi.AddPersonAsync(newPerson).GetAwaiter().GetResult();
            Console.WriteLine("AddPersonAsync result:");
            Console.WriteLine($"{postResult.Id}-{postResult.Name}");


            var editResult = personsApi.EditPersonAsync(1).GetAwaiter().GetResult();
            Console.WriteLine("EditPersonAsync result:");
            Console.WriteLine($"{editResult}");

            var delResult = personsApi.DeletePersonAsync(1).GetAwaiter().GetResult();
            Console.WriteLine("DeletePersonAsync result:");
            Console.WriteLine($"{delResult}");


            Console.ReadKey();
        }
    }
}
