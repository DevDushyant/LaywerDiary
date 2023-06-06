using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_CourtFee", Schema = "LDiary")]
    public class CourtFeeEntity : AuditableEntity
    {
        [Required]
        [ForeignKey("CourtFeeType")]
        public int CourtFeeTypeId { get; set; }
        public virtual CourtFeeTypeEntity CourtFeeType { get; set; }
        [Required]
        public float Value { get; set; }        
    }
}