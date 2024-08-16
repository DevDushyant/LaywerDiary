using System;

namespace CourtApp.Application.DTOs.CaseTitle
{
    public class CaseTitleResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Case { get; set; }
        public string Type { get; set; }
    }
}
