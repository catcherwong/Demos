namespace AspNetCoreDemo
{
    using System.Linq;
    using FluentValidation;
    
    public class StudentService : IStudentService
    {
        private readonly AbstractValidator<QueryStudentHobbiesDto> _validator;
        //private readonly IValidator<QueryStudentHobbiesDto> _validator;

        public StudentService(AbstractValidator<QueryStudentHobbiesDto> validator)
        //public StudentService(IValidator<QueryStudentHobbiesDto> validator)
        {
            this._validator = validator;
        }

        public (bool flag, string msg) QueryHobbies(QueryStudentHobbiesDto dto)
        {
            var res = _validator.Validate(dto, ruleSet: "all");

            if(!res.IsValid)
            {
                return (false, res.Errors.FirstOrDefault().ErrorMessage);
            }
            else
            {
                return (true, string.Empty);
            }
        }
    }


    public interface IStudentService
    {
        (bool flag, string msg) QueryHobbies(QueryStudentHobbiesDto dto);
    }
}
