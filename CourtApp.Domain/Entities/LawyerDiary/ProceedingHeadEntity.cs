using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_proceeding_head", Schema = "ld")]
    public class ProceedingHeadEntity:AuditableEntity
    {
        
        public new Guid Id{ get; set; }
        [Required]
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
