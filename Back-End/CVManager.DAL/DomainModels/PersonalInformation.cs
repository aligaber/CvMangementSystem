using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManager.DAL.DomainModels
{
    [Table("PersonalInformation")]
    public class PersonalInformation
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string? CityName { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? MobileNumber { get; set; }
        public Cv Cv { get; set; }
    }
}
