using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_book", Schema = "ld")]
    public class LDBookEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        
        [ForeignKey("BookTypeId")]
        public virtual BookTypeEntity BookType { get; set; }
        
        [ForeignKey("PublisherId")]
        public virtual PublisherEntity Publisher { get; set; }
        public int Year { get; set; }
    }
}
