using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_court_fee", Schema = "ld")]
    public class CourtFeeEntity : AuditableEntity
    {
        public Guid FeeTypeId { get; set; }       
        public virtual CourtFeeTypeEntity FeeType { get; set; }        
        public float Value { get; set; }        
    }
}