using AutoMapper;
using CourtApp.Application.Features.BookMasters.Commands;
using CourtApp.Application.Features.BookMasters.Queries;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class BookMasterProfile : Profile
    {
        public BookMasterProfile()
        {
            CreateMap<GetBookMasterByIdResponse, LDBookEntity>().ReverseMap();
            CreateMap<GetAllBookMasterCacheResponse, LDBookEntity>().ReverseMap();
            CreateMap<CreateBookMasterCommand, LDBookEntity>().ReverseMap();
            CreateMap<UpdateBookMasterCommand, LDBookEntity>().ReverseMap();
            CreateMap<DeleteBookMasterCommand, LDBookEntity>().ReverseMap();
        }

    }
}
