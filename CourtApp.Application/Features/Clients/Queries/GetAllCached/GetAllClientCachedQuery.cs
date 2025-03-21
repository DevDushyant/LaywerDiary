﻿using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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
                Name = e.Name.ToUpper(),
                Email = e.Email,
                Mobile = e.Mobile,
                ClientType = e.ClientType,
                ReferalBy = e.ReferalBy,
                Address = e.Address.ToUpper()
            };
            var predicate = PredicateBuilder.True<ClientEntity>();
            var paginatedList = await
                _RepoClient
                .Clients
                .Where(w => w.CreatedBy.Equals(request.UserId))
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}