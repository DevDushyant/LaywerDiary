using AutoMapper;
using CourtApp.Application.Features.Commands.BookMasters;
using CourtApp.Application.Features.Queries.BookMasters;
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
