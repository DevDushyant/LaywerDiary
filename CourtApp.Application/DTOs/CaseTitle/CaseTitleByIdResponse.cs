using System;

namespace CourtApp.Application.DTOs.CaseTitleRes
{
    public class CaseTitleByIdResponse
    {
        public  Guid Id { get; set; }
        public  Guid CaseId { get; set; }
        public int TypeId { get; set; }
        public  string Title { get; set; }
    }
}
