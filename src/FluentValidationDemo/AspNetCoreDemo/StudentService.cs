using System.Linq;
using FluentValidation;

namespace AspNetCoreDemo
{
    public class StudentService : IStudentService
    {
        private readonly AbstractValidator<QueryStudentHobbiesDto> _validator;

        public StudentService(AbstractValidator<QueryStudentHobbiesDto> validator)
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
