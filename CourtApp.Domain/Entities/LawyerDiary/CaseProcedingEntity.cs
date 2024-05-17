using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("r_case_proceeding", Schema = "ld")]
    public class CaseProcedingEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public Guid HeadId { get; set; }
        public Guid SubHeadId { get; set; }
        public Guid StageId { get; set; }
        public DateTime NextDate { get; set; }
        public string Remark { get; set; }
        public string Abbreviation { get; set; }
    }
}
