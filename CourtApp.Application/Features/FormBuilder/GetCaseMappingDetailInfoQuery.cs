using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.CacheRepositories.FormBuilder;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormBuilder
{
    public class GetCaseMappingDetailInfoQuery : IRequest<Result<CaseMappingDetailInfoDto>>
    {
        public Guid Id { get; set; }
    }
    public class GetCaseMappingDetailInfoQueryHandler : IRequestHandler<GetCaseMappingDetailInfoQuery, Result<CaseMappingDetailInfoDto>>
    {
        private readonly ICaseStageCacheRepository _RepoStage;
        private readonly ICaseNatureCacheRepository _RepoNature;
        private readonly IUserCaseRepository _RepoCase;
        private readonly ICaseDraftingRepository _RepoDrafting;
        private readonly ITemplateInfoCacheRepository _RepoTemplate;
        private readonly IFormTempMappingRepository _RepoMapping;
        public GetCaseMappingDetailInfoQueryHandler(IUserCaseRepository _RepoCase,
            ICaseNatureCacheRepository _RepoNature,
            ICaseStageCacheRepository _RepoStage,
            ICaseDraftingRepository _RepoDrafting,
            ITemplateInfoCacheRepository _RepoTemplate,
            IFormTempMappingRepository _RepoMapping
            )
        {
            this._RepoCase = _RepoCase;
            this._RepoNature = _RepoNature;
            this._RepoNature = _RepoNature;
            this._RepoTemplate = _RepoTemplate;
            this._RepoDrafting = _RepoDrafting;
            this._RepoTemplate = _RepoTemplate;
            this._RepoMapping = _RepoMapping;
        }
        public async Task<Result<CaseMappingDetailInfoDto>> Handle(GetCaseMappingDetailInfoQuery request, CancellationToken cancellationToken)
        {
            var draftingDetail = _RepoDrafting
                .Entities
                .Include(c => c.Case)
                .Include(c=>c.Case.CaseType)
                .Where(w => w.Id == request.Id).FirstOrDefault();
            if (draftingDetail != null)
            {
                var caseId = draftingDetail.CaseId;
                var TemplateId = draftingDetail.TemplateId;
                var draftId = draftingDetail.DraftingFormId;
                var FieldDetails = draftingDetail.FieldDetails;
                var TempInfo=await _RepoTemplate.GetByIdAsync(TemplateId);
                var FieldMapping = _RepoMapping
                    .Entities
                    .Where(w => w.FormId == draftId && w.TemplateId == TemplateId)                    
                    .FirstOrDefault();

                var tagMappingDetails = from fm in FieldMapping.FieldsMapping
                                        join fd in FieldDetails on fm.Key equals fd.Key
                                        select new MappingDetails
                                        {
                                            Key = fm.Tag,
                                            Value = fd.Value
                                        };
                CaseMappingDetailInfoDto cmd = new CaseMappingDetailInfoDto();
                cmd.CaseTitle = draftingDetail.Case.FirstTitle + " Vs " + draftingDetail.Case.FirstTitle;
                cmd.CaseNoYear = draftingDetail.Case.CaseNo + "/" + draftingDetail.Case.CaseYear;
                cmd.CaseType = draftingDetail.Case.CaseType.Name_En;
                cmd.TagValues = tagMappingDetails.ToList(); 
                cmd.TemplateName=TempInfo.TemplateName;
                cmd.TemplatePath = TempInfo.TemplatePath;
                return Result<CaseMappingDetailInfoDto>.Success(cmd);
            }
            return null;
        }
    }
}
