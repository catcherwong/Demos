using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;

namespace BasicConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var addCusDto = new AddCustomerDto();

            AddCustomer(addCusDto);

            AddCustomerWithFluentValidation(addCusDto);

            var addCusDto2 = new AddCustomerDto2
            {
                Address = new Address(),
                Hobbies = new List<string> { "", "coding" }
            };

            ListAndIncludeValidation(addCusDto2);

            Console.ReadKey();
        }

        static void AddCustomer(AddCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                Console.WriteLine($"name cannot be null");
                return;
            }

            if (string.IsNullOrWhiteSpace(dto.Phone))
            {
                Console.WriteLine($"phone cannot be null");
                return;
            }

            if (dto.Other <= 0)
            {
                Console.WriteLine($"other must great than 0");
                return;
            }
            //save code ...
        }

        static void AddCustomerWithFluentValidation(AddCustomerDto dto)
        {
            var validator = new AddCustomerDtoValidator();
            var validRes = validator.Validate(dto);
            if (!validRes.IsValid)
            {
                //first error message
                Console.WriteLine(validRes.Errors.FirstOrDefault());
                ////all error messages
                //Console.WriteLine(validRes.ToString(","));
                //Console.WriteLine(string.Join(",", validRes.Errors.Select(x => x.ErrorMessage)));
                //Console.WriteLine(string.Join(",", validRes.Errors));
            }

            //save code ...
        }


        static void ListAndIncludeValidation(AddCustomerDto2 dto)
        {
            var validator = new AddCustomerDto2Validator();
            var validRes = validator.Validate(dto);
            if (!validRes.IsValid)
            {
                //first error message
                Console.WriteLine(validRes.Errors.FirstOrDefault());
                ////all error messages
                //Console.WriteLine(validRes.ToString(","));
                //Console.WriteLine(string.Join(",", validRes.Errors.Select(x => x.ErrorMessage)));
                //Console.WriteLine(string.Join(",", validRes.Errors));
            }

            //save code ...
        }
    }

    public class AddCustomerDto
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public int Other { get; set; }
    }


    public class AddCustomerDtoValidator : AbstractValidator<AddCustomerDto>
    {
        public AddCustomerDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotEqual("catcher").WithMessage("name cannot be null");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("phone cannot be null");
            RuleFor(x => x.Other).GreaterThan(0).WithMessage("other must great than 0");
        }
    }

    public class AddCustomerDto2 : AddCustomerDto
    {
        public int UserType { get; set; }

        public List<string> Hobbies { get; set; }

        public Address Address { get; set; }
    }

    public class AddCustomerDto2Validator : AbstractValidator<AddCustomerDto2>
    {
        public AddCustomerDto2Validator()
        {
            Include(new AddCustomerDtoValidator());
            RuleFor(x => x.UserType).NotEqual(1).WithMessage("usertype cannot be 1");
            RuleForEach(x => x.Hobbies).NotEmpty().WithMessage("hobbies cannot contain empty value");
            RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator());
        }
    }

    public class Address
    {
        public string County { get; set; }
        public string Postcode { get; set; }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Postcode).NotNull();
        }
    }
}
