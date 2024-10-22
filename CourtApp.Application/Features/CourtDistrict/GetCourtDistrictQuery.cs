﻿using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CourtApp.Application.Extensions;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class GetCourtDistrictQuery : IRequest<PaginatedResult<CourtDistrictReponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int StateId { get; set; }
        //public int DistrictId { get; set; }
    }
    public class GetCourtDistrictQueryHandler : IRequestHandler<GetCourtDistrictQuery, PaginatedResult<CourtDistrictReponse>>
    {
        private readonly ICourtDistrictRepository repository;
        public GetCourtDistrictQueryHandler(ICourtDistrictRepository repository)
        {
            this.repository = repository;
        }
        public async Task<PaginatedResult<CourtDistrictReponse>> Handle(GetCourtDistrictQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtDistrictEntity, CourtDistrictReponse>> expression = e => new CourtDistrictReponse
            {
                Id = e.Id,
                Abbreviation = e.Abbreviation,
                //DistrictName = e.District.Name_En,
                Name_En = e.Name_En,
                Name_Hn = e.Name_Hn,
                StateName = e.State.Name_En
            };
            var predicate = PredicateBuilder.True<CourtDistrictEntity>();
            if (predicate != null)
            {
                if (request.StateId != 0)
                    predicate = predicate.And(y => y.StateId == request.StateId);
                //if (request.DistrictId != 0)
                //    predicate = predicate.And(x => x.DistrictCode == request.DistrictId);

            }
            try
            {

                var paginatedList = await repository.Entities
                    .Include(c => c.State)
                    //.Include(c => c.District)
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                paginatedList.TotalPages=repository.Entities.Count();
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
