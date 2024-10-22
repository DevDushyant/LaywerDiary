using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Domain.Entities.FormBuilder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormBuilder
{
    public class CreateTemplateInfoCommand : IRequest<Result<Guid>>
    {
        public string TemplateName { get; set; }
        public string TemplatePath { get; set; }
        public List<TemplateTags> Tags { get; set; }
    }
    public class TemplateTags
    {
        public string Tag { get; set; }
    }
    public class CreateTemplateInfoCommandHandler : IRequestHandler<CreateTemplateInfoCommand, Result<Guid>>
    {
        private readonly ITemplateInfoRepository _repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _UoW { get; set; }
        public CreateTemplateInfoCommandHandler(ITemplateInfoRepository _repository,
            IMapper _mapper, IUnitOfWork _UoW)
        {
            this._repository = _repository;
            this._mapper = _mapper;
            this._UoW = _UoW;
        }
        public async Task<Result<Guid>> Handle(CreateTemplateInfoCommand request, CancellationToken cancellationToken)
        {
            if (request.TemplateName != string.Empty)
            {
                var Entity = _repository.Entities
                    .Where(w => w.TemplateName.Equals(request.TemplateName));
                if (Entity.Any())
                    return Result<Guid>.Fail($"The given template info is already exists.");
                else
                {                   
                    var entity = new TemplateInfoEntity() { CreatedBy = "" };
                    List<TemplateTagsEntity> tdel = new List<TemplateTagsEntity>();
                    entity.TemplateName = request.TemplateName;
                    entity.TemplatePath  = request.TemplatePath;                    
                    foreach (var item in request.Tags)
                    {                        
                        tdel.Add(new TemplateTagsEntity()
                        {
                            Tag = item.Tag
                        });
                    }
                    entity.Tags = tdel;
                    await _repository.InsertAsync(entity);
                    await _UoW.Commit(cancellationToken);
                    return Result<Guid>.Success(entity.Id);
                }
            }
            return Result<Guid>.Fail("Template Name is not given");
        }
    }
}
