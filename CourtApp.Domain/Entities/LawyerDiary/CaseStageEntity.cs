using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_CaseStage", Schema = "LDiary")]
    public class CaseStageEntity : AuditableEntity
    {
        [Required]
        public string CaseStage { get; set; }
    }
}