using AspNetCoreHero.Results;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.TypeOfCases.Query
{
    public class GetAllTypeOfCasesQuery:IRequest<PaginatedResult<GetAllTypeOfCasesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CourtTypeId { get; set; }
        public int StateId { get; set; }
        public GetAllTypeOfCasesQuery(int pagenumber, int pagesize)
        {
            PageNumber = pagenumber;
            PageSize = pagesize;           
        }
    }

    public class GetAllTypeOfCasesQueryHandler : IRequestHandler<GetAllTypeOfCasesQuery, PaginatedResult<GetAllTypeOfCasesResponse>>
    {
        private readonly ITypeOfCasesRepository _repository;
        public GetAllTypeOfCasesQueryHandler(ITypeOfCasesRepository _repository)
        {
            this._repository = _repository;
        }
        public async Task<PaginatedResult<GetAllTypeOfCasesResponse>> Handle(GetAllTypeOfCasesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<TypeOfCasesEntity, GetAllTypeOfCasesResponse>> expression = e => new GetAllTypeOfCasesResponse
            {
                Id = e.Id,
               CaseNature=e.Nature.Name_En,
               Name_En=e.Name_En,
               Name_Hn=e.Name_Hn,
               Abbreviation=e.Abbreviation,
               CourtTypeName=e.CourtType.CourtType,
               StateName=e.State.Name_En
            };
            var predicate = PredicateBuilder.True<TypeOfCasesEntity>();

            if (request.CategoryId != Guid.Empty)
                predicate = predicate.And(b => b.Nature.Id==request.CategoryId);

            if (request.StateId != 0)
                predicate = predicate.And(b => b.StateId == request.StateId);

            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtTypeId == request.CourtTypeId);
            try
            {
                var paginatedList = await _repository.QryEntities
                    .Include(c => c.CourtType)
                    .Include(c => c.State)
                    .Include(c => c.Nature)
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                paginatedList.TotalCount = _repository.QryEntities.Count();
                return paginatedList;
            }
            catch(Exception ex) { 
            Console.WriteLine(ex.ToString());
            }
            return null;

        }
    }
}
