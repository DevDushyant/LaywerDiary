using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models.Title
{
    public class TitleGetViewModel
    {
        public Guid CaseId { get; set; }
        public string Type { get; set; }
        public string CaseDetail { get; set; }
    }
}
