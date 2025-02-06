using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseCategory;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory
{
    public class GetQueryByIdCaseCategory : IRequest<Result<CaseCategoryByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class QryCmdByIdCaseCategoryHandler : IRequestHandler<GetQueryByIdCaseCategory, Result<CaseCategoryByIdResponse>>
    {
        private readonly ICaseNatureCacheRepository _repository;
        private readonly IMapper mapper;
        public QryCmdByIdCaseCategoryHandler(ICaseNatureCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this.mapper = _mapper;
        }


        public async Task<Result<CaseCategoryByIdResponse>> Handle(GetQueryByIdCaseCategory request, CancellationToken cancellationToken)
        {
            var data = _repository.GetCachedListAsync().Result.Where(w => w.Id == request.Id).FirstOrDefault();
            var mappeddata = mapper.Map<CaseCategoryByIdResponse>(data);
            return Result<CaseCategoryByIdResponse>.Success(mappeddata);
        }
    }
}
