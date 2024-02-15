using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_case_kind", Schema = "ld")]
    public class CaseKindEntity : AuditableEntity
    {
        [Required]
        public string CaseKind { get; set; }

        [Required]
        [ForeignKey("CourtType")]
        public int CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
    }
}