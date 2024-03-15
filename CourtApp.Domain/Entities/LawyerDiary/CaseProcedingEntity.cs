using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    public class CaseProcedingEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public int ProcedingId { get; set; }
        public DateTime ProcedingDate { get; set; }

    }
}
