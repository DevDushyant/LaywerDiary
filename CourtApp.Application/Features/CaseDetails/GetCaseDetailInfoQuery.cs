using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Constants;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.CaseDetails
{
    public class GetCaseDetailInfoQuery : IRequest<Result<CaseDetailInfoDto>>
    {
        public Guid CaseId { get; set; }
    }
    public class GetCaseDetailInfoQueryHandler : IRequestHandler<GetCaseDetailInfoQuery, Result<CaseDetailInfoDto>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly IMapper _Mapper;
        public GetCaseDetailInfoQueryHandler(IUserCaseRepository _CaseRepo, IMapper _Mapper)
        {
            this._CaseRepo = _CaseRepo;
            this._Mapper = _Mapper;
        }
        public async Task<Result<CaseDetailInfoDto>> Handle(GetCaseDetailInfoQuery request, CancellationToken cancellationToken)
        {
            var detail = await _CaseRepo
                                .Entites
                                .Include(ct => ct.CaseType)
                                .Include(c => c.CourtType)
                                .Include(c => c.CourtBench)
                                .Include(c => c.CaseCategory)
                                .Include(c=>c.FTitle)
                                .Include(c=>c.STitle)
                                .Include(c=>c.CaseStage)
                                .Where(w => w.Id == request.CaseId)
                                .FirstOrDefaultAsync();
            CaseDetailInfoDto ct = new CaseDetailInfoDto();
            ct.Id= request.CaseId;
            ct.InstitutionDate = detail.InstitutionDate.Date.ToString("dd/MM/yyyy");
            ct.CourtType = detail.CourtType.CourtType;
            ct.CaseNoYear = detail.CaseNo + "/" + detail.CaseYear;
            ct.CaseCategory = detail.CaseCategory.Name_En;
            ct.CaseType = detail.CaseType.Name_En;
            ct.CisNoYear = detail.CisNumber + "/" + detail.CisYear;
            //ct.FirstTitle = StaticDropDownDictionaries.FirstTitle()
            //    .Where(w => w.Key == detail.FirstTitleCode)
            //    .Select(s => s.Value).FirstOrDefault();
            ct.FirstTitleDetail = detail.FirstTitle;
            ct.FirstTitle = detail.FTitle.Name_En;
            //ct.SecondTitle = StaticDropDownDictionaries.SecoundTitle()
            //    .Where(w => w.Key == detail.SecoundTitleCode)
            //    .Select(s => s.Value).FirstOrDefault();
            ct.SecondTitleDetail = detail.SecondTitle;
            ct.SecondTitle = detail.FTitle.Name_En;
            ct.NextDate = detail.NextDate!=null?detail.NextDate.Value.ToString("dd/MM/yyyy"):"";
            ct.CaseStage = detail.CaseStage.CaseStage;
            ct.IsHighCourt = detail.CourtTypeId.Equals("359acb34-7b8c-4fc8-a276-8b198ea5105c") ? true : false;
            ct.CourtBench=detail.CourtBench.CourtBench_En;
            return Result<CaseDetailInfoDto>.Success(ct);
        }
    }
}
