using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string LandMark { get; set; }
        public string HouseNo { get; set; }

        public SelectList Countries { get; set; }
        public string CountryCode { get; set; }
        public SelectList States { get; set; }
        public string   StateCode { get; set; }
        public SelectList Districts { get; set; }
        public int DistrictCode { get; set; }
        public SelectList Cities { get; set; }
        public int CityId { get; set; }
        public SelectList ReferedBy { get; set; }
        public SelectList Appearence { get; set; }
        public SelectList Narration { get; set; }
    }
}
