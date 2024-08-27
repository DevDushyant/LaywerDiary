using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Features.FormBuilder;
using CourtApp.Web.Areas.Litigation.Models;
namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class DraftingProfile:Profile
    {
        public DraftingProfile()
        {
            CreateMap<FieldDetailsDto, FormProperties>();
            CreateMap<FormBuilderViewModel, CreateCaseDraftingDetailCommand>();
            CreateMap<FormProperties, TemplateFields>();
            CreateMap<CaseDarftingDtoResponse, FormCaseMappingViewModel>();
        }
    }
}
