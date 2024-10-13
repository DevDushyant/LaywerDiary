using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.ProcSubHead;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class GetProceedingSubHeadQuery : IRequest<PaginatedResult<GetProcSubHeadResponse>>
    {
        public Guid HeadId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetProceedingSubHeadQueryHandler : IRequestHandler<GetProceedingSubHeadQuery, PaginatedResult<GetProcSubHeadResponse>>
    {
        private readonly IProceedingSubHeadRepository repository;
        private readonly IMapper mapper;
        public GetProceedingSubHeadQueryHandler(IProceedingSubHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<PaginatedResult<GetProcSubHeadResponse>> Handle(GetProceedingSubHeadQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ProceedingSubHeadEntity, GetProcSubHeadResponse>> expression = e => new GetProcSubHeadResponse
            {
                Id = e.Id,               
                Name_En = e.Name_En,
                Name_Hn = e.Name_Hn,
                Head=e.Head.Name_En                
            };
            var predicate = PredicateBuilder.True<ProceedingSubHeadEntity>();
            if (predicate != null)
            {
                if (request.HeadId != Guid.Empty)
                    predicate = predicate.And(y => y.HeadId == request.HeadId);               
            }
            try
            {

                var paginatedList = await repository.Entities  
                    .Include(e => e.Head)
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return null;

        }
    }
}
