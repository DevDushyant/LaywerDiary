using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("r_case_working", Schema = "ld")]
    public class CaseWorkEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public Guid WorkId { get; set; }
        public Guid SubWorkId { get; set; }
        public DateTime WorkingDate { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public virtual WorkMasterEntity Work { get; set; }
        public virtual WorkMasterSubEntity SubWork { get; set; }
        public virtual CaseDetailEntity Case { get; set; }
    }
}
