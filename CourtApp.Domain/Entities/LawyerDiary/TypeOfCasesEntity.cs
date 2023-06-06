using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_Typeofcases", Schema = "LDiary")]
    public class TypeOfCasesEntity : AuditableEntity
    {
        [Required]
        [ForeignKey("CaseNature")]
        public int CaseNatureId { get; set; }
        public virtual CaseNatureEntity CaseNature { get; set; }

        [Required]
        public string Typeofcases { get; set; }
    }
}