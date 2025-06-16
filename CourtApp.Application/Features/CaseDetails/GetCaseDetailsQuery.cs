using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.UserCase
{
    public class GetCaseDetailsQuery : IRequest<PaginatedResult<CaseDetailResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public int Year { get; set; }
        public int StateCode { get; set; }
        public int DistrictCode { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CaseTyepId { get; set; }
        public DateTime HearingDate { get; set; }
        public string CallingFrm { get; set; }
        public List<string> LinkedIds { get; set; }
        //public string UserId { get; set; }
    }
    public class GetCaseDetailsQueryHandler : IRequestHandler<GetCaseDetailsQuery, PaginatedResult<CaseDetailResponse>>
    {
        private readonly ICourtTypeCacheRepository _RepoCourtType;
        private readonly ICourtTypeRepository _RepoCrtType;
        private readonly ICaseStageCacheRepository _RepoStage;
        private readonly ICaseNatureCacheRepository _RepoNature;
        private readonly IUserCaseRepository _RepoCase;
        private readonly ICaseProceedingRepository _RepoProceeding;
        private readonly IFSTitleCacheRepository _RepoFSTitle;
        private readonly ICourtBenchRepository _RepoCourtBench;
        private readonly ICaseAssignedRepository _assignRepo;
        public GetCaseDetailsQueryHandler(ICaseNatureCacheRepository repoNature,
            IUserCaseRepository repoCase, ICourtTypeCacheRepository RepoCourtType,
            ICaseStageCacheRepository RepoStage, ICaseProceedingRepository repoProceeding,
            IFSTitleCacheRepository repoFSTitle, ICourtBenchRepository repoCourtBench,
            ICourtTypeRepository repoCrtType, ICaseAssignedRepository assignRepo)
        {
            _RepoNature = repoNature;
            _RepoCase = repoCase;
            _RepoCourtType = RepoCourtType;
            _RepoStage = RepoStage;
            _RepoProceeding = repoProceeding;
            _RepoFSTitle = repoFSTitle;
            _RepoCourtBench = repoCourtBench;
            _RepoCrtType = repoCrtType;
            _assignRepo = assignRepo;
        }
        public async Task<PaginatedResult<CaseDetailResponse>> Handle(GetCaseDetailsQuery request, CancellationToken cancellationToken)
        {

            try
            {

                //Step 1: Getting all case of logged in user.
                var userCaseQuery = (from c in _RepoCase.Entites
                                     join ac in _assignRepo.Entities on c.Id equals ac.CaseId into caseAssignments
                                     from ac in caseAssignments.DefaultIfEmpty()
                                     where request.LinkedIds.Contains(c.CreatedBy)
                                           || request.LinkedIds.Contains(ac.LawyerId.ToString())
                                     let refer = ac != null && request.LinkedIds.Contains(ac.LawyerId.ToString()) ? "Assigned" : "Self"
                                     let maxProcDate = c.CaseProcEntities.Any()
                                         ? c.CaseProcEntities
                                             .OrderByDescending(p => p.NextDate)
                                             .Select(p => p.NextDate)
                                             .FirstOrDefault() ?? default
                                         : (c.NextDate ?? default)
                                     let matchingProceeding = c.CaseProcEntities
                                         .FirstOrDefault(p => p.ProceedingDate.HasValue &&
                                                              p.ProceedingDate.Value.Date == request.HearingDate.Date)

                                     let isCaseAssigned = refer == "Self" && ac != null && ac.CaseId == c.Id
                                     let AssignedLawyerId = refer == "Self" && ac != null ? ac.LawyerId : Guid.Empty
                                     select new CaseDetailResponse
                                     {
                                         Id = c.Id,
                                         Reference = refer,
                                         CaseNumber = c.CaseNo,
                                         FTitleType = c.FTitle.Name_En,
                                         FirstTitle = c.FirstTitle,
                                         STitleType = c.STitle.Name_En,
                                         SecondTitle = c.SecondTitle,
                                         CaseYear = c.CaseYear.ToString(),
                                         CourtType = c.CourtType.CourtType.ToString(),
                                         CaseTypeName = c.CaseType.Name_En,
                                         CourtName = c.CourtBench.CourtBench_En,
                                         CaseStage = c.CaseStage.CaseStage,
                                         CaseTitle = (c.FirstTitle + " V/S " + c.SecondTitle + " [" +
                                                      (string.IsNullOrEmpty(c.CaseNo)
                                                          ? c.CaseYear.ToString()
                                                          : c.CaseNo + "/" + c.CaseYear.ToString()) +
                                                      "]").ToUpperInvariant(),
                                         NextHearingDate = maxProcDate,
                                         IsProceedingDone = matchingProceeding != null,
                                         ProceedingDate = matchingProceeding != null
                                             ? matchingProceeding.ProceedingDate.Value
                                             : default,
                                         IsCaseAssigned=isCaseAssigned,
                                         LawyerId= AssignedLawyerId
                                     })
                                    .OrderByDescending(o => o.CaseYear)
                                    .AsQueryable();

                // ✅ Step 2: Apply hearing date filter if provided (matches either ProceedingDate OR NextHearingDate)
                if (request.HearingDate != default)
                {
                    var hearingDate = request.HearingDate.Date;

                    userCaseQuery = userCaseQuery.Where(c =>
                        (c.ProceedingDate == hearingDate) // Proceeding on same date
                        || c.NextHearingDate.Date == hearingDate                                 // OR Next date matches
                    );
                }
                return await userCaseQuery
                            .OrderByDescending(c => c.NextHearingDate)
                            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
