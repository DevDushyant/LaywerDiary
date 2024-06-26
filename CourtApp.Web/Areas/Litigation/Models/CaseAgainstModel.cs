using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseAgainstModel
    {
        public Guid Id { get; set; }
        public DateTime? ImpugedOrderDate { get; set; }
        public Guid? CourtTypeId { get; set; }
        public Guid? CourtBenchId { get; set; }
        public Guid? CaseCategoryId { get; set; }
        public Guid? CaseTypeId { get; set; }
        public int? StateId { get; set; }
        public int? StrengthId { get; set; }
        public string CaseNo { get; set; }
        public int? CaseYear { get; set; }        
        public int? CisYear { get; set; }       
        public string OfficerName { get; set; }
        public string Cadre { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? AgainstBenchId { get; set; }
        public Guid? ComplexId { get; set; }
        public Guid? CourtId { get; set; }
        public string CisNumber { get; set; }
        public string CnrNumber { get; set; }

    }
}
