using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Queries.GetById
{
    public class GetClientByIdQuery : IRequest<Result<GetClientByIdResponse>>
    {
        public int Id { get; set; }

        public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Result<GetClientByIdResponse>>
        {
            private readonly IClientCacheRepository _clientCache;
            private readonly IMapper _mapper;

            public GetClientByIdQueryHandler(IClientCacheRepository clientCache, IMapper mapper)
            {
                _clientCache = clientCache;
                _mapper = mapper;
            }

            public async Task<Result<GetClientByIdResponse>> Handle(GetClientByIdQuery query, CancellationToken cancellationToken)
            {
                var client = await _clientCache.GetByIdAsync(query.Id);
                var mappedClient = _mapper.Map<GetClientByIdResponse>(client);
                return Result<GetClientByIdResponse>.Success(mappedClient);
            }
        }
    }
}