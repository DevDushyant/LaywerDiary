using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CaseWorking;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CourtApp.Application.Features.CaseWork
{
    public class GetAssignedWorkQuery : IRequest<Result<List<AssignedWorkToCaseResponse>>>
    {
        public Guid CaseId { get; set; }
        public DateTime WorkingDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAssignedWorkQueryHandler : IRequestHandler<GetAssignedWorkQuery, Result<List<AssignedWorkToCaseResponse>>>
    {
        private readonly ICaseWorkRepository _Repository;
        private readonly ICaseProceedingRepository _ProcRepo;
        private readonly IWorkMasterSubRepository _SWRepo;
        public GetAssignedWorkQueryHandler(ICaseWorkRepository _Repository,
            ICaseProceedingRepository _ProcRepo,
            IWorkMasterRepository wRepo,
            IWorkMasterSubRepository sWRepo)
        {
            this._Repository = _Repository;
            this._ProcRepo = _ProcRepo;
            _SWRepo = sWRepo;
        }
        public async Task<Result<List<AssignedWorkToCaseResponse>>> Handle(GetAssignedWorkQuery request, CancellationToken cancellationToken)
        {
            var CaseWorkDetail = await _ProcRepo.GetListAsync();
            if (CaseWorkDetail.Any())
            {
                List<AssignedWorkToCaseResponse> awc = new List<AssignedWorkToCaseResponse>();
                foreach (var c in CaseWorkDetail.Distinct())
                {
                    AssignedWorkToCaseResponse a = new AssignedWorkToCaseResponse();
                    a.CaseId = c.CaseId;
                    a.CaseDetail = c.Case.FirstTitle + " " + c.Case.SecondTitle + " (" + c.Case.CaseYear + "/" + c.Case.CaseNo + ")";
                    a.AWorks = new List<AssignedWork>();
                    a.LastWorkingDate = c.ProcWork.LastWorkingDate != null ? c.ProcWork.LastWorkingDate.Value.ToString("dd-MM-yyyy") : "";
                    foreach (var w in c.ProcWork.Works.Where(s => s.Status == 0))
                    {
                        AssignedWork aw = new AssignedWork();
                        aw.Id = c.Id;
                        aw.WorkId = w.WorkId;
                        var swork = await _SWRepo.GetByIdAsync(w.WorkId);
                        aw.WorkDetail = swork != null ? swork.Work.Work_En + " - " + swork.Name_En : "";
                        a.AWorks.Add(aw);
                    }
                    awc.Add(a);
                }
                var workc = awc.Where(w => w.AWorks.Count > 0).ToList();                    
                if (workc.Count() > 0)
                    return Result<List<AssignedWorkToCaseResponse>>.Success(workc);
                else
                    return Result<List<AssignedWorkToCaseResponse>>.Success();
            }
            return Result<List<AssignedWorkToCaseResponse>>.Fail("There is no record");
            //Expression<Func<CaseWorkEntity, CaseWorkDto>> expression = e => new CaseWorkDto
            //{
            //    Id = e.Id,
            //    CaseId = e.Case.Id,
            //    CaseTitle = e.Case.FirstTitle + " V/S " + e.Case.SecondTitle + "(" + e.Case.CaseNo + "/" + e.Case.CaseYear + ")",
            //    Work = e.Work.Work.Work_En,
            //    WorkId = e.Work.Work.Id,
            //    SubWork = e.Work.Name_En,
            //    SubWId = e.Work.Id,
            //    WDate = e.WorkingDate != null ? e.WorkingDate.Value.ToString("dd/MM/yyyy") : "",
            //    Status = e.Status,

            //};
            //var predicate = PredicateBuilder.True<CaseWorkEntity>();
            //if (request.CaseId != Guid.Empty)
            //    predicate = predicate.And(b => b.CaseId == request.CaseId);
            //try
            //{

            //    var rData = _Repository.Entities.Where(predicate)
            //        .Select(expression)
            //        .Where(w=>w.Status==0)
            //        .ToList();

            //    var CaseDetail = rData.Select(c => new { CId = c.CaseId, CDetail = c.CaseTitle })
            //        .Distinct().ToList();

            //    List<AssignedWorkToCaseResponse> dtl = new List<AssignedWorkToCaseResponse>();
            //    foreach (var cd in CaseDetail)
            //    {
            //        AssignedWorkToCaseResponse assigned = new AssignedWorkToCaseResponse();
            //        assigned.CaseDetail = cd.CDetail;
            //        assigned.CaseId = cd.CId;
            //        List<AssignedWork> Works = new List<AssignedWork>();
            //        foreach (var w in rData)
            //        {                        
            //            if (cd.CId == w.CaseId)
            //            {
            //                var asW = new AssignedWork();
            //                asW.Id = w.Id;
            //                asW.WorkId = w.SubWId;
            //                asW.WorkDetail = w.Work + " - " + w.SubWork;                            
            //                Works.Add(asW);
            //            }
            //        }
            //        assigned.AWorks = Works;
            //        dtl.Add(assigned);
            //    }               
            //    return Result<List<AssignedWorkToCaseResponse>>.Success(dtl);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    return null;
            //}
        }
    }
}
