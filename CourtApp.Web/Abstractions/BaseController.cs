using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CourtApp.Application.Constants;
using CourtApp.Application.Features.CaseCategory;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.CourtComplex;
using CourtApp.Application.Features.CourtDistrict;
using CourtApp.Application.Features.CourtMasters;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.ProceedingSubHead;
using CourtApp.Application.Features.Queries.Districts;
using CourtApp.Application.Features.Queries.States;
using CourtApp.Application.Features.TypeOfCases.Query;
using CourtApp.Application.Features.UserCase;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Application.Features.WorkMasterSub;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.Litigation.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Abstractions
{
    public abstract class BaseController<T> : Controller
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;
        private IViewRenderService _viewRenderInstance;
        private IMapper _mapperInstance;
        private INotyfService _notifyInstance;
        protected INotyfService _notify => _notifyInstance ??= HttpContext.RequestServices.GetService<INotyfService>();

        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected IViewRenderService _viewRenderer => _viewRenderInstance ??= HttpContext.RequestServices.GetService<IViewRenderService>();
        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();

        #region Dropdown Select List
        public async Task<SelectList> LoadStates()
        {
            var response = await _mediator.Send(new GetStateMasterQuery());
            var ViewModel = _mapper.Map<List<StateViewModel>>(response.Data);
            return new SelectList(ViewModel, nameof(StateViewModel.Code), nameof(StateViewModel.Name_En), null, null);
        }
        public async Task<SelectList> LoadCourtTypes()
        {
            var response = await _mediator.Send(new GetCourtTypeQuery());
            var CaseKind = _mapper.Map<List<CourtTypeViewModel>>(response.Data);
            return new SelectList(CaseKind, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null);
        }
        public async Task<SelectList> DdlLoadCourtDistricts(int DistrictId)
        {
            var districts = await _mediator.Send(new GetCourtDistrictQuery() { DistrictId = DistrictId });
            var districtViewModel = _mapper.Map<List<CourtDistrictViewModel>>(districts.Data);
            return new SelectList(districtViewModel, nameof(CourtDistrictViewModel.Id), nameof(CourtDistrictViewModel.Name_En), null, null);
        }
        public async Task<SelectList> DdlCourt()
        {
            var response = await _mediator.Send(new GetCourtTypeQuery());
            var ViewModel = _mapper.Map<List<CourtTypeViewModel>>(response.Data);
            return new SelectList(ViewModel, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null);
        }
        public async Task<SelectList> LoadCaseNature()
        {
            var response = await _mediator.Send(new GetQueryCaseCategory());
            var CaseNatures = _mapper.Map<List<CaseNatureViewModel>>(response.Data);
            return new SelectList(CaseNatures, nameof(CaseNatureViewModel.Id), nameof(CaseNatureViewModel.Name_En), null, null);
        }
        public async Task<JsonResult> LoadCaseCategory(Guid CourtTypeId)
        {
            var response = await _mediator.Send(new GetQueryCaseCategory { CourtTypeId = CourtTypeId });
            var CaseNatures = _mapper.Map<List<CaseNatureViewModel>>(response.Data);
            var data = Json(CaseNatures);
            return data;
        }
        public async Task<SelectList> LoadCaseKinds()
        {
            var response = await _mediator.Send(new CaseKindAllCacheQuery());
            var CaseKind = _mapper.Map<List<CaseKindViewModel>>(response.Data);
            return new SelectList(CaseKind, nameof(CaseKindViewModel.Id), nameof(CaseKindViewModel.CaseKind), null, null);

        }
        public async Task<SelectList> DdlLoadDistrict(int StateCode)
        {
            var districts = await _mediator.Send(new GetDistrictQuery() { StateCode = StateCode });
            var districtViewModel = _mapper.Map<List<DistrictViewModel>>(districts.Data);
            return new SelectList(districtViewModel, nameof(DistrictViewModel.Code), nameof(DistrictViewModel.Name_En), null, null);
        }
        public async Task<SelectList> DdlCaseStages()
        {
            var stages = await _mediator.Send(new CaseStageCacheAllQuery());
            var caseStagesViewModel = _mapper.Map<List<CaseStageViewModel>>(stages.Data);
            return new SelectList(caseStagesViewModel, nameof(CaseStageViewModel.Id), nameof(CaseStageViewModel.CaseStage), null, null);
        }
        public async Task<SelectList> DdlClient()
        {
            var response = await _mediator.Send(new GetAllClientCachedQuery());
            var viewModel = _mapper.Map<List<GClientViewModel>>(response.Data);
            return new SelectList(viewModel, nameof(GClientViewModel.Id), nameof(GClientViewModel.Name), null, null);
        }

        public async Task<SelectList> UserCaseTitle()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery());
            var viewModel = _mapper.Map<List<GetCaseViewModel>>(response.Data);
            return new SelectList(viewModel, nameof(GetCaseViewModel.Id), nameof(GetCaseViewModel.CaseTitle), null, null);

        }


        #endregion

        public async Task<JsonResult> LoadDistricts(int StateCode)
        {
            var districts = await _mediator.Send(new GetDistrictQuery() { StateCode = StateCode });
            var data = Json(districts);
            return data;
        }
        public async Task<JsonResult> LoadCourtDistrict(int DistrictId)
        {
            var districts = await _mediator.Send(new GetCourtDistrictQuery() { DistrictId = DistrictId });
            var data = Json(districts);
            return data;
        }

        public async Task<JsonResult> LoadCourtDistrictByState(int StateId)
        {
            var districts = await _mediator.Send(new GetCourtDistrictQuery() { StateId = StateId });
            var data = Json(districts);
            return data;
        }

        public async Task<JsonResult> LoadCourt(Guid CourtTypeId)
        {
            var response = await _mediator.Send(new GetCourtMasterAllQuery()
            {
                CourtTypeId = CourtTypeId

            });
            return Json(response);
        }
        public async Task<JsonResult> LoadCourtType()
        {
            var response = await _mediator.Send(new GetCourtTypeQuery());
            return Json(response);
        }

        public async Task<JsonResult> LoadCourtComplex(Guid CDistrictId)
        {
            var response = await _mediator.Send(new GetCourtComplexQuery() { CourDistrictId = CDistrictId });
            return Json(response);
        }
        public async Task<JsonResult> LoadTypeOfCase(Guid natureId)
        {
            var caseType = await _mediator.Send(new GetAllTypeOfCasesQuery(1, 100) { CategoryId = natureId });
            var data = Json(caseType);
            return data;
        }
        public async Task<JsonResult> LCCByCourtTypeStage(Guid CourtType, int StateId)
        {
            var CCatogories = await _mediator.Send(new GetQueryCaseCategory()
            {
                StateCode = StateId,
                CourtTypeId = CourtType
            });
            if (CCatogories.Succeeded)
            {
                var data = Json(CCatogories);
                return data;
            }
            return null;
        }

        public async Task<JsonResult> LoadCourtBench(Guid CourtTypeId, int StateId, Guid ComplexId)
        {
            var dt = await _mediator.Send(new GetCourtBenchQuery(1, 100) { StateId = StateId, CourtTypeId = CourtTypeId, CourtId = ComplexId });
            var data = Json(dt);
            return data;
        }

        #region Case Proceeding & Sub Proceeding
        public async Task<SelectList> DdlProcHeads()
        {
            var response = await _mediator.Send(new GetProceedingHeadQuery());
            var viewModel = _mapper.Map<List<ProceedingHeadViewModel>>(response.Data);
            return new SelectList(viewModel, nameof(ProceedingHeadViewModel.Id), nameof(ProceedingHeadViewModel.Name_En), null, null);
        }
        public async Task<JsonResult> DdlSubProcHeads(Guid Id)
        {
            var response = await _mediator.Send(new GetProceedingSubHeadQuery { HeadId = Id });
            return Json(response);
        }
        #endregion

        #region Case Work And Sub Work Area
        public async Task<SelectList> DdlWorks()
        {
            var response = await _mediator.Send(new GetWorkMasterCommand());
            var viewModel = _mapper.Map<List<WorkMasterViewModel>>(response.Data);
            return new SelectList(viewModel, nameof(WorkMasterViewModel.Id), nameof(WorkMasterViewModel.Work_En), null, null);
        }
        public async Task<JsonResult> DdlSubWorks(Guid WorkId)
        {
            var response = await _mediator.Send(new GWorkSubMstQuery { WorkId = WorkId });
            return Json(response);
        }
        #endregion

        #region Static Dropdown Region

        public SelectList DdlYears()
        {
            return new SelectList(StaticDropDownDictionaries.Year(), "Key", "Value");
        }
        public SelectList FirstTtitleList()
        {
            return new SelectList(StaticDropDownDictionaries.FirstTitle(), "Key", "Value");
        }
        public SelectList SecondTtitleList()
        {
            return new SelectList(StaticDropDownDictionaries.SecoundTitle(), "Key", "Value");
        }

        public SelectList DdlCaseStatus()
        {
            return new SelectList(StaticDropDownDictionaries.CaseStatus().OrderBy(v => v.Value), "Key", "Value");
        }
        public SelectList DdlCaseTitle()
        {
            return new SelectList(StaticDropDownDictionaries.CaseTitle().OrderBy(v => v.Value), "Key", "Value");
        }
        public SelectList DdlCadres()
        {
            return new SelectList(StaticDropDownDictionaries.Cadres().OrderBy(v => v.Value), "Key", "Value");
        }
        public SelectList DdlStrength()
        {
            return new SelectList(StaticDropDownDictionaries.Stength().OrderBy(v => v.Key), "Key", "Value");
        }
        #endregion

    }
}