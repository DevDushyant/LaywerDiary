using System;
namespace CourtApp.Application.DTOs.CaseDetails
{
    public abstract class CaseDetailDto
    {
        public Guid Id { get; set; }
        public string InsititutionDate { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public string SecondTitle { get; set; }
        public string CourtType { get; set; }
        public string CaseAbbretion { get; set; }
        public string CaseType { get; set; }
        public string CourtBench { get; set; }
        public string Stage { get; set; }
    }
}
