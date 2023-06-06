using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
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

namespace CourtApp.Application.Features.CourtFeeStructure.Queries
{
    public class GetCourtFeeStructAllQuery : IRequest<Result<List<GetAllCourtFeeStructureResponse>>>
    {
        public string StateCode { get; set; }
    }

    public class GetCourtFeeStructAllQueryHandler : IRequestHandler<GetCourtFeeStructAllQuery, Result<List<GetAllCourtFeeStructureResponse>>>
    {
        private readonly ICourtFeeStructureRepository _repository;
        private readonly IMapper mapper;
        public GetCourtFeeStructAllQueryHandler(ICourtFeeStructureRepository _repository, IMapper mapper)
        {
            this.mapper = mapper;
            this._repository = _repository;
        }
        public async Task<Result<List<GetAllCourtFeeStructureResponse>>>Handle(GetCourtFeeStructAllQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtFeeStructureEntity, GetAllCourtFeeStructureResponse>> expression = e => new GetAllCourtFeeStructureResponse
            {
                Id = e.Id,
                StateName = e.State.StateName,
                MaxValue=e.MaxValue,
                MinValue=e.MinValue,
                Rate=e.Rate,
                FixAmount=e.FixAmount

            };
            var predicate = PredicateBuilder.True<CourtFeeStructureEntity>();
            if (request.StateCode != null)
                predicate = predicate.And(b => b.StateCode == request.StateCode);

            var datalist =  _repository.Entites;
            var dtlist = datalist.Where(predicate)
               .Select(expression)
               .ToList();
            return Result<List<GetAllCourtFeeStructureResponse>>.Success(dtlist);
        }
    }
}
