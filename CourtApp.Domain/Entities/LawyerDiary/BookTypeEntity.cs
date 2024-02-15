using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_book_type", Schema = "ld")]
    public class BookTypeEntity : AuditableEntity
    {
        [Required]
        public string BookType { get; set; }
    }
}