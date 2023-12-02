using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace personalInfoManager.Core.DTOs
{
    public class PersonalInformationDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string? CityName { get; set; }

        public string? Email { get; set; }

        public string? MobileNumber { get; set; }
    }

    public class PersonalInformationDTOValidator : AbstractValidator<PersonalInformationDTO>
    {
        public PersonalInformationDTOValidator()
        {
            RuleFor(personalInfo => personalInfo.FullName).NotEmpty().NotNull().WithMessage("Fullname is required");

            RuleFor(personalInfo => personalInfo.Email).EmailAddress().WithMessage("Invalid email address");

            RuleFor(personalInfo => personalInfo.MobileNumber).NotEmpty().NotNull().Matches("^[0-9]*$").WithMessage("Mobile number must contain numbers only");
        }
    }
}
