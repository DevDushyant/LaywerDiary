using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class ClientsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName  => FirstName + " " + LastName;

        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public SelectList States { get; set; }
        public string StateCode { get; set; }
        public SelectList Districts { get; set; }
        public int DistrictCode { get; set; }

    }
}
