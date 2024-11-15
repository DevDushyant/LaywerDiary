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
                            _WorkRepo.Entities
                            .Where(w => w.Abbreviation.Equals("COPY")), // Filtered WorkRepo entries
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
                List<CopyDisposalResponse> awc = new List<CopyDisposalResponse>();
                if (CaseWorkDetailsWithWork.Count() > 0)
                {
                    foreach (var cd in CaseWorkDetailsWithWork)
                    {
                        CopyDisposalResponse a = new CopyDisposalResponse();
                        a.Id = cd.Case.Id;
                        a.CourtType = "";
                        a.CaseNo = cd.Case.CaseNo;
                        a.CaseYear = cd.Case.CaseYear;
                        a.CourtBench = "";
                        a.FirstTitle = cd.Case.FirstTitle;
                        a.SecondTitle = cd.Case.SecondTitle;
                        a.CaseType = "";
                        a.CaseAbbretion = "";
                        a.AppliedOn = cd.CaseWorkDetail.AppliedOn != default(DateTime) ? cd.CaseWorkDetail.AppliedOn.ToString("dd/MM/yyyy") : "";
                        a.ReceivedOn = cd.CaseWorkDetail.ReceivedOn != default(DateTime) ? cd.CaseWorkDetail.ReceivedOn.ToString("dd/MM/yyyy") : "";
                        a.Reason = cd.Work.Work_En;
                        awc.Add(a);
                    }
                }
                if (request.SearchType == 3) awc = awc.Where(w => w.ReceivedOn != "").ToList();
                if (request.SearchType == 2) awc = awc.Where(w => w.AppliedOn != "").ToList();
                return Result<List<CopyDisposalResponse>>.Success(awc.ToList());
            }
            return null;
        }
    }
}
