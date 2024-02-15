using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_practice_subject",Schema ="ld")]
    public class PracticeSubjectEntity : AuditableEntity
    {
        [Required]
        public string Subject { get; set; }
    }
}