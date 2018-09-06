namespace WebApiClientApi
{
    using System.Collections.Generic;
    using WebApiClient;
    using WebApiClient.Attributes;

    public interface IPersonApiClient : IHttpApiClient
    {
        [HttpGet("/api/persons")]
        ITask<List<Person>> GetPersonsAsync();

        [HttpGet("/api/persons/{id}")]
        ITask<Person> GetPersonAsync(int id);

        [HttpPost("/api/persons")]
        ITask<Person> AddPersonAsync([JsonContent]Person person);

        [HttpPut("/api/persons")]
        ITask<string> EditPersonAsync([JsonContent]int id);

        [HttpDelete("/api/persons/{id}")]
        ITask<string> DeletePersonAsync(int id);

        [HttpGet("/api/persons/tw")]
        ITask<string> RetryTestAsync();
    }

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
