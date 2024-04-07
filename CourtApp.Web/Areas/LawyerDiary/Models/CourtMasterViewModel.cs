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
        public SelectList CourtDistricts { get; set; }
        public int DistrictCode { get; set; }
        public int  StateCode { get; set; }
        public Guid CourtTypeId { get; set; }
        public string CourtType { get; set; }
        public string CourtName { get; set; }       
        public Guid Id { get; set; }
        public Guid CourtDistrictId { get; set; }
        public Guid CourtComplexId { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public List<CourtBench> CBAddress { get; set; }
}

    public class CourtBench
    {
        public string Court_Bench_Name { get; set; }
        public string Court_Bench_Address { get; set; }
    }
}
