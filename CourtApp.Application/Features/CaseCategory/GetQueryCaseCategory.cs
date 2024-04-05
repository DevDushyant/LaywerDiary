using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CaseCategory;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory
{
    public class GetQueryCaseCategory : IRequest<PaginatedResult<CaseCategoryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid CourtTypeId { get; set; }
        public int StateCode { get; set; }
    }
    public class QCmdCaseCategoryHandler : IRequestHandler<GetQueryCaseCategory, PaginatedResult<CaseCategoryResponse>>
    {
        private readonly ICaseNatureRepository repository;
        public QCmdCaseCategoryHandler(ICaseNatureRepository repository)
        {
            this.repository = repository;
        }
        public async Task<PaginatedResult<CaseCategoryResponse>> Handle(GetQueryCaseCategory request, CancellationToken cancellationToken)
        {
            Expression<Func<NatureEntity, CaseCategoryResponse>> expression = e => new CaseCategoryResponse
            {
                Id = e.Id,
                CourtType = e.CourtType.CourtType,
                StateName = e.State.Name_En,
                Name_En = e.Name_En,
                Name_Hn = e.Name_Hn
            };
            var predicate = PredicateBuilder.True<NatureEntity>();
            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtTypeId == request.CourtTypeId);

            if (request.StateCode != 0)
                predicate = predicate.And(p => p.StateId == request.StateCode);


            var paginatedList = await repository.CaseNatures.Where(predicate)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}
