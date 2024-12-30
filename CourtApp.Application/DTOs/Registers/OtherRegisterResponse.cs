using CourtApp.Application.DTOs.CaseDetails;
using System;
namespace CourtApp.Application.DTOs.Registers
{
    public class OtherRegisterResponse : CaseDetailDto
    {        
        public string WorkType { get; set; }
        public string WorkDone { get; set; }
        public string WorkDate { get; set; }
    }
}
