using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtFeeStructure.Queries
{
    public class GetCourtFeeStructAllQuery : IRequest<PaginatedResult<GetAllCourtFeeStructureResponse>>
    {
        public int StateCode { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetCourtFeeStructAllQueryHandler : IRequestHandler<GetCourtFeeStructAllQuery, PaginatedResult<GetAllCourtFeeStructureResponse>>
    {
        private readonly ICourtFeeStructureRepository _repository;
        private readonly IMapper mapper;
        public GetCourtFeeStructAllQueryHandler(ICourtFeeStructureRepository _repository, IMapper mapper)
        {
            this.mapper = mapper;
            this._repository = _repository;
        }
        public async Task<PaginatedResult<GetAllCourtFeeStructureResponse>>Handle(GetCourtFeeStructAllQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtFeeStructureEntity, GetAllCourtFeeStructureResponse>> expression = e => new GetAllCourtFeeStructureResponse
            {
                Id = e.Id,              
                StateName = e.State.Name_En,
                MaxValue=e.MaxValue,
                MinValue=e.MinValue,
                Rate=e.Rate,
                FixAmount=e.FixAmount

            };
            var predicate = PredicateBuilder.True<CourtFeeStructureEntity>();
            if (request.StateCode != 0)
                predicate = predicate.And(b => b.State.Code == request.StateCode);

            var datalist =  _repository.Entites;
            var dtlist =await datalist.Where(predicate)
               .Select(expression)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return dtlist;
        }
    }
}
