using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_book", Schema = "ld")]
    public class LDBookEntity : AuditableEntity
    {
        [Required]
        [ForeignKey("BookType")]
        public int BookTypeId { get; set; }
        public virtual BookTypeEntity BookType { get; set; }


        [Required]
        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }
        public virtual PublisherEntity Publisher { get; set; }

        public int Year { get; set; }
    }
}
