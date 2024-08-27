using System;
namespace CourtApp.Application.DTOs.FormBuilder
{
    public class CaseDarftingDetailDtoByIdResponse
    {
        public Guid Id { get; set; }
        public string CaseTitle { get; set; }
        public string TemplateName { get; set; }        
    }
}
