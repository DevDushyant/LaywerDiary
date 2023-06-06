using System;
using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CaseStages.Query
{
    public class CaseStageCacheAllQuery : IRequest<Result<List<CaseStageCacheAllQueryResponse>>>
    {
        public CaseStageCacheAllQuery()
        {

        }
    }

    public class CaseStageCacheAllQueryHanlder : IRequestHandler<CaseStageCacheAllQuery, Result<List<CaseStageCacheAllQueryResponse>>>
    {
        private readonly ICaseStageCacheRepository _repository;
        private readonly IMapper _mapper;
        public CaseStageCacheAllQueryHanlder(ICaseStageCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<CaseStageCacheAllQueryResponse>>> Handle(CaseStageCacheAllQuery request, CancellationToken cancellationToken)
        {
            var dt = await _repository.GetCachedListAsync();
            var mappeddata = _mapper.Map<List<CaseStageCacheAllQueryResponse>>(dt);
            return Result<List<CaseStageCacheAllQueryResponse>>.Success(mappeddata);
        }
    }
}
