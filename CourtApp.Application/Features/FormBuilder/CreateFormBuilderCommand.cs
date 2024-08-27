using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Domain.Entities.FormBuilder;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormBuilder
{
    public class CreateFormBuilderCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string FormName { get; set; }
        public FormFieldsDto Form { get; set; }
    }
    public class CreateFormBuilderCommandHandler : IRequestHandler<CreateFormBuilderCommand, Result<Guid>>
    {
        private readonly IFormBuilderRepository repository;
        private IUnitOfWork _UoW { get; set; }
        private readonly IMapper _mapper;

        public CreateFormBuilderCommandHandler(IFormBuilderRepository repository, IUnitOfWork _UoW, IMapper _mapper)
        {
            this.repository = repository;
            this._mapper = _mapper;
            this._UoW = _UoW;
        }
        public async Task<Result<Guid>> Handle(CreateFormBuilderCommand request, CancellationToken cancellationToken)
        {
            if (request.FormName != null)
            {
                var FormEntity = repository.Entities
                    .Where(w => w.FormName.ToLower().Equals(request.FormName.Trim().ToLower()));

                if (FormEntity.Any())
                    return Result<Guid>.Fail($"{request.FormName} is already exists.");
                else
                {
                    var _map = _mapper.Map<FormBuilderEntity>(request);
                    await repository.InsertAsync(_map);
                    await _UoW.Commit(cancellationToken);
                    return Result<Guid>.Success(_map.Id);
                }
            }
            return null;
        }
    }
}
