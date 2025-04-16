using AspNetCoreHero.Results;
using AutoMapper;
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
using System.Threading;
using System.Threading.Tasks;


namespace CourtApp.Application.Features.Registers
{
    public class DisposalRegisterQuery : IRequest<PaginatedResult<DisposalRegisterResponse>>
    {
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<string> LinkedIds { get; set; }
        //public string UserId { get; set; }
    }

    public class RegistDisposalQueryHandler : IRequestHandler<DisposalRegisterQuery, PaginatedResult<DisposalRegisterResponse>>
    {

        private readonly IMapper _mapper;
        private readonly ICaseProceedingRepository _wRepo;
        public RegistDisposalQueryHandler(IMapper _mapper, ICaseProceedingRepository _wRepo)
        {
            this._mapper = _mapper;
            this._wRepo = _wRepo;
        }
        public Task<PaginatedResult<DisposalRegisterResponse>> Handle(DisposalRegisterQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<CaseProcedingEntity>();
            if (request.LinkedIds.Count > 0)
                predicate = predicate.And(c => request.LinkedIds.Contains(c.CreatedBy));

            DateTime to = DateTime.Now;
            predicate = predicate.And(w => w.Head.Abbreviation == "DISP");
            var fndt = _wRepo
                .Entities
                .Include(h => h.Head)
                .Where(predicate)
                .Select(e => new DisposalRegisterResponse
                {
                    DisposalDate = e.ProceedingDate != null ? e.ProceedingDate.Value.ToString("dd-MM-yyyy") : "",
                    Id = e.CaseId,
                    FirstTitle = e.Case.FirstTitle,
                    SecondTitle = e.Case.SecondTitle,
                    No = e.Case.CaseNo,
                    Year = e.Case.CaseYear.ToString(),
                    Court = e.Case.CourtBench.CourtBench_En,
                    CaseType = e.Case.CaseType.Name_En,
                    Reason = e.SubHead.Name_En
                })
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return fndt;
        }
    }
}
