using AspNetCoreHero.Abstractions.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_CourtType",Schema ="LDiary")]
    public class CourtTypeEntity : AuditableEntity
    {
        [Required]
        public string CourtType { get; set; }
    }
}