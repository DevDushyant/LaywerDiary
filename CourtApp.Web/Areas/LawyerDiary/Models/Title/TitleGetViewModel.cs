using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models.Title
{
    public class TitleGetViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CaseDetail { get; set; }
        public string Type { get; set; }
    }
}
