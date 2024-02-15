using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_court_fee_type", Schema = "ld")]
    public class CourtFeeTypeEntity : AuditableEntity
    {
        [Required]
        public string CourtFeeType { get; set; }
    }
}
