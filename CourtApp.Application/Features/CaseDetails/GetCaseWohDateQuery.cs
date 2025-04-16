using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class GetCaseWohDateQuery : IRequest<PaginatedResult<GetCaseInfoDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<string> LinkedIds { get; set; }
    }
    public class GetCaseWohDateQueryHandler : IRequestHandler<GetCaseWohDateQuery, PaginatedResult<GetCaseInfoDto>>
    {
        private readonly IUserCaseRepository _repository;
        public GetCaseWohDateQueryHandler(IUserCaseRepository _repository)
        {
            this._repository = _repository;
        }
        public async Task<PaginatedResult<GetCaseInfoDto>> Handle(GetCaseWohDateQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.Today.Date;
            var cases = await _repository.Entites
                        .Include(c => c.CourtType)
                        .Include(c => c.CaseType)
                        .Include(c => c.CaseStage)
                        .Include(c => c.CourtBench)
                        .Where(c => request.LinkedIds.Contains(c.CreatedBy) && c.DisposalDate == null)
                        .Select(e => new
                        {
                            Case = e,
                            LatestNextDate = e.CaseProcEntities
                                                .OrderByDescending(o => o.NextDate.Value)
                                                .Select(s => (DateTime?)s.NextDate.Value)
                                                .FirstOrDefault()
                        })
                        .Where(x => !x.Case.NextDate.HasValue // if next date is not present.
                                    || (x.Case.NextDate.HasValue
                                            && x.LatestNextDate.HasValue
                                            && x.LatestNextDate.Value > x.Case.NextDate.Value
                                            ? x.LatestNextDate.Value : x.Case.NextDate.Value) < today

                              )
                        .Select(x => new GetCaseInfoDto
                        {
                            Id = x.Case.Id,
                            No = x.Case.CaseNo,
                            Year = x.Case.CaseYear.ToString(),
                            CaseType = x.Case.CaseType.Name_En,
                            Court = x.Case.CourtBench.CourtBench_En,
                            CaseStage = x.Case.CaseStage.CaseStage,
                            DisposalDate = x.Case.DisposalDate,
                            CaseDetail = x.Case.FirstTitle + " V/S " + x.Case.SecondTitle,
                            NextDate = x.LatestNextDate.HasValue
                                        ? x.LatestNextDate.Value.ToString()
                                        : (x.Case.NextDate.HasValue ?
                                            x.Case.NextDate.Value.ToString() : "")
                        })
                        .OrderByDescending(o => o.Year)
                        .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return cases;
        }
    }
}
