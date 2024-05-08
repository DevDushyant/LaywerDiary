using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CaseWorking;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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
        public GetAssignedWorkQueryHandler(ICaseWorkRepository _Repository)
        {
            this._Repository = _Repository;
        }
        public async Task<Result<List<AssignedWorkToCaseResponse>>> Handle(GetAssignedWorkQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseWorkEntity, CaseWorkDto>> expression = e => new CaseWorkDto
            {
                Id = e.Id,
                CaseId = e.Case.Id,
                CaseTitle = e.Case.FirstTitle + " V/S " + e.Case.SecondTitle + "(" + e.Case.CaseNo + "/" + e.Case.CaseYear + ")",
                Work = e.SubWork.Work.Work_En,
                WorkId = e.SubWork.Work.Id,
                SubWork = e.SubWork.Name_En,
                SubWId = e.SubWork.Id,
                WDate = e.WorkingDate.ToString("dd/MM/yyyy"),
                Status = e.Status,

            };
            var predicate = PredicateBuilder.True<CaseWorkEntity>();
            if (request.CaseId != Guid.Empty)
                predicate = predicate.And(b => b.CaseId == request.CaseId);
            try
            {

                var rData = _Repository.Entities.Where(predicate)
                    .Select(expression)
                    .Where(w=>w.Status==0)
                    .ToList();
                
                var CaseDetail = rData.Select(c => new { CId = c.CaseId, CDetail = c.CaseTitle })
                    .Distinct().ToList();

                List<AssignedWorkToCaseResponse> dtl = new List<AssignedWorkToCaseResponse>();
                foreach (var cd in CaseDetail)
                {
                    AssignedWorkToCaseResponse assigned = new AssignedWorkToCaseResponse();
                    assigned.CaseDetail = cd.CDetail;
                    assigned.CaseId = cd.CId;
                    List<AssignedWork> Works = new List<AssignedWork>();
                    foreach (var w in rData)
                    {                        
                        if (cd.CId == w.CaseId)
                        {
                            var asW = new AssignedWork();
                            asW.WorkId = w.SubWId;
                            asW.WorkDetail = w.Work + " - " + w.SubWork;                            
                            Works.Add(asW);
                        }
                    }
                    assigned.AWorks = Works;
                    dtl.Add(assigned);
                }               
                return Result<List<AssignedWorkToCaseResponse>>.Success(dtl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
