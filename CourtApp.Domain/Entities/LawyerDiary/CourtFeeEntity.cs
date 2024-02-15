using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_court_fee", Schema = "ld")]
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