using System;

namespace CourtApp.Web.Areas.Client.Model
{
    public class GClientViewModel
    {
        public Guid UId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
