using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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
