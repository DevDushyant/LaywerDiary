using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Query
{
    public class GetCourtTypeQuery : IRequest<Result<List<GetCourtTypeResponse>>>
    {
        
    }
    public class GetCourtTypeQueryQueryHandler :IRequestHandler<GetCourtTypeQuery, Result<List<GetCourtTypeResponse>>>
    {
        private readonly ICourtTypeCacheRepository _courtType;
        private readonly IMapper _mapper;

        public GetCourtTypeQueryQueryHandler(ICourtTypeCacheRepository _CourtType, IMapper mapper)
        {
            this._courtType = _CourtType;
            _mapper = mapper;
        }

        public async Task<Result<List<GetCourtTypeResponse>>> Handle(GetCourtTypeQuery request, CancellationToken cancellationToken)
        {
            var courtTypeList = await _courtType.GetCachedListAsync();
            var mappedCourtTpe = _mapper.Map<List<GetCourtTypeResponse>>(courtTypeList);

            return Result<List<GetCourtTypeResponse>>.Success(mappedCourtTpe);
        }
    }
}
