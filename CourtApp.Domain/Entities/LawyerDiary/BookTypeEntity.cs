using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_BookType", Schema = "LDiary")]
    public class BookTypeEntity : AuditableEntity
    {
        [Required]
        public string BookType { get; set; }
    }
}