using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CourtApp.Application.Features.CaseDetails
{
    public class GetCaseInfoQuery : IRequest<PaginatedResult<GetCaseInfoDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string CaseNumber { get; set; }
        public int Year { get; set; }
        public string UserId { get; set; }
    }

    public class GetCaseInfoQueryHandler : IRequestHandler<GetCaseInfoQuery, PaginatedResult<GetCaseInfoDto>>
    {
        private readonly IUserCaseRepository _repository;
        private readonly ICaseProceedingRepository _ProcRepo;
        private readonly ICaseAssignedRepository _assignRepo;
        public GetCaseInfoQueryHandler(IUserCaseRepository _repository, ICaseProceedingRepository procRepo, ICaseAssignedRepository assignRepo)
        {
            this._repository = _repository;
            _ProcRepo = procRepo;
            _assignRepo = assignRepo;
        }
        public async Task<PaginatedResult<GetCaseInfoDto>> Handle(GetCaseInfoQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                if (request.UserId != null)
                    predicate = predicate.And(c => c.CreatedBy.Equals(request.UserId));
                if (request.Year != 0)
                    predicate = predicate.And(y => y.CaseYear == request.Year);
                if (request.CaseNumber != null && request.CaseNumber != string.Empty)
                    predicate = predicate.And(x => x.CaseNo == request.CaseNumber);
            }
            try
            {
                //var cases = await _repository.Entites
                //            .Include(c => c.CourtType)
                //            .Include(c => c.CaseType)
                //            .Include(c => c.CaseStage)
                //            .Include(c => c.CourtBench)
                //            .Where(c => c.CreatedBy == request.UserId)
                //            .Select(e => new GetCaseInfoDto
                //            {
                //                Id = e.Id,
                //                Reference = "Self",
                //                No = e.CaseNo,
                //                Year = e.CaseYear.ToString(),
                //                CourtType = e.CourtType.CourtType.ToString(),
                //                CaseType = e.CaseType.Name_En,
                //                Court = e.CourtBench.CourtBench_En,
                //                CaseStage = e.CaseStage.CaseStage,
                //                DisposalDate = e.DisposalDate,
                //                CaseDetail = e.FirstTitle + " V/S " + e.SecondTitle,
                //                NextDate = e.CaseProcEntities
                //                            .OrderByDescending(o => o.NextDate.Value) // Order by latest date
                //                            .Select(s => s.NextDate.Value.ToString("dd-MM-yyyy"))
                //                            .FirstOrDefault() ??
                //                            (e.NextDate.HasValue ? e.NextDate.Value.ToString("dd-MM-yyyy") : "")
                //            })
                //            .OrderByDescending(o => o.Year)
                //            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                //return cases;
                //var userCases = _repository.Entites
                //            .Include(c => c.CourtType)
                //            .Include(c => c.CaseType)
                //            .Include(c => c.CaseStage)
                //            .Include(c => c.CourtBench)
                //            .Where(c => c.CreatedBy == request.UserId);
                var cases = await (from c in _repository.Entites
                                   join ac in _assignRepo.Entities
                                   on c.Id equals ac.CaseId into caseAssignments
                                   from ac in caseAssignments.DefaultIfEmpty() // Left join, to include cases without an assignment
                                   where c.CreatedBy == request.UserId || ac.LawyerId == Guid.Parse(request.UserId) // Check if user is the creator or assigned lawyer
                                   select new GetCaseInfoDto
                                   {
                                       Id = c.Id,
                                       Reference = ac != null && ac.LawyerId == Guid.Parse(request.UserId) ? "Assigned" : "Self", // Reference is "Assigned" if LawyerId matches
                                       No = c.CaseNo,
                                       Year = c.CaseYear.ToString(),
                                       CourtType = c.CourtType.CourtType.ToString(),
                                       CaseType = c.CaseType.Name_En,
                                       Court = c.CourtBench.CourtBench_En,
                                       CaseStage = c.CaseStage.CaseStage,
                                       DisposalDate = c.DisposalDate,
                                       CaseDetail = c.FirstTitle + " V/S " + c.SecondTitle,
                                       NextDate = c.CaseProcEntities
                                           .OrderByDescending(o => o.NextDate.Value) // Order by latest date
                                           .Select(s => s.NextDate.Value.ToString("dd-MM-yyyy"))
                                           .FirstOrDefault() ?? (c.NextDate.HasValue ? c.NextDate.Value.ToString("dd-MM-yyyy") : "")
                                   })
                   .OrderByDescending(o => o.Year)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return cases;

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
    }
}
