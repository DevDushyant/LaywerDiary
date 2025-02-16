using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CourtApp.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? DemographicId { get; set; }
        public Demographic Demographic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ProfileImgPath { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;

        /// <summary>
        /// User type whether the user is lawyer, operator and etc.
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Navigation property for operators if the user is a lawyer.
        /// </summary>
        public ICollection<OperatorUser> Operators { get; set; }
    }
}