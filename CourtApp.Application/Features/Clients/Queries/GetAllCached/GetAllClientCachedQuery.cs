using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using KT3Core.Areas.Global.Classes;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Application.Interfaces.Repositories;
using System.Linq.Expressions;
using System;
using System.Linq;
using CourtApp.Application.Extensions;

namespace CourtApp.Application.Features.Clients.Queries.GetAllCached
{
    public class GetAllClientCachedQuery : IRequest<PaginatedResult<GetAllClientCachedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }        
    }

    public class GetAllClientsCachedQueryHandler : IRequestHandler<GetAllClientCachedQuery, PaginatedResult<GetAllClientCachedResponse>>
    {
        private readonly IClientRepository _RepoClient;
        private readonly IMapper _mapper;

        public GetAllClientsCachedQueryHandler(IClientRepository _Repository, IMapper mapper)
        {
            this._RepoClient = _Repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllClientCachedResponse>> Handle(GetAllClientCachedQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ClientEntity, GetAllClientCachedResponse>> expression = e => new GetAllClientCachedResponse
            {
                Id = e.Id,
                Name=String.Concat(e.FirstName," ",e.MiddleName," ",e.LastName),
                Email = e.Email,
                FatherName = e.FatherName,
                Mobile = e.Mobile                
            };
            var predicate = PredicateBuilder.True<ClientEntity>();
            var paginatedList = await _RepoClient.Clients                   
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}