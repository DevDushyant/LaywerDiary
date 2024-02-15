using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_publisher",Schema ="ld")]
    public class PublisherEntity : AuditableEntity
    {
        [Required]
        public string PublicationName { get; set; }        
        public string PropriatorName { get; set; }
    }
}