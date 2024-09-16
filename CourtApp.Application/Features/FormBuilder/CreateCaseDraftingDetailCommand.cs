using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.FormBuilder;

namespace CourtApp.Application.Features.FormBuilder
{
    public class CreateCaseDraftingDetailCommand : IRequest<Result<Guid>>
    {
        public Guid CaseId { get; set; }
        public Guid TemplateId { get; set; }
        public Guid DraftingFormId { get; set; }
        public List<TemplateFields> FieldDetails { get; set; }
    }

    public class TemplateFields
    {
        public Guid Key { get; set; }
        public string Value { get; set; }
    }

    public class CreateCaseDraftingDetailCommandCommandHanlder : IRequestHandler<CreateCaseDraftingDetailCommand, Result<Guid>>
    {
        private readonly ICaseDraftingRepository repository;
        private IUnitOfWork _UoW { get; set; }
        private readonly IMapper _mapper;
        public CreateCaseDraftingDetailCommandCommandHanlder(ICaseDraftingRepository repository
            , IUnitOfWork _UoW, IMapper _mapper)
        {
            this.repository = repository;
            this._mapper = _mapper;
            this._UoW = _UoW;
        }
        public async Task<Result<Guid>> Handle(CreateCaseDraftingDetailCommand request, CancellationToken cancellationToken)
        {
            if (request.CaseId != Guid.Empty && request.TemplateId != Guid.Empty)
            {
                var FormEntity = repository.Entities
                    .Where(w => w.CaseId == request.CaseId && w.TemplateId == request.TemplateId);

                if (FormEntity.Any())
                    return Result<Guid>.Fail($"Given is already exists.");
                else
                {
                    var _map = _mapper.Map<DraftingDetailEntity>(request);
                    await repository.InsertAsync(_map);
                    await _UoW.Commit(cancellationToken);
                    return Result<Guid>.Success(_map.Id);
                }
            }
            return null;
        }
    }
}
