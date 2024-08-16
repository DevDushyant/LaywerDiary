using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.DTOs.FormPrint;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CourtApp.Application.Features.FormPrint
{
    public class GetInspectionQuery : IRequest<Result<List<InspectionResponse>>>
    {
        public List<Guid> CaseIds { get; set; }
    }
    public class GetCasesByIdsQueryHandler : IRequestHandler<GetInspectionQuery, Result<List<InspectionResponse>>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly ICaseProceedingRepository _wRepo;
        private readonly IMapper _mapper;
        private readonly IClientRepository _ClientRepo;
        private readonly IFSTitleRepository _AppeareceRepo;
        public GetCasesByIdsQueryHandler(IUserCaseRepository _CaseRepo, IMapper _mapper,
            ICaseProceedingRepository wRepo, IClientRepository clientRepo, IFSTitleRepository _AppeareceRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._mapper = _mapper;
            _wRepo = wRepo;
            _ClientRepo = clientRepo;
            this._AppeareceRepo = _AppeareceRepo;
        }
        public async Task<Result<List<InspectionResponse>>> Handle(GetInspectionQuery request, CancellationToken cancellationToken)
        {
            var Cases = from cd in _CaseRepo.Entites.Where(w => request.CaseIds.Contains(w.Id))
                        join cl in _ClientRepo.Clients on cd.Id equals cl.Id into clientDetails
                        from c in clientDetails.DefaultIfEmpty()
                        join f in _AppeareceRepo.Entities on c.AppearenceID equals f.Id into Appearnce
                        from a in Appearnce.DefaultIfEmpty()
                        join p in _wRepo.Entities on cd.Id equals p.CaseId into CProcs
                        from cpd in CProcs.DefaultIfEmpty()
                        select new InspectionResponse
                        {
                            Title = cd.FirstTitle + " Vs " + cd.SecondTitle,
                            NoYear = cd.CaseNo + "/" + cd.CaseYear,
                            CaseType = cd.CaseType.Name_En,
                            CourtType = cd.CourtType.CourtType,
                            CourtName = cd.CourtBench.CourtBench_En,
                            Appearence = a.Name_En,
                            NextDate = cpd.NextDate != null ? cpd.NextDate.Value.ToString("dd/MM/yyyy") : ""
                        };
            return Result<List<InspectionResponse>>.Success(Cases.ToList());
        }
    }
}
