using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_court_fee", Schema = "ld")]
    public class CourtFeeEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        [Required]
        [ForeignKey("CourtFeeTypeId")]
        public virtual CourtFeeTypeEntity CourtFeeType { get; set; }
        [Required]
        public float Value { get; set; }        
    }
}