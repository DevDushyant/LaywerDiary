using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KT3Core.Areas.Global.Classes;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Application.Interfaces.Repositories;

namespace CourtApp.Application.Features.Clients.Queries.GetAllCached
{
    public class GetAllClientCachedQuery : IRequest<Result<List<GetAllClientCachedResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
    }

    public class GetAllClientsCachedQueryHandler : IRequestHandler<GetAllClientCachedQuery, Result<List<GetAllClientCachedResponse>>>
    {
        private readonly IClientRepository _Repository;
        private readonly IMapper _mapper;

        public GetAllClientsCachedQueryHandler(IClientRepository _Repository, IMapper mapper)
        {
            this._Repository = _Repository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllClientCachedResponse>>> Handle(GetAllClientCachedQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<ClientEntity>();
            var clientList = await _Repository.GetListAsync();
            var mappedClient = _mapper.Map<List<GetAllClientCachedResponse>>(clientList);
            return Result<List<GetAllClientCachedResponse>>.Success(mappedClient);
        }
    }
}