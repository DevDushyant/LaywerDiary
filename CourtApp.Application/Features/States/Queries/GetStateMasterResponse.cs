using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.States.Queries
{
    public class GetStateMasterResponse
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
}