using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters
{
    public class GetCourtBenchQuery : IRequest<PaginatedResult<CourtBenchResponse>>
    {
        public GetCourtBenchQuery(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
        public int StateId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetCourtBenchQueryHandler : IRequestHandler<GetCourtBenchQuery, PaginatedResult<CourtBenchResponse>>
    {
        private readonly ICourtBenchRepository _Repository;
        private readonly ICourtMasterRepository _CourtMasterRepo;
        public GetCourtBenchQueryHandler(ICourtBenchRepository _Repository, ICourtMasterRepository _CourtMasterRepo)
        {
            this._Repository = _Repository;
            this._CourtMasterRepo = _CourtMasterRepo;
        }
        public Task<PaginatedResult<CourtBenchResponse>> Handle(GetCourtBenchQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtBenchEntity, CourtBenchResponse>> expression = e => new CourtBenchResponse
            {
                Id = e.Id,
                Address = e.Address,
                CourtBench_En = e.CourtBench_En,
                CourtBench_Hn = e.CourtBench_Hn
            };
            var predicate = PredicateBuilder.True<CourtBenchEntity>();
            Guid CourtMasterId = Guid.Empty;
            var dt = _CourtMasterRepo.GetListAsync().Result
                .Where(w => w.StateId == request.StateId && w.CourtTypeId == request.CourtTypeId).ToList();
            if (request.CourtTypeId != Guid.Empty && request.CourtId != Guid.Empty)
                CourtMasterId = dt
                    .Select(s => s.Id).FirstOrDefault();
            else
                CourtMasterId = dt.Select(s => s.Id).FirstOrDefault();
            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtMasterId == CourtMasterId);

            var dtlist = _Repository.Entities.Where(predicate)
               .Select(expression).
               ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return dtlist;

        }
    }
}
