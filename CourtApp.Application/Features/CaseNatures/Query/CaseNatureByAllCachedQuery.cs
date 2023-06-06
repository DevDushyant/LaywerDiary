using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CaseNatures.Query
{
    public class CaseNatureByAllCachedQuery : IRequest<Result<List<CaseNatureAllCachedResponse>>>
    {
        public CaseNatureByAllCachedQuery()
        {

        }
    }

    public class CaseNatureByAllCachedQueryCommandHandler : IRequestHandler<CaseNatureByAllCachedQuery, Result<List<CaseNatureAllCachedResponse>>>
    {
        private readonly ICaseNatureCacheRepository caseNatureCacheRepository;
        private readonly IMapper _mapper;

        public CaseNatureByAllCachedQueryCommandHandler(ICaseNatureCacheRepository caseNatureCacheRepository, IMapper _mapper)
        {
            this.caseNatureCacheRepository = caseNatureCacheRepository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<CaseNatureAllCachedResponse>>> Handle(CaseNatureByAllCachedQuery request, CancellationToken cancellationToken)
        {
            var dt = await caseNatureCacheRepository.GetCachedListAsync();
            var mappeddata = _mapper.Map<List<CaseNatureAllCachedResponse>>(dt);
            return Result<List<CaseNatureAllCachedResponse>>.Success(mappeddata);
        }
    }
}
