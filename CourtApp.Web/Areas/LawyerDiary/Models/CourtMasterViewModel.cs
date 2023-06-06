using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CourtMasterViewModel
    {
        
        public SelectList CourtTypes { get; set; }
        public SelectList States { get; set; }
        public SelectList Districts { get; set; }
        public int DistrictCode { get; set; }
        public string  StateCode { get; set; }
        public int CourtTypeId { get; set; }
        public string CourtType { get; set; }
        public string CourtName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
        public Guid UniqueId { get; set; }
        public string State { get; set; }
        public string District { get; set; }
    }
}
