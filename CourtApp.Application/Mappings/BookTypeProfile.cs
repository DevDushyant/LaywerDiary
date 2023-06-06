using AutoMapper;
using CourtApp.Application.Features.BookTypes.Commands;
using CourtApp.Application.Features.BooTypes.Queries.GetAllCached;
using CourtApp.Application.Features.BooTypes.Queries.GetById;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class BookTypeProfile : Profile
    {
        public BookTypeProfile()
        {
            CreateMap<CreateBookTypeCommand, BookTypeEntity>().ReverseMap();
            CreateMap<GetBookTypeByIdResponse, BookTypeEntity>().ReverseMap();
            CreateMap<GetAllBookTypeCachedResponse, BookTypeEntity>().ReverseMap();
        }
    }
}
