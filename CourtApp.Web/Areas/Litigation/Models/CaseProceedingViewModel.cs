using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseProceedingViewModel
    {
        public Guid CaseId { get; set; }
        public Guid PHeadId { get; set; }
        public Guid PSHeadId { get; set; }
    }
}
