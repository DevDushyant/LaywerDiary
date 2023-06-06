using AspNetCoreHero.Abstractions.Domain;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    public class CaseProcedingEntity : AuditableEntity
    {
        [ForeignKey("CaseEntity")]
        public Guid CaseId { get; set; }
        public virtual CaseEntity CaseEntity { get; set; }

        [ForeignKey("CaseProceding")]
        public int ProcedingId { get; set; }
        public virtual CaseProcedingMasterEntity CaseProceding { get; set; }
        public DateTime ProcedingDate { get; set; }

    }
}
