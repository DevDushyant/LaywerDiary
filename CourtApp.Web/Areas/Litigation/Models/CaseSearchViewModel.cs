using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseSearchViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string NoYear { get; set; }
        public string Title { get; set; }
        public string DocFilePath { get; set; }
    }
}
