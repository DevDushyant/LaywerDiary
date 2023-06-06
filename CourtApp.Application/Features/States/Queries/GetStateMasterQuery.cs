using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace CourtApp.Application.Features.States.Queries
{
    public class GetStateMasterQuery: IRequest<Result<List<GetStateMasterResponse>>>
    {
        public GetStateMasterQuery()
        {
            
        }
    }
    public class GetStateMasterQueryHandler : IRequestHandler<GetStateMasterQuery, Result<List<GetStateMasterResponse>>>
    {
        private readonly IStateMasterCacheRepository _repositoryCache;
        private readonly IMapper _mapper;
        public GetStateMasterQueryHandler(IStateMasterCacheRepository _repositoryCache,IMapper _mapper)
        {
            this._repositoryCache=_repositoryCache;
            this._mapper=_mapper;
        }
        public async Task<Result<List<GetStateMasterResponse>>> Handle(GetStateMasterQuery request, CancellationToken cancellationToken)
        {
            var list = await _repositoryCache.GetStateListAsync();
            var mappeddata = _mapper.Map<List<GetStateMasterResponse>>(list.OrderBy(s=>s.StateName));
            return Result<List<GetStateMasterResponse>>.Success(mappeddata);
        }
    }

}