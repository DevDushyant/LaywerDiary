using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_case_kind", Schema = "ld")]
    public class CaseKindEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        [Required]
        public string CaseKind { get; set; }

        [Required]
        [ForeignKey("CourtTypeId")]
        public virtual CourtTypeEntity CourtType { get; set; }
    }
}