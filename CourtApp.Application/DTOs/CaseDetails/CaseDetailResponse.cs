﻿using System;

namespace CourtApp.Application.DTOs.Case
{
    public class CaseDetailResponse
    {
        public Guid Id { get; set; }
        public string CaseTypeName { get; set; }
        public string CourtType { get; set; }
        public string CaseKindName { get; set; }
        public string CaseNumber { get; set; }
        public string CaseYear{ get; set; }
        public DateTime NextHearingDate { get; set; }
        public string CaseStage { get; set; }
        public string FTitleType { get; set; }
        public string FirstTitle { get; set; }
        public string STitleType { get; set; }
        public string SecondTitle { get; set; }
        public string CourtName { get; set; }
        public string CaseTitle { get; set; }
        public string Abbreviation { get; set; }       
    }
}
