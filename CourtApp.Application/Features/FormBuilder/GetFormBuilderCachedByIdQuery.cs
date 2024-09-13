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
    public class GetFormBuilderCachedByIdQuery : IRequest<Result<FormBuilderResponseByIdDto>>
    {
        public Guid Id { get; set; }
    }
    public class GetFormBuilderCachedByIdQueryHanlder : IRequestHandler<GetFormBuilderCachedByIdQuery, Result<FormBuilderResponseByIdDto>>
    {
        private readonly IMapper _mapper;
        private readonly IFormBuilderCacheRepository _repository;
        public GetFormBuilderCachedByIdQueryHanlder(IFormBuilderCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<FormBuilderResponseByIdDto>> Handle(GetFormBuilderCachedByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Dt = await _repository.GetByIdAsync(request.Id);
                var result = _mapper.Map<FormBuilderResponseByIdDto>(Dt);
                if (Dt != null && Dt.FieldsDetails != null)
                {
                    var fields = Dt.FieldsDetails.Fields;
                    var mappedDt = _mapper.Map<List<FieldDetailsDto>>(fields);
                    result.FieldDetails= mappedDt;
                    return Result<FormBuilderResponseByIdDto>.Success(result);
                }
                return Result<FormBuilderResponseByIdDto>.Fail("No Record found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
