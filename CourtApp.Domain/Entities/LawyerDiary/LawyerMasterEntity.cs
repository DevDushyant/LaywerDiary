using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_lawyer", Schema = "ld")]
    public class LawyerMasterEntity : AuditableEntity
    {
        [Key]
        public new Guid Id { get; set; }
        [Required]
        public string name { get; set; }
        public int type { get; set; }
    }
}
