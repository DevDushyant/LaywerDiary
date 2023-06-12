using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;

namespace CourtApp.Application.Features.Queries.Districts
{
    public class GetDistrictQuery : IRequest<Result<List<GetDistrictResponse>>>
    {
        public string StateCode { get; set; }
    }

    public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, Result<List<GetDistrictResponse>>>
    {

        private readonly IDsitrictMasterCacheRepository _repositoryCache;
        private readonly IMapper _mapper;
        public GetDistrictQueryHandler(IDsitrictMasterCacheRepository _repositoryCache, IMapper _mapper)
        {
            this._repositoryCache = _repositoryCache;
            this._mapper = _mapper;
        }
        public async Task<Result<List<GetDistrictResponse>>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
        {
            request.StateCode = request.StateCode == null ? "" : request.StateCode;
            var list = await _repositoryCache.GetDistrictListByStateAsync(request.StateCode);
            var mappeddata = _mapper.Map<List<GetDistrictResponse>>(list.OrderBy(i => i.DistrictName));
            return Result<List<GetDistrictResponse>>.Success(mappeddata);
        }
    }
}