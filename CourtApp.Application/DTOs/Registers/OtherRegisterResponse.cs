﻿using CourtApp.Application.DTOs.CaseDetails;
namespace CourtApp.Application.DTOs.Registers
{
    public class OtherRegisterResponse : CaseDetailDto
    {
        public string WorkType { get; set; }
        public string WorkDone { get; set; }
        public string WorkDate { get; set; }
        public string Reference { get; set; }
    }
}
