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
        public required Guid CourtTypeId { get; set; }
        public required Guid CourtId { get; set; }
        public required string Number { get; set; }
        public required int Year { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public string  ProcOfficer { get; set; }
        public string  Cadder { get; set; }
    }
}
