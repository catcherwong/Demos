using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "No of student")]
        [Required(ErrorMessage ="No is required")]
        [StringLength(maximumLength: 6, MinimumLength = 2)]
        public string No { get; set; }

        [Display(Name = "Name of student")]
        [Required(ErrorMessage = "Name is required")]        
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }

        [Display(Name = "Gender of student")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(maximumLength:6,MinimumLength =1)]
        public string Gender { get; set; }        
    }

    public class StudentCreateVM
    {
        [Display(Name = "No")]
        [Required(ErrorMessage = "No is required")]
        [StringLength(maximumLength: 6, MinimumLength = 2)]
        public string No { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(maximumLength: 6, MinimumLength = 1)]
        public string Gender { get; set; }
    }
}
