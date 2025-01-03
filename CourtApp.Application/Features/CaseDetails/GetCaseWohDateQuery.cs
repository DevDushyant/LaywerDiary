using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class GetCaseWohDateQuery : IRequest<PaginatedResult<GetCaseInfoDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
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
            try
            {
                var cases = await _repository
                    .Entites
                    .Include(ct => ct.CourtType)
                    .Include(ct => ct.CaseType)
                    .Include(cs => cs.CaseStage)
                    .Include(c => c.CourtBench)
                    .Where(c => c.CreatedBy.Equals(request.UserId) && c.NextDate==null)
                    .Select(e => new GetCaseInfoDto
                    {
                        Id = e.Id,
                        No = e.CaseNo,
                        Year = e.CaseYear.ToString(),
                        CaseType = e.CaseType.Name_En,
                        Court = e.CourtBench.CourtBench_En,
                        CaseStage = e.CaseStage.CaseStage,
                        DisposalDate = e.DisposalDate,
                        CaseDetail = e.FirstTitle + " V/S " + e.SecondTitle,
                        NextDate = ""
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
