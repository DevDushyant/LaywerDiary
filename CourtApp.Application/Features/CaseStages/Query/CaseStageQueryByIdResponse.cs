﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStages.Query
{
    public class CaseStageQueryByIdResponse
    {
        public int Id { get; set; }
        public string CaseStage { get; set; }
    }
}
