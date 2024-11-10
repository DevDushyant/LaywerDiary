using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using CourtApp.Application.Extensions;
using System.Collections.Generic;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Application.DTOs.CaseWorking;
using Microsoft.EntityFrameworkCore;
using static CourtApp.Application.Constants.Permissions;
namespace CourtApp.Application.Features.Registers
{
    public class CopyingRegisterQuery : IRequest<Result<List<CopyDisposalResponse>>>
    {
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int SearchType { get; set; }
        public string UserId { get; set; }
    }
    public class CopyingRegisterQueryHandler : IRequestHandler<CopyingRegisterQuery, Result<List<CopyDisposalResponse>>>
    {
        private readonly IUserCaseRepository _caseRepo;
        private readonly ICaseWorkRepository _wRepo;
        private readonly ICaseProceedingRepository _ProcRepo;
        private readonly IWorkMasterRepository _WorkRepo;
        public CopyingRegisterQueryHandler(IUserCaseRepository _caseRepo
            , ICaseWorkRepository _wRepo,
ICaseProceedingRepository procRepo,
IWorkMasterRepository _WorkRepo)
        {
            this._caseRepo = _caseRepo;
            this._wRepo = _wRepo;
            _ProcRepo = procRepo;
            this._WorkRepo = _WorkRepo;
        }
        public async Task<Result<List<CopyDisposalResponse>>> Handle(CopyingRegisterQuery request, CancellationToken cancellationToken)
        {
            var distinctCases = await _ProcRepo.Entities
                                .Where(w => w.CreatedBy.Equals(request.UserId))
                             .Include(c => c.Case)
                                 .ThenInclude(c => c.CaseType)
                             .Include(c => c.ProcWork)
                                 .ThenInclude(pw => pw.Works)
                             .GroupBy(d => d.CaseId)
                             .Select(g => g.FirstOrDefault()) // Select distinct cases by CaseId
                             .ToListAsync(); // Load into memory           
            if (distinctCases.Any())
            {
                // Now you can safely flatten the Works collection in memory
                var CaseWorkDetail = distinctCases
                    .Where(c => c.ProcWork != null)
                    .SelectMany(c => c.ProcWork.Works)
                    .ToList();
                var dt = from c in CaseWorkDetail
                         join w in _WorkRepo.Entities.Where(w => w.Abbreviation.Equals("COPY")) on c.WorkTypeId equals w.Id
                         select new
                         {
                             CaseWorkDetail = c,
                             Work = w
                         };
                List<CopyDisposalResponse> awc = new List<CopyDisposalResponse>();
                if (dt.Count() > 0)
                {
                    foreach (var cd in distinctCases)
                    {
                        CopyDisposalResponse a = new CopyDisposalResponse();
                        var wr = cd.ProcWork.Works.Select(s => new
                        {
                            s.WorkId,
                            s.WorkTypeId,
                            s.ReceivedOn,
                            s.Status,
                            s.AppliedOn
                        }).FirstOrDefault();
                        var workd = await _WorkRepo.GetByIdAsync(wr.WorkId);
                        a.Id = cd.Case.Id;
                        a.CourtType = "";
                        a.CaseNo = cd.Case.CaseNo;
                        a.CaseYear = cd.Case.CaseYear;
                        a.CourtBench = "";
                        a.FirstTitle = cd.Case.FirstTitle;
                        a.SecondTitle = cd.Case.SecondTitle;
                        a.CaseType = "";
                        a.CaseAbbretion = "";
                        if (workd != null)
                            a.Reason = workd.Work_En;
                        a.AppliedOn = wr.AppliedOn != default(DateTime) ? wr.AppliedOn.ToString("dd/MM/yyyy") : "";
                        a.ReceivedOn = wr.ReceivedOn != default(DateTime) ? wr.ReceivedOn.ToString("dd/MM/yyyy") : "";
                        awc.Add(a);
                    }

                }
                return Result<List<CopyDisposalResponse>>.Success(awc.ToList());
            }
            //int[] status = new int[] { 1, 2 };
            //IQueryable<CaseWorkEntity> cWdt = _wRepo.Entities.Where(w => w.WorkType.Abbreviation == "COPY"
            //&& status.Contains(w.Status)).Distinct();
            //if (request.SearchType == 2) cWdt = cWdt.Where(s => s.Status == 1);
            //if (request.SearchType == 3) cWdt = cWdt.Where(s => s.Status == 2);            
            //if (cWdt.Count() > 0)
            //{
            //    var fndt = from cd in _caseRepo.Entites
            //               join w in cWdt on cd.Id equals w.CaseId
            //               select new CopyDisposalResponse
            //               {
            //                   Id = cd.Id,
            //                   CourtType = cd.CourtType.CourtType,
            //                   CaseNo = cd.CaseNo,
            //                   CaseYear = cd.CaseYear,
            //                   CourtBench = cd.CourtBench.CourtBench_En,
            //                   FirstTitle = cd.FirstTitle,
            //                   SecondTitle = cd.SecondTitle,
            //                   CaseType = cd.CaseType.Name_En,
            //                   CaseAbbretion = cd.CaseType.Abbreviation,
            //                   Reason = w.Work.Name_En,
            //                   AppliedOn = w.AppliedOn != null ? w.AppliedOn.Value.ToString("dd/MM/yyyy") : "",
            //                   ReceivedOn = w.ReceivedOn != null ? w.ReceivedOn.Value.ToString("dd/MM/yyyy") : "",
            //               };
            //    if (fndt != null)
            //        return await fndt.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            //}
            return null;
        }
    }
}
