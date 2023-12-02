using FluentValidation;
using personalInfoManager.Core.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CVManager.Core.DTOs
{
    public class CvDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PersonalInformationDTO PersonalInformation { get; set; }
        public ICollection<ExperienceInformationDTO> ExperienceInformation { get; set; }
    }

    public class CvDTOValidator : AbstractValidator<CvDTO>
    {
        public CvDTOValidator()
        {
            RuleFor(cv => cv.Name).NotEmpty().NotNull().WithMessage("Name is required");

            RuleFor(cv => cv.PersonalInformation).SetValidator(new PersonalInformationDTOValidator());

            RuleForEach(cv => cv.ExperienceInformation).SetValidator(new ExperienceInformationDTOValidator());


        }
    }
}
