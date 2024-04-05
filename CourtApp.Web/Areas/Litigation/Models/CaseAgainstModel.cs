using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseAgainstModel
    {
        public DateTime ImpugedDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public string Number { get; set; }
        public int CaseYear { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public string ProcOfficer { get; set; }
        public string Cadre { get; set; }
       
    }
}
