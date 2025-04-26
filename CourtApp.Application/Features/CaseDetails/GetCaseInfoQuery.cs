using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public List<string> LinkedIds { get; set; }
    }

    public class GetCaseInfoQueryHandler : IRequestHandler<GetCaseInfoQuery, PaginatedResult<GetCaseInfoDto>>
    {
        private readonly IUserCaseRepository _repository;
        private readonly ICaseProceedingRepository _ProcRepo;
        private readonly ICaseAssignedRepository _assignRepo;
        public GetCaseInfoQueryHandler(IUserCaseRepository _repository,
            ICaseProceedingRepository procRepo,
            ICaseAssignedRepository assignRepo)
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
                if (request.LinkedIds.Count > 0)
                    predicate = predicate.And(c => request.LinkedIds.Contains(c.CreatedBy));
                if (request.Year != 0)
                    predicate = predicate.And(y => y.CaseYear == request.Year);
                if (request.CaseNumber != null && request.CaseNumber != string.Empty)
                    predicate = predicate.And(x => x.CaseNo == request.CaseNumber);
            }
            try
            {
                var casesQuery = (from c in _repository.Entites
                                  join ac in _assignRepo.Entities
                                  on c.Id equals ac.CaseId into caseAssignments
                                  from ac in caseAssignments.DefaultIfEmpty()
                                  where request.LinkedIds.Contains(c.CreatedBy)
                                  || request.LinkedIds.Contains(ac.LawyerId.ToString()) // Check if user is the creator or assigned lawyer
                                  let asignedOrSelf = ac != null && request.LinkedIds.Contains(ac.LawyerId.ToString()) ? "Assigned" : "Self"
                                  let isCaseAssigned = asignedOrSelf == "Self" && ac != null && ac.CaseId == c.Id
                                  let AssignedLawyerId = asignedOrSelf == "Self" && ac != null ? ac.LawyerId : Guid.Empty
                                  select new GetCaseInfoDto
                                  {
                                      Id = c.Id,
                                      Reference = asignedOrSelf,
                                      IsCaseAssigned = isCaseAssigned,
                                      LawyerId=AssignedLawyerId,
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
                   .AsQueryable();
                // Step 2: Fetch all the data into memory
                var allCases = await casesQuery.ToListAsync();

                // Step 3: Perform the distinct operation in memory (client-side)
                var distinctCases = allCases
                    .DistinctBy(c => c.Id)  // Distinct by CaseId (Id) in memory
                    .ToList();

                // Step 4: Apply pagination client-side
                var paginatedCases = distinctCases
                    .Skip((request.PageNumber - 1) * request.PageSize) // Skip the records for pagination
                    .Take(request.PageSize) // Take the records for pagination
                    .ToList();

                // Step 5: Set the total count (distinct count)
                var totalCount = distinctCases.Count;

                // Step 6: Calculate total pages
                int totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

                // Step 7: Return the paginated result wrapped in PaginatedResult<GetCaseInfoDto>
                var result = PaginatedResult<GetCaseInfoDto>.Success(paginatedCases, totalCount, request.PageNumber, request.PageSize);
                return result;

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
    }
}
