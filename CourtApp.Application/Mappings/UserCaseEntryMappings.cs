using AutoMapper;
using CourtApp.Application.Commands.Cases;
using CourtApp.Application.Features.BookMasters.Commands;
using CourtApp.Application.Features.BookMasters.Queries;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    internal class UserCaseEntryMappings : Profile
    {
        public UserCaseEntryMappings()
        {
           
            CreateMap<CommandCreateCaseEntry, CaseEntity>().ReverseMap();
            CreateMap<CommandUpdateCaseEntry, CaseEntity>().ReverseMap();
            CreateMap<CommandDeleteCaseEntry, CaseEntity>().ReverseMap();
            
        }
    
    }
}
