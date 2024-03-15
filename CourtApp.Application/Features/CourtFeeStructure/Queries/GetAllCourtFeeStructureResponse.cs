using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtFeeStructure.Queries
{
    public class GetAllCourtFeeStructureResponse
    {
        public Guid Id { get; set; }   
        public string StateName { get; set; }        
        public Double MinValue { get; set; }
        public Double MaxValue { get; set; }
        public Double Rate { get; set; }
        public Double FixAmount { get; set; }
    }
}
