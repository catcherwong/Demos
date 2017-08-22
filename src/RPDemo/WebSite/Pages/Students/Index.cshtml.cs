using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSite.Services;

namespace WebSite.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;
        public IndexModel(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        public IEnumerable<Student> StudentList;


        public async Task OnGetAsync()
        {
            var _studentList = await _studentService.GetStudentListAsync();
            this.StudentList = _studentList;
        }
    }
}