using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("case_detail_against", Schema = "ld")]
    public class CaseDetailAgainstEntity
    {        
        public Guid Id { get; set; }        
        public DateTime ImpugedOrderDate { get; set; }
        public  Guid CourtTypeId { get; set; }
        public  Guid CourtBenchId { get; set; }
        public  int StateId { get; set; }
        public  int StrengthId { get; set; }
        public  string CaseNo { get; set; }
        public  int CaseYear { get; set; }
        public string CisNo { get; set; }
        public int CisYear { get; set; }
        public string CnrNo { get; set; }
        public string  OfficerName { get; set; }
        public string  Cadre { get; set; }
    }
}
