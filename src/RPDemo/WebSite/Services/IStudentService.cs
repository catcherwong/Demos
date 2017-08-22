using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebSite.Services
{
    public interface IStudentService
    {        
        Task<IEnumerable<Student>> GetStudentListAsync();

        Task<Student> GetStudentByIdAsync(int id);

        Task<bool> AddStudentAsync(StudentCreateVM student);

        Task<bool> UpdateStudentAsync(Student student);

        Task<bool> DeleteStudentByIdAsync(int id);
    }
}
