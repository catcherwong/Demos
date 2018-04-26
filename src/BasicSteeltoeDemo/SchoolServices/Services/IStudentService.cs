namespace SchoolServices.Services
{
    using System.Threading.Tasks;

    public interface IStudentService
    {
        Task<string> GetStudentListAsync(string name);
    }
}
