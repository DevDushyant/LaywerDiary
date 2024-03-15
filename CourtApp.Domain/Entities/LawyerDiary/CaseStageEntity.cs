using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_case_stage", Schema = "ld")]
    public class CaseStageEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        [Required]
        public string CaseStage { get; set; }
    }
}