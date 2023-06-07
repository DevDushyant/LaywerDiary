using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Features.BookMasters.Queries;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Queries.Cases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Queries.LawyerMaster
{

    public class HandlerQueryLawyerGetAll : IRequestHandler<QueryGetAllLawyerEntity, Result<List<ResponseGetAllLawyer>>>
    {
        private readonly ILawyerCacheRepository _Repository;
        private readonly IMapper _mapper;
        public HandlerQueryLawyerGetAll(ILawyerCacheRepository _Repository, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
        }
        public async Task<Result<List<ResponseGetAllLawyer>>> Handle(QueryGetAllLawyerEntity request, CancellationToken cancellationToken)
        {
            var bookTypeList = await _Repository.GetCachedListAsync();
            var mappedBookTye = _mapper.Map<List<ResponseGetAllLawyer>>(bookTypeList);
            return Result<List<ResponseGetAllLawyer>>.Success(mappedBookTye);
        }
    }
}
