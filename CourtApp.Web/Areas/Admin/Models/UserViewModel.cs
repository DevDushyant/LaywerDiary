using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CourtApp.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; } = true;
        public string EnrollmentNo { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        //public string Password { get; set; }
        //public string ConfirmPassword { get; set; }

        public byte[] ProfilePicture { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Id { get; set; }
    }
}