using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_case_title", Schema = "ld")]
    public class CaseTitleEntity : AuditableEntity
    {
        [Key]
        public new Guid Id { get; set; }
        public int TypeId { get; set; }
        public required string Title { get; set; }

        [ForeignKey("CaseId")]
        public virtual CaseEntity Case { get; set; }
    }
}
