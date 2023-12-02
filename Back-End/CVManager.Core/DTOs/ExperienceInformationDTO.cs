using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManager.Core.DTOs
{
    public class ExperienceInformationDTO
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string CompanyName { get; set; }
        [StringLength(255)]
        public string? City { get; set; }

        [StringLength(1000)]
        public string? CompanyField { get; set; }
        public int CvId { get; set; }
    }

    public class ExperienceInformationDTOValidator : AbstractValidator<ExperienceInformationDTO>
    {
        public ExperienceInformationDTOValidator()
        {
            RuleFor(experience => experience.CompanyName).NotEmpty().NotNull().MaximumLength(20).WithMessage("Company name must be a text with length between 1 and 20 charachters");
        }
    }
}
