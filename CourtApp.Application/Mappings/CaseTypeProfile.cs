using AutoMapper;
using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class TypeofcasesProfile : Profile
    {
        public TypeofcasesProfile()
        {
            CreateMap<TypeOfCasesCacheQueryResponse, TypeOfCasesEntity>().ReverseMap();
            CreateMap<TypeOfCasesQueryByIdResponse, TypeOfCasesEntity>().ReverseMap();
            CreateMap<CreateTypeOfCasesCommand, TypeOfCasesEntity>().ReverseMap();
            CreateMap<UpdateTypeOfCasesCommand, TypeOfCasesEntity>().ReverseMap();
            CreateMap<DeleteTypeOfCasesCommand, TypeOfCasesEntity>().ReverseMap();
        }
    }
}
