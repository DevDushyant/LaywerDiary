using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CourtApp.Application.Features.Registers
{
    public class RegistCopyDisposalQuery : IRequest<PaginatedResult<CopyDisposalResponse>>
    {
        public string RegiserType { get; set; }
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class RegistDisposalQueryHandler : IRequestHandler<RegistCopyDisposalQuery, PaginatedResult<CopyDisposalResponse>>
    {
        private readonly IUserCaseRepository _caseRepo;
        private readonly IMapper _mapper;
        private readonly ICaseWorkRepository _wRepo;
        public RegistDisposalQueryHandler(IMapper _mapper, IUserCaseRepository _caseRepo
            , ICaseWorkRepository _wRepo)
        {
            this._caseRepo = _caseRepo;
            this._mapper = _mapper;
            this._wRepo = _wRepo;
        }
        public async Task<PaginatedResult<CopyDisposalResponse>> Handle(RegistCopyDisposalQuery request, CancellationToken cancellationToken)
        {
            string wcode = "";
            if (request.RegiserType == "d") wcode = "DISP"; else wcode = "COPY";
            var cWdt = _wRepo.Entities.Where(w => w.WorkingDate >= request.FromDt
                            && w.WorkingDate <= request.ToDt
                            && w.WorkType.Abbreviation == wcode);

            if (cWdt.Count() > 0)
            {
                var fndt = from cd in _caseRepo.Entites
                           join w in cWdt on cd.Id equals w.CaseId
                           select new CopyDisposalResponse
                           {
                               Id = cd.Id,
                               CourtType = cd.CourtType.CourtType,
                               CaseNo = cd.CaseNo,
                               CaseYear = cd.CaseYear,
                               CourtBench = cd.CourtBench.CourtBench_En,
                               FirstTitle = cd.FirstTitle,
                               SecondTitle = cd.SecondTitle,
                               CaseType = cd.CaseType.Name_En,
                               Reason = w.Work.Name_En
                           };

                if (fndt != null)
                    return await fndt.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
            return null;

        }
    }
}
