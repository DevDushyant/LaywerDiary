using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("case_titles", Schema = "ld")]
    public class CaseTitleEntity : AuditableEntity
    {
        [Key]
        public new Guid Id { get; set; }
        public int TypeId { get; set; }
        public Guid CaseId { get; set; }
        public required string Title { get; set; } 
        public virtual CaseDetailEntity Case { get; set; }
    }
}
