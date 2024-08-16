using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models.Lawyer
{
    public class LawyerUpsertViewModel
    {
        public Guid  Id{ get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EnrollNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
