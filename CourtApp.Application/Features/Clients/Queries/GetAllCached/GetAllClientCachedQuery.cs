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
using Microsoft.EntityFrameworkCore;

namespace CourtApp.Application.Features.Clients.Queries.GetAllCached
{
    public class GetAllClientCachedQuery : IRequest<PaginatedResult<GetAllClientCachedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
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
                Name = e.Name,
                Email = e.Email,
                Mobile = e.Mobile,
                Councel = e.OppositCounselId != Guid.Empty ? (e.OppositCounsel.FirstName + " " + e.OppositCounsel.LastName) : "",
                OffEmail = e.OfficeEmail,
                Appearence = e.Appearence.Name_En,
                Address = e.Address
            };
            var predicate = PredicateBuilder.True<ClientEntity>();
            var paginatedList = await
                _RepoClient
                .Clients
                .Include(a => a.Appearence)
                .Where(w => w.CreatedBy.Equals(request.UserId))
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}