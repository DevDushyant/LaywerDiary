using AspNetCoreHero.Abstractions.Domain;
using CourtApp.Domain.Entities.Common;
using CourtApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Client", Schema = "LDiary")]
    public class ClientEntity : AuditableEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        [ForeignKey("State")]
        public string StateCode { get; set; }
        public virtual StateEntity State { get; set; }

        [ForeignKey("District")]
        public int DistrictCode { get; set; }
        public virtual DistrictEntity District { get; set; }
        public string Address { get; set; }
       
    }
}