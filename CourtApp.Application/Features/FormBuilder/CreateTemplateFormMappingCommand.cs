using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
namespace CourtApp.Application.Features.FormBuilder
{
    public class CreateTemplateFormMappingCommand : IRequest<Result<Guid>>
    {
        public Guid TemplateId { get; set; }
        public Guid FormId { get; set; }
        public List<Mapping>FieldsMapping { get; set; }
    }
    public class Mapping
    {
        public string Tag { get; set; }
        public Guid Key { get; set; }
    }
}
