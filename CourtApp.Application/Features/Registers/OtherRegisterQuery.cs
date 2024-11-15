using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Registers
{
    public class OtherRegisterQuery : IRequest<Result<List<OtherRegisterResponse>>>
    {
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
    }
    public class OtherRegisterQueryHandler : IRequestHandler<OtherRegisterQuery, Result<List<OtherRegisterResponse>>>
    {
        private readonly IUserCaseRepository _caseRepo;
        private readonly ICaseProceedingRepository _ProcRepo;
        private readonly IWorkMasterRepository _wRepo;
        public OtherRegisterQueryHandler(IUserCaseRepository _caseRepo,
            IWorkMasterRepository _wRepo,
            ICaseProceedingRepository procRepo)
        {
            this._caseRepo = _caseRepo;
            this._wRepo = _wRepo;
            _ProcRepo = procRepo;
        }
        public async Task<Result<List<OtherRegisterResponse>>> Handle(OtherRegisterQuery request, CancellationToken cancellationToken)
        {

            //Expression<Func<CaseWorkEntity, OtherRegisterResponse>> expression = e => new OtherRegisterResponse
            //{
            //    Id = e.CaseId,
            //    Title = e.Case.CaseType.Abbreviation + "(" + e.Case.CaseNo.ToString() + "/" + e.Case.CaseYear.ToString() + ")" + e.Case.FirstTitle + "Vs" + e.Case.SecondTitle,
            //    WorkDate = e.Status == 1 && e.LastModifiedOn != null ? e.LastModifiedOn.Value.ToString("dd/MM/yyyy") : "",
            //    WorkDone = e.Work.Name_En,
            //    WorkType = e.WorkType.Work_En
            //};
            //var predicate = PredicateBuilder.True<CaseWorkEntity>();
            //DateTime to = DateTime.Now;
            //if (request.ToDt != default(DateTime)) to = request.ToDt;
            //if (request.FromDt != default(DateTime))
            //    predicate = predicate.And(b => b.LastModifiedOn.Value >= request.FromDt && b.LastModifiedOn.Value <= to);
            //predicate = predicate.And(w => w.WorkType.Abbreviation != "COPY" && w.WorkType.Abbreviation != "DISP");
            //var fndt = _wRepo.Entities.Where(predicate)
            //    .Select(expression)
            //    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            var distinctCases = await _ProcRepo.Entities
                                .Where(w => w.CreatedBy.Equals(request.UserId))
                             .Include(c => c.Case)
                                 .ThenInclude(c => c.CaseType)
                             .Include(c => c.ProcWork)
                                 .ThenInclude(pw => pw.Works)
                             .ToListAsync(); // Load into memory           
            if (distinctCases.Any())
            {

                var CaseWorkDetailsWithWork = distinctCases
                        .SelectMany(c => c.ProcWork.Works
                        .Select(work => new // Flatten Works and retain Case reference
                        {
                            Case = c.Case,
                            Work = work
                        }))
                        .Join(
                            _wRepo.Entities
                            .Where(w => !w.Abbreviation.Equals("COPY")), // Filtered WorkRepo entries
                            cw => cw.Work.WorkTypeId, // Key from CaseWorkDetail (WorkTypeId)
                            w => w.Id,                // Key from _WorkRepo (Id)
                            (cw, w) => new            // Project result with CaseWorkDetail and Work
                            {
                                Case = cw.Case,
                                CaseWorkDetail = cw.Work,
                                Work = w
                            }
                        )
                        .ToList();
                List<OtherRegisterResponse> awc = new List<OtherRegisterResponse>();
                if (CaseWorkDetailsWithWork.Count() > 0)
                {
                    foreach (var cd in CaseWorkDetailsWithWork)
                    {
                        OtherRegisterResponse a = new OtherRegisterResponse();
                        a.Id = cd.Case.Id;
                        a.Title = cd.Case.FirstTitle+" Vs "+cd.Case.SecondTitle;
                        a.WorkDone =cd.Work.Work_En;
                        a.WorkDate= cd.Work.LastModifiedOn!=null?cd.Work.LastModifiedOn.Value.ToString("dd/MM/yyyy"):"-";                        
                        awc.Add(a);
                    }
                }               
                return Result<List<OtherRegisterResponse>>.Success(awc.ToList());
            }
            return null;           
        }
    }
}
