using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("case_detail_against", Schema = "ld")]
    public class AgainstCaseDetails : AuditableEntity
    {        
        public new Guid Id { get; set; }
        public DateTime ImpugedOrderDate { get; set; }

        [ForeignKey("CourtTypeId")]
        public virtual CourtTypeEntity CourtType { get; set; }

        [ForeignKey("CourtId")]
        public virtual CourtMasterEntity Court { get; set; }
        public required string Number { get; set; }
        public required int Year { get; set; }
        public int CisNumber { get; set; }
        public int CisYear { get; set; }
        public int CnrNumber { get; set; }
        public string  ProcOfficer { get; set; }
        public string  Cadder { get; set; }
    }
}
