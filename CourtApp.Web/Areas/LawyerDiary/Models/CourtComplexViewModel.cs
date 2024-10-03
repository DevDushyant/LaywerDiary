using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CourtComplexViewModel
    {
        public SelectList States { get; set; }
        public SelectList Districts { get; set; }
        public SelectList CourtDistricts { get; set; }
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string CDistrictName { get; set; }
        public string Abbreviation { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public Guid CourtDistrictId { get; set; }
        
    }
}
