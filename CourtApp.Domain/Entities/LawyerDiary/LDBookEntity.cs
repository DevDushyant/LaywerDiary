using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_Book", Schema = "LDiary")]
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
