using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    public class GetCourtComplexCacheQuery : IRequest<Result<List<CourtComplexResponse>>>
    {
        public int DistrictId { get; set; }
        public Guid CourtDistrictId { get; set; }
    }
    public class GetCourtComplexCacheQueryHandler : IRequestHandler<GetCourtComplexCacheQuery, Result<List<CourtComplexResponse>>>
    {
        private readonly ICourtComplexCacheRepository _cacheRepository;
        private readonly IMapper _mapper;
        public GetCourtComplexCacheQueryHandler(ICourtComplexCacheRepository _cacheRepository, IMapper _mapper)
        {
            this._cacheRepository = _cacheRepository;
            this._mapper = _mapper;

        }
        public async Task<Result<List<CourtComplexResponse>>> Handle(GetCourtComplexCacheQuery request, CancellationToken cancellationToken)
        {
            var dl = await _cacheRepository.GetCachedListAsync();
            var rs = dl.Where(w => w.CourtDistrictId == request.CourtDistrictId)
                .OrderBy(o => o.Name_En).ToList();
            var mappedDt = _mapper.Map<List<CourtComplexResponse>>(rs);
            return Result<List<CourtComplexResponse>>.Success(mappedDt);
        }
    }
}
