using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_CourtFeeType", Schema = "LDiary")]
    public class CourtFeeTypeEntity : AuditableEntity
    {
        [Required]
        public string CourtFeeType { get; set; }
    }
}
