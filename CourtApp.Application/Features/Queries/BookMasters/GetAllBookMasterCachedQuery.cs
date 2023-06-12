using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.BookMasters
{
    public class GetAllBookMasterCachedQuery : IRequest<Result<List<GetAllBookMasterCacheResponse>>>
    {
        public int BookTypeId { get; set; }
        public int PublisherId { get; set; }
        public GetAllBookMasterCachedQuery()
        {

        }
    }

    public class GetAllBookMasterCachedQueryHandler : IRequestHandler<GetAllBookMasterCachedQuery, Result<List<GetAllBookMasterCacheResponse>>>
    {
        private readonly IBookMasterCacheRepository repository;
        private readonly IMapper _mapper;
        public GetAllBookMasterCachedQueryHandler(IBookMasterCacheRepository repository, IMapper _mapper)
        {
            this.repository = repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<GetAllBookMasterCacheResponse>>> Handle(GetAllBookMasterCachedQuery request, CancellationToken cancellationToken)
        {

            var bookTypeList = await repository.GetCachedListAsync();
            var mappedBookTye = _mapper.Map<List<GetAllBookMasterCacheResponse>>(bookTypeList);
            return Result<List<GetAllBookMasterCacheResponse>>.Success(mappedBookTye);
        }
    }
}
