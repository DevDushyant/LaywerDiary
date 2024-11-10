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
using System.Linq;
using System.Linq.Expressions;
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
        public string UserId { get; set; }
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
            if (predicate != null)
                predicate = predicate.And(c => c.CreatedBy.Equals(request.UserId));

            DateTime to = DateTime.Now;
            //if (request.ToDt != default(DateTime)) to = request.ToDt;
            //if (request.FromDt != default(DateTime))
            //  predicate = predicate.And(b => b.LastModifiedOn.Value >= request.FromDt && b.LastModifiedOn.Value <= to);
            predicate = predicate.And(w => w.Head.Abbreviation == "DISP");           
            var fndt = _wRepo
                .Entities
                .Include(h => h.Head)
                .Where(predicate)
                .Select(e => new DisposalRegisterResponse
                {
                    DisposalDate = e.ProceedingDate != null ? e.ProceedingDate.Value.ToString("dd-MM-yyyy") : "",
                    Id = e.CaseId,
                    Title = e.Case.FirstTitle + "Vs" + e.Case.SecondTitle,
                    Reason = e.SubHead.Name_En
                })
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return fndt;
        }
    }
}
