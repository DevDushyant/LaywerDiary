using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters
{
    public class GetCourtMasterAllQuery : IRequest<Result<List<GetCourtMasterDataAllResponse>>>
    {
        public Guid CourtTypeId { get; set; }
        public GetCourtMasterAllQuery()
        {

        }
    }

    internal class GetCourtMasterAllQueryHandler : IRequestHandler<GetCourtMasterAllQuery, Result<List<GetCourtMasterDataAllResponse>>>
    {
        private readonly ICourtMasterRepository _repository;
        public GetCourtMasterAllQueryHandler(ICourtMasterRepository _repository)
        {
            this._repository = _repository;
        }
        public Task<Result<List<GetCourtMasterDataAllResponse>>> Handle(GetCourtMasterAllQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtMasterEntity, GetCourtMasterDataAllResponse>> expression = e => new GetCourtMasterDataAllResponse
            {
                CourtType = e.CourtType.CourtType,
                CourtName = e.Name_En,
                Id = e.Id,
                CourtFullName = e.Name_En,
                State = e.State.Name_En,
                District = e.District.Name_En
            };
            var predicate = PredicateBuilder.True<CourtMasterEntity>();
            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtType.Id == request.CourtTypeId);


            var dtlist = _repository.Entities.Where(predicate)
               .Select(expression)
               .ToList();

            return Task.FromResult(Result<List<GetCourtMasterDataAllResponse>>.Success(dtlist));
        }
    }
}
