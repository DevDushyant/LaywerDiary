using AutoMapper;
using CourtApp.Application.Features.BookTypes.Commands;
using CourtApp.Application.Features.Commands.BookTypes;
using CourtApp.Application.Features.Queries.BookTypes.GetAllCached;
using CourtApp.Application.Features.Queries.BookTypes.GetById;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class BookTypeMapping:Profile
    {
        public BookTypeMapping()
        {
            CreateMap<GetAllBookTypeCachedResponse, BookTypeViewModel>().ReverseMap();
            CreateMap<GetBookTypeByIdResponse, BookTypeViewModel>().ReverseMap();
            CreateMap<CreateBookTypeCommand, BookTypeViewModel>().ReverseMap();
            CreateMap<UpdateCreateBookTypeCommand, BookTypeViewModel>().ReverseMap();
        }
    }
}
