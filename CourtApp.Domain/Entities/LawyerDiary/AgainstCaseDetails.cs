using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("Against_case_details", Schema = "ld")]
    public class AgainstCaseDetails : AuditableEntity
    {
        
        public new Guid Id { get; set; }
        public Guid clientcaseid { get; set; }
        public DateTime? CaseAgainstDecisionDate { get; set; }
        public string AgainstCaseNumber { get; set; }
        public int? AgainstYear { get; set; }
       // public Guid LinkedCaseId { get; set; }
        public Guid AgainstCourtTypeId { get; set; }
        public Guid AgainstCourtId { get; set; }
        //public DateTime NextDate { get; set; }

        #region Relation Area
        [ForeignKey("clientcaseid")]
        public virtual CaseEntity CaseDetails { get; set; }

        [ForeignKey("AgainstCourtTypeId")]
        public virtual CourtTypeEntity AgainstCourtType { get; set; }
        [ForeignKey("AgainstCourtId")]
        public virtual CourtMasterEntity AgainstCourt { get; set; }
        #endregion
    }
}
