using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManager.DAL.DomainModels
{
    [Table("ExperienceInformation")]
    public class ExperienceInformation
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string CompanyName { get; set; }
        [StringLength(255)]
        public string? City { get; set; }

        [StringLength(1000)]
        public string? CompanyField { get; set; }

        public int CvId { get; set; }
        public Cv Cv { get; set; }
    }
}
