using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebSite.Services
{
    public class StudentService : IStudentService
    {
        private HttpClient _client;

        public StudentService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:59309/api/students");
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IEnumerable<Student>> GetStudentListAsync()
        {
            var resStr = await _client.GetStringAsync("");

            return JsonConvert.DeserializeObject<IEnumerable<Student>>(resStr);
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var url = $"http://localhost:59309/api/students/{id}";

            var resStr = await _client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<Student>(resStr);
        }

        public async Task<bool> AddStudentAsync(StudentCreateVM student)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("No", student.No),
                new KeyValuePair<string, string>("Name", student.Name),
                new KeyValuePair<string, string>("Gender", student.Gender),
            };

            var content = new FormUrlEncodedContent(pairs);

            var res = await _client.PostAsync("", content);

            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Id", student.Id.ToString()),
                new KeyValuePair<string, string>("No", student.No),
                new KeyValuePair<string, string>("Name", student.Name),
                new KeyValuePair<string, string>("Gender", student.Gender),
            };

            var content = new FormUrlEncodedContent(pairs);

            var res = await _client.PutAsync("", content);

            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStudentByIdAsync(int id)
        {
            var url = $"http://localhost:59309/api/students/{id}";
            var res = await _client.DeleteAsync(url);

            return res.IsSuccessStatusCode;
        }

        ~ StudentService()
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }

    }
}
