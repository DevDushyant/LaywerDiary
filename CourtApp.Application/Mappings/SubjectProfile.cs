using AutoMapper;
using CourtApp.Application.Features.Subjects.Commands;
using CourtApp.Application.Features.Subjects.Queries;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<CreateSubjectCommand, PracticeSubjectEntity>().ReverseMap();           
            CreateMap<UpdateSubjectCommand, PracticeSubjectEntity>().ReverseMap();           
            CreateMap<DeleteSubjectCommand, PracticeSubjectEntity>().ReverseMap();           
            CreateMap<PracticeSubjectQueryResponse, PracticeSubjectEntity>().ReverseMap();
            CreateMap<PracticeSubjectCacheQueryResponse, PracticeSubjectEntity>().ReverseMap();
        }
   
    }
}
