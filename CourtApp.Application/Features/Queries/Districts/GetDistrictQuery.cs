using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;

namespace CourtApp.Application.Features.Queries.Districts
{
    public class GetDistrictQuery : IRequest<Result<List<GetDistrictResponse>>>
    {
        public int StateCode { get; set; }
    }

    public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, Result<List<GetDistrictResponse>>>
    {

        private readonly IDsitrictMasterCacheRepository _repositoryCache;
        private readonly IDsitrictMasterRepository _repository;
        private readonly IMapper _mapper;
        public GetDistrictQueryHandler(IDsitrictMasterRepository _repository, IMapper _mapper)
        {
            //this._repositoryCache = _repositoryCache;
            this._repository= _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<GetDistrictResponse>>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
        {
            request.StateCode = request.StateCode;
            var list = await _repository.GetDistrictListByStateAsync(request.StateCode);
            var mappeddata = _mapper.Map<List<GetDistrictResponse>>(list.OrderBy(i => i.Name_En));
            return Result<List<GetDistrictResponse>>.Success(mappeddata);
        }
    }
}