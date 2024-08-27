using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Interfaces.CacheRepositories.FormBuilder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.FormBuilder
{
    public class GetFormBuilderCachedByIdQuery : IRequest<Result<List<FieldDetailsDto>>>
    {
        public Guid Id { get; set; }
    }
    public class GetFormBuilderCachedByIdQueryHanlder : IRequestHandler<GetFormBuilderCachedByIdQuery, Result<List<FieldDetailsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IFormBuilderCacheRepository _repository;
        public GetFormBuilderCachedByIdQueryHanlder(IFormBuilderCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<FieldDetailsDto>>> Handle(GetFormBuilderCachedByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Dt = await _repository.GetByIdAsync(request.Id);
                if (Dt != null && Dt.FieldsDetails != null)
                {
                    var fields = Dt.FieldsDetails.Fields;
                    var mappedDt = _mapper.Map<List<FieldDetailsDto>>(fields);                    
                    return Result<List<FieldDetailsDto>>.Success(mappedDt);
                }
                return Result<List<FieldDetailsDto>>.Fail("No Record found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
