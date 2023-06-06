using AspNetCoreHero.Results;
using CourtApp.Application.Constants;
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

namespace CourtApp.Application.Features.CourtMasters.Query
{
    public class GetCourtMasterAllQuery : IRequest<Result<List<GetCourtMasterDataAllResponse>>>
    {
        public int CourtTypeId { get; set; }
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
                CourtName = e.CourtName,
                UniqueId = e.Id,
                Bench = e.Bench,
                CourtFullName = e.CourtName + "(" + e.Bench + ")",
                State = e.State.StateName,
                District = e.District.DistrictName
            };
            var predicate = PredicateBuilder.True<CourtMasterEntity>();
            if (request.CourtTypeId != 0)
                predicate = predicate.And(b => b.CourtTypeId == request.CourtTypeId);
            

            var dtlist = _repository.QryEntities.Where(predicate)
               .Select(expression)
               .ToList();

            return Task.FromResult(Result<List<GetCourtMasterDataAllResponse>>.Success(dtlist));
        }
    }
}
