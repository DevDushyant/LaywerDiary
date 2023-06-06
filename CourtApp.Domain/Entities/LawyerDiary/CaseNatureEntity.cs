using AspNetCoreHero.Abstractions.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_CaseNature", Schema = "LDiary")]
    public class CaseNatureEntity : AuditableEntity
    {
        [Required]
        public string  CaseNature { get; set; }
    }
}