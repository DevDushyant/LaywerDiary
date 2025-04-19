using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.UserCase
{
    public class GetCaseDetailsQuery : IRequest<Result<List<CaseDetailResponse>>>
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
    public class GetCaseDetailsQueryHandler : IRequestHandler<GetCaseDetailsQuery, Result<List<CaseDetailResponse>>>
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
        public async Task<Result<List<CaseDetailResponse>>> Handle(GetCaseDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var hearingDate = request.HearingDate.Date;

                // Step 1: Get proceeding data
                var proceedingData = _RepoProceeding.Entities.AsQueryable();
                if (request.HearingDate != default)
                {
                    proceedingData = proceedingData
                        .Where(n => n.NextDate.HasValue && n.NextDate.Value.Date == hearingDate);
                }

                // Step 2: Dictionary for Max Proceeding Dates
                Dictionary<Guid, DateTime> maxDatesByCaseId = new();
                if (proceedingData.Any())
                {
                    maxDatesByCaseId = proceedingData
                        .Where(p => p.NextDate.HasValue)
                        .GroupBy(p => p.CaseId)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Max(p => p.NextDate.Value)
                        );
                }

                // Step 3: Base query with assigned/self logic and projection
                var query = _RepoCase.Entites
                    .Include(p => p.CaseProcEntities)
                    .Join(
                        _assignRepo.Entities,
                        caseEntity => caseEntity.Id,
                        assign => assign.CaseId,
                        (caseEntity, assign) => new { Case = caseEntity, Assign = assign }
                    )
                    .Where(x =>
                        request.LinkedIds.Contains(x.Case.CreatedBy) ||
                        request.LinkedIds.Contains(x.Assign.LawyerId.ToString())
                    )
                    .Select(x => new CaseDetailResponse
                    {
                        Id = x.Case.Id,
                        CourtName = x.Case.CourtBench.CourtBench_En,
                        CaseTypeName = x.Case.CaseType.Name_En,
                        FTitleType = x.Case.FTitle.Name_En,
                        STitleType = x.Case.FTitle.Name_En, // Potential typo: should be SecondTitleType?
                        FirstTitle = x.Case.FirstTitle,
                        SecondTitle = x.Case.SecondTitle,
                        CaseStage = x.Case.CaseStage.CaseStage,
                        CaseNumber = x.Case.CaseNo ?? "",
                        CaseYear = x.Case.CaseYear.ToString(),
                        NextHearingDate = x.Case.NextDate ?? default,
                        CaseTitle = x.Case.FirstTitle + " V/S " + x.Case.SecondTitle,
                        IsProceedingDone = x.Case.CaseProcEntities != null &&
                                                     x.Case.CaseProcEntities.Any(s => s.ProceedingDate == request.HearingDate),
                        Reference = request.LinkedIds.Contains(x.Assign.LawyerId.ToString()) ? "Assigned" : "Self"
                    })
                    .AsQueryable();

                // Step 4: Update hearing dates from proceedingData if available
                //IQueryable<CaseDetailResponse> caseDetails = Enumerable.Empty<CaseDetailResponse>().AsQueryable();
                List<CaseDetailResponse> caseDetails;
                if (maxDatesByCaseId.Any())
                {
                    caseDetails = await query.ToListAsync();
                    caseDetails = caseDetails.Select(cd => new CaseDetailResponse
                    {
                        Id = cd.Id,
                        CourtName = cd.CourtName,
                        Abbreviation = cd.Abbreviation,
                        CaseTypeName = cd.CaseTypeName,
                        FTitleType = cd.FTitleType,
                        STitleType = cd.STitleType,
                        FirstTitle = cd.FirstTitle,
                        SecondTitle = cd.SecondTitle,
                        CaseStage = cd.CaseStage,
                        CaseNumber = cd.CaseNumber,
                        CaseYear = cd.CaseYear,
                        CaseTitle = cd.CaseTitle,
                        IsProceedingDone = cd.IsProceedingDone,
                        NextHearingDate = maxDatesByCaseId.ContainsKey(cd.Id)
                            ? maxDatesByCaseId[cd.Id]
                            : cd.NextHearingDate,
                        Reference = cd.Reference
                    }).ToList();
                }
                else
                    caseDetails = query.ToList();

                // Step 5: Filter by hearing date if provided
                if (request.HearingDate != default)
                {
                    var nextDate = request.HearingDate.Date;
                    caseDetails = caseDetails
                        .Where(e => e.NextHearingDate.Date == nextDate)
                        .ToList();
                }
                // Manual paging
                var result = caseDetails
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();
                return Result<List<CaseDetailResponse>>.Success(result);
                // Step 6: Paginate and return
                //return await caseDetails.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            //Expression<Func<CaseDetailEntity, CaseDetailResponse>> expression = e => new CaseDetailResponse
            //{
            //    Id = e.Id,
            //    FTitleType = e.FTitle.Name_En,
            //    STitleType = e.FTitle.Name_En,
            //    CaseTypeName = e.CaseType.Name_En,
            //    CourtType = e.CourtType.CourtType,
            //    CaseNumber = e.CaseNo == null ? "" : e.CaseNo,
            //    CaseYear = e.CaseYear.ToString(),
            //    CourtName = e.CourtBench.CourtBench_En,
            //    FirstTitle = e.FirstTitle,
            //    SecondTitle = e.SecondTitle,
            //    NextHearingDate = e.NextDate.HasValue == true ? e.NextDate.Value : default(DateTime),
            //    CaseStage = e.CaseStage.CaseStage,
            //    CaseTitle = e.FirstTitle + " V/S " + e.SecondTitle,
            //    IsProceedingDone = e.CaseProcEntities != null ?
            //                        e.CaseProcEntities
            //                            .Where(s => s.ProceedingDate == request.HearingDate).Count() > 0
            //                        ? true : false : false
            //};
            //var predicate = PredicateBuilder.True<CaseDetailEntity>();
            //if (predicate != null)
            //{
            //    if (request.LinkedIds.Count > 0)
            //        predicate = predicate.And(c => request.LinkedIds.Contains(c.CreatedBy));
            //    if (request.Year != 0)
            //        predicate = predicate.And(y => y.CaseYear == request.Year);
            //    if (request.CaseNumber != string.Empty)
            //        predicate = predicate.And(x => x.CaseNo == request.CaseNumber);
            //}


            //var td = _RepoCase.Entites
            //    .Include(p => p.CaseProcEntities)                
            //    .Where(predicate)
            //    .Select(expression);

            //var td = _RepoCase.Entites
            //        .Include(p => p.CaseProcEntities)
            //        .Join(
            //            _assignRepo.Entities,
            //            caseEntity => caseEntity.Id,
            //            assign => assign.CaseId,
            //            (caseEntity, assign) => new { Case = caseEntity, Assign = assign }
            //        )
            //        .Where(x =>
            //            request.LinkedIds.Contains(x.Case.CreatedBy) ||
            //            request.LinkedIds.Contains(x.Assign.LawyerId.ToString())
            //        )
            //        .Select(x => x.Case) // keep it IQueryable<CaseDetailEntity>
            //       .AsQueryable()
            //       .Select(expression);




            //IQueryable<CaseProcedingEntity> proceedingData = _RepoProceeding.Entities;
            //if (request.HearingDate! != default(DateTime))
            //    proceedingData = _RepoProceeding.Entities
            //        .Where(n => n.NextDate.Value == request.HearingDate);
            //var td = _RepoCase.Entites
            //        .Include(p => p.CaseProcEntities)
            //        .Join(
            //            _assignRepo.Entities,
            //            caseEntity => caseEntity.Id,
            //            assign => assign.CaseId,
            //            (caseEntity, assign) => new { Case = caseEntity, Assign = assign }
            //        )
            //        .Where(x =>
            //            request.LinkedIds.Contains(x.Case.CreatedBy) ||
            //            request.LinkedIds.Contains(x.Assign.LawyerId.ToString())
            //        )
            //        .Select(x => new CaseDetailResponse
            //        {
            //            Id = x.Case.Id,
            //            FTitleType = x.Case.FTitle.Name_En,
            //            STitleType = x.Case.FTitle.Name_En, // Check this line for potential typo
            //            CaseTypeName = x.Case.CaseType.Name_En,
            //            CourtType = x.Case.CourtType.CourtType,
            //            CaseNumber = x.Case.CaseNo ?? "",
            //            CaseYear = x.Case.CaseYear.ToString(),
            //            CourtName = x.Case.CourtBench.CourtBench_En,
            //            FirstTitle = x.Case.FirstTitle,
            //            SecondTitle = x.Case.SecondTitle,
            //            NextHearingDate = x.Case.NextDate ?? default(DateTime),
            //            CaseStage = x.Case.CaseStage.CaseStage,
            //            CaseTitle = x.Case.FirstTitle + " V/S " + x.Case.SecondTitle,
            //            IsProceedingDone = x.Case.CaseProcEntities != null &&
            //                                x.Case.CaseProcEntities.Any(s => s.ProceedingDate == request.HearingDate),
            //            Reference = request.LinkedIds.Contains(x.Assign.LawyerId.ToString()) ? "Assigned" : "Self"
            //        })
            //        .AsQueryable();
            //IQueryable<CaseDetailResponse> fnldt;
            //if (proceedingData.Count() > 0)
            //{
            //    fnldt = from cd in td
            //            let MaxDt = (from cp in proceedingData
            //                         where cp.CaseId == cd.Id
            //                         orderby cp.NextDate.Value descending
            //                         select cp.NextDate.Value).FirstOrDefault()
            //            select new CaseDetailResponse
            //            {
            //                Id = cd.Id,
            //                CourtName = cd.CourtName,
            //                Abbreviation = cd.Abbreviation,
            //                CaseTypeName = cd.CaseTypeName,
            //                FTitleType = cd.FTitleType,
            //                FirstTitle = cd.FirstTitle,
            //                STitleType = cd.STitleType,
            //                SecondTitle = cd.SecondTitle,
            //                CaseStage = cd.CaseStage,
            //                CaseNumber = cd.CaseNumber,
            //                NextHearingDate = MaxDt != (default) ? MaxDt : cd.NextHearingDate,
            //                CaseTitle = cd.CaseTitle,
            //                CaseYear = cd.CaseYear,
            //                IsProceedingDone = cd.IsProceedingDone
            //            };
            //}
            //else
            //{
            //    fnldt = from cd in td select cd;
            //}

            //var dt = from cd in fnldt
            //         select new CaseDetailResponse
            //         {
            //             Id = cd.Id,
            //             CourtName = cd.CourtName,
            //             Abbreviation = cd.Abbreviation,
            //             CaseTypeName = cd.CaseTypeName,
            //             CaseYear = cd.CaseYear,
            //             FTitleType = cd.FTitleType,
            //             FirstTitle = cd.FirstTitle,
            //             STitleType = cd.STitleType,
            //             SecondTitle = cd.SecondTitle,
            //             CaseStage = cd.CaseStage,
            //             CaseNumber = cd.CaseNumber,
            //             NextHearingDate = cd.NextHearingDate,
            //             CaseTitle = cd.CaseTitle,
            //             IsProceedingDone = cd.IsProceedingDone
            //         };
            //if (request.HearingDate! != default(DateTime))
            //{
            //    var d = from e in dt where e.NextHearingDate.Date == request.HearingDate.Date select e;
            //    return await d.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            //}
            //return await dt.ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
