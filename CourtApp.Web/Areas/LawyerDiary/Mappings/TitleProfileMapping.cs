using AutoMapper;
using CourtApp.Application.DTOs.CaseTitle;
using CourtApp.Application.DTOs.CaseTitleRes;
using CourtApp.Application.DTOs.FSTitle;
using CourtApp.Application.Features.CaseTitle;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Web.Areas.LawyerDiary.Models.Title;
namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class TitleProfileMapping : Profile
    {
        public TitleProfileMapping()
        {
            CreateMap<FSTitleMViewModel, FSTitleCreateCommand>();
            CreateMap<FSTitleMViewModel, FSTitleUpdateCommand>();
            CreateMap<FSTitleByIdResponse, FSTitleMViewModel>();
            CreateMap<FSTitleResponse, FSTitleLViewModel>();

            CreateMap<TitleViewModel, CreateCaseTitleCommand>();
            CreateMap<TitleViewModel, UpdateCaseTitleCommand>();
            CreateMap<CaseTitleResponse, TitleGetViewModel>()
                .ForPath(d => d.CaseDetail, s => s.MapFrom(s => s.Case));

            CreateMap<CaseTitleByIdResponse, TitleViewModel>();


        }
    }
}
