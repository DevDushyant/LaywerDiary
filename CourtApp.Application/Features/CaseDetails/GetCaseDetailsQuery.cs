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
            /*
            Expression<Func<CaseDetailEntity, CaseDetailResponse>> expression = e => new CaseDetailResponse
            {
                Id = e.Id,
                FTitleType = e.FTitle.Name_En,
                STitleType = e.FTitle.Name_En,
                CaseTypeName = e.CaseType.Name_En,
                CourtType = e.CourtType.CourtType,
                CaseNumber = e.CaseNo == null ? "" : e.CaseNo,
                CaseYear = e.CaseYear.ToString(),
                CourtName = e.CourtBench.CourtBench_En,
                FirstTitle = e.FirstTitle,
                SecondTitle = e.SecondTitle,               
                NextHearingDate = e.NextDate.HasValue == true ? e.NextDate.Value : default(DateTime),
                CaseStage = e.CaseStage.CaseStage,
                CaseTitle = e.FirstTitle + " V/S " + e.SecondTitle,
                IsProceedingDone = e.CaseProcEntities != null ?
                                    e.CaseProcEntities
                                        .Where(s => s.ProceedingDate == request.HearingDate).Count() > 0
                                    ? true : false : false
            };*/
            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                if (request.LinkedIds.Count > 0)
                    predicate = predicate.And(u => request.LinkedIds.Contains(u.CreatedBy));
                if (request.Year != 0)
                    predicate = predicate.And(y => y.CaseYear == request.Year);
                if (request.CaseNumber != string.Empty)
                    predicate = predicate.And(x => x.CaseNo == request.CaseNumber);
            }

            try
            {
                IQueryable<CaseProcedingEntity> proceedingData = null;
                if (request.HearingDate! != default(DateTime))
                    proceedingData = _RepoProceeding.Entities
                        .Where(n => n.ProceedingDate.Value == request.HearingDate);
                /*
                var td = _RepoCase.Entites
                    .Include(p => p.CaseProcEntities)                
                    .Where(predicate)
                    .Select(expression);*/

                var td = (from c in _RepoCase.Entites
                          join ac in _assignRepo.Entities
                          on c.Id equals ac.CaseId into caseAssignments
                          from ac in caseAssignments.DefaultIfEmpty()
                          where request.LinkedIds.Contains(c.CreatedBy)
                          || request.LinkedIds.Contains(ac.LawyerId.ToString()) // Check if user is the creator or assigned lawyer
                          select new CaseDetailResponse
                          {
                              Id = c.Id,
                              Reference = ac != null && request.LinkedIds.Contains(ac.LawyerId.ToString()) ? "Assigned" : "Self", // Reference is "Assigned" if LawyerId matches
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
                              //DisposalDate = c.DisposalDate,                                  
                              CaseTitle = (c.FirstTitle + " V/S " + c.SecondTitle + " [" +
                                            (string.IsNullOrEmpty(c.CaseNo) ? c.CaseYear.ToString() : c.CaseNo + "/" + c.CaseYear.ToString()) +
                                            "]").ToUpperInvariant(),
                              NextHearingDate = c.CaseProcEntities
                                                .OrderByDescending(o => o.NextDate)
                                                .Select(s => s.NextDate)
                                                .FirstOrDefault() ?? (c.NextDate.HasValue ? c.NextDate.Value : default(DateTime)),
                              IsProceedingDone = c.CaseProcEntities != null &&
                                                 c.CaseProcEntities.Any(s => s.ProceedingDate == request.HearingDate),

                              //NextHearingDate = c.CaseProcEntities
                              //    .OrderByDescending(o => o.NextDate.Value) // Order by latest date
                              //    .Select(s => s.NextDate.Value.ToString("dd-MM-yyyy"))
                              //    .FirstOrDefault() ?? (c.NextDate.HasValue ? c.NextDate.Value.ToString("dd-MM-yyyy") : (default))
                          })
                       .OrderByDescending(o => o.CaseYear)
                       .AsQueryable();

                IQueryable<CaseDetailResponse> fnldt;
                if (proceedingData.Count() > 0)
                {
                    fnldt = from cd in td.Where(w => w.IsProceedingDone == true)
                            let MaxDt = (from cp in proceedingData
                                         where cp.CaseId == cd.Id
                                         orderby cp.NextDate.Value descending
                                         select cp.NextDate.Value).FirstOrDefault()
                            select new CaseDetailResponse
                            {
                                Id = cd.Id,
                                CourtName = cd.CourtName,
                                CaseTypeName = cd.CaseTypeName,
                                FTitleType = cd.FTitleType,
                                FirstTitle = cd.FirstTitle,
                                STitleType = cd.FTitleType,
                                SecondTitle = cd.FTitleType,
                                CaseStage = cd.CaseStage,
                                CaseNumber = cd.CaseNumber,
                                NextHearingDate = MaxDt != (default) ? MaxDt : cd.NextHearingDate,
                                CaseTitle = cd.CaseTitle,
                                CaseYear = cd.CaseYear,
                                Reference = cd.Reference,
                                IsProceedingDone = cd.IsProceedingDone
                            };
                    return await fnldt.ToPaginatedListAsync(request.PageNumber, request.PageSize);
                }
                else
                {
                    fnldt = from cd in td select cd;
                    var dt = from cd in fnldt
                             select new CaseDetailResponse
                             {
                                 Id = cd.Id,
                                 CourtName = cd.CourtName,
                                 Abbreviation = cd.Abbreviation,
                                 CaseTypeName = cd.CaseTypeName,
                                 CaseYear = cd.CaseYear,
                                 FTitleType = cd.FTitleType,
                                 FirstTitle = cd.FirstTitle,
                                 STitleType = cd.STitleType,
                                 SecondTitle = cd.SecondTitle,
                                 CaseStage = cd.CaseStage,
                                 CaseNumber = cd.CaseNumber,
                                 NextHearingDate = cd.NextHearingDate,
                                 CaseTitle = cd.CaseTitle,
                                 IsProceedingDone = cd.IsProceedingDone,
                                 Reference = cd.Reference
                             };
                    if (request.HearingDate! != default(DateTime))
                    {
                        var d = from e in dt where e.NextHearingDate.Date == request.HearingDate.Date select e;
                        return await d.ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    }
                }
                return await td.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
