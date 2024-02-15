using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    public class CaseWorkEntity : AuditableEntity
    {
        [ForeignKey("CaseEntity")]
        public Guid CaseId { get; set; }
        public virtual CaseEntity CaseEntity { get; set; }

        [ForeignKey("StageEntity")]
        public int CaseStageId { get; set; }
        public virtual CaseStageEntity StageEntity { get; set;}

        [ForeignKey("CaseWorkMasterEntity")]
        public int CaseWorkID { get; set; } 
        public virtual CaseWorkMasterEntity CaseWorkMasterEntity { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime NextDate { get; set; }
        public string Remark { get; set; }

    }
}
