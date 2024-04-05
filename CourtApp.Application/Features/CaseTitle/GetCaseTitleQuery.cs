using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CaseTitle;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseTitle
{
    public class GetCaseTitleQuery : IRequest<PaginatedResult<CaseTitleResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TypeId { get; set; }
        
    }
    public class GetCaseTitleQueryHandler : IRequestHandler<GetCaseTitleQuery, PaginatedResult<CaseTitleResponse>>
    {
        private readonly ICaseTitleRepository repository;
        public GetCaseTitleQueryHandler(ICaseTitleRepository repository)
        {
            this.repository = repository;
        }
        public async Task<PaginatedResult<CaseTitleResponse>> Handle(GetCaseTitleQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseTitleEntity, CaseTitleResponse>> expression = e => new CaseTitleResponse
            {
                Id = e.Id,
                Case=e.Case.FirstTitle,
                title=e.Title,
                
            };
            var predicate = PredicateBuilder.True<CaseTitleEntity>();
            if (predicate != null)
            {
                if (request.TypeId != 0)
                    predicate = predicate.And(y => y.TypeId == request.TypeId);               

            }
            try
            {

                var paginatedList = await repository.Titles                   
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
