using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class UpdateHearingDtViewModel
    {
        public Guid CaseId { get; set; }
        public DateTime HearingDt { get; set; }
    }
}
