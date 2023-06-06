using AspNetCoreHero.Abstractions.Domain;
using CourtApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_CourtFeeStructure", Schema = "LDiary")]
    public class CourtFeeStructureEntity : AuditableEntity
    {
        [Key]
        public new Guid Id { get; set; }

        [ForeignKey("State")]
        public string StateCode { get; set; }
        public virtual StateEntity State { get; set; }
        public Double MinValue { get; set; }
        public Double MaxValue { get; set; }
        public Double Rate { get; set; }
        public Double FixAmount { get; set; }
    }
}
