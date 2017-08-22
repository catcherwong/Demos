using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Threading.Tasks;
using WebSite.Services;

namespace WebSite.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly IStudentService _studentService;
        public CreateModel(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [BindProperty]
        public StudentCreateVM Student { get; set; }

        public void OnGet()
        {            
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var res = await _studentService.AddStudentAsync(this.Student);

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