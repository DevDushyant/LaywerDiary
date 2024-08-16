using System;
namespace CourtApp.Application.DTOs.Registers
{
    public class OtherRegisterResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string WorkType { get; set; }
        public string WorkDone { get; set; }
        public string WorkDate { get; set; }
    }
}
