using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManager.DAL.DomainModels
{
    [Table("Cvs")]
    public class Cv
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [ForeignKey(nameof(PersonalInformation))]
        public int PersonalInformationId { get; set; }

        public PersonalInformation PersonalInformation { get; set; }

        public ICollection<ExperienceInformation> ExperienceInformation { get; set; }
    }
}
