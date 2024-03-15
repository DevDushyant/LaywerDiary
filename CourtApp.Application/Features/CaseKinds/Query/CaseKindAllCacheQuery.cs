﻿using System;
using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Linq;
using CourtApp.Application.Interfaces.Repositories;

namespace CourtApp.Application.Features.CaseKinds.Query
{
    public class CaseKindAllCacheQuery : IRequest<Result<List<CaseKindCacheQueryResponse>>>
    {
        public Guid CourtTypeId { get; set; }       
    }

    public class CaseKindAllCacheQueryCommandHandler : IRequestHandler<CaseKindAllCacheQuery, Result<List<CaseKindCacheQueryResponse>>>
    {
        private readonly ICaseKindRepository _repository;
        private readonly IMapper _mapper;

        public CaseKindAllCacheQueryCommandHandler(ICaseKindRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public  Task<Result<List<CaseKindCacheQueryResponse>>> Handle(CaseKindAllCacheQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseKindEntity, CaseKindCacheQueryResponse>> expression = e => new CaseKindCacheQueryResponse
            {
                Id=e.Id,
                CourtType = e.CourtType.CourtType,
                CaseKind = e.CaseKind
            };
            var predicate = PredicateBuilder.True<CaseKindEntity>();
           
            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtType.Id == request.CourtTypeId);

            var data = _repository.QryEntities.Where(predicate).Select(expression).ToList();
            var mappeddata = _mapper.Map<List<CaseKindCacheQueryResponse>>(data);
            return Task.FromResult(Result<List<CaseKindCacheQueryResponse>>.Success(mappeddata));
        }
    }
}
