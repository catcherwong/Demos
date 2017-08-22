using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Threading.Tasks;
using WebSite.Services;

namespace WebSite.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly IStudentService _studentService;
        public EditModel(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.Student = await _studentService.GetStudentByIdAsync(id.Value);

            if (this.Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var res = await _studentService.UpdateStudentAsync(this.Student);

            if (res)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }
    }
}