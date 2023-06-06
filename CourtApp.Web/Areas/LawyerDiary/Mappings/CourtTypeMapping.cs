using AutoMapper;
using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Application.Features.CourtType.Command;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CourtTypeMapping:Profile
    {
        public CourtTypeMapping()
        {
            CreateMap<GetCourtTypeResponse, CourtTypeViewModel>().ReverseMap();
           
            CreateMap<CreateCourtTypeCommand, CourtTypeViewModel>().ReverseMap();
            CreateMap<UpdateCourtTypeCommand, CourtTypeViewModel>().ReverseMap();
            CreateMap<DeleteCourtTypeCommand, CourtTypeViewModel>().ReverseMap();
        }
    }
}
