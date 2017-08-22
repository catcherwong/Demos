using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Threading.Tasks;
using WebSite.Services;

namespace WebSite.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly IStudentService _studentService;
        public DetailsModel(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        public Student Student;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            this.Student = await _studentService.GetStudentByIdAsync(id.Value);

            return Page();
        }
    }
}