using CourtApp.Infrastructure.Identity.Models;
using System;

namespace CourtApp.Web.Areas.Admin.Models
{
    public class OperatorViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfJoining { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
