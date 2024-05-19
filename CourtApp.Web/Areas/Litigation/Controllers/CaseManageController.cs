using CourtApp.Application.Features.BookMasters.Command;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseCategory;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseProceeding;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class CaseManageController : BaseController<CaseManageController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<IActionResult> CreateOrUpdateAsync(Guid id)
        {
            var ClientList = await _mediator.Send(new GetAllClientCachedQuery() { });
            if (id == Guid.Empty)
            {
                var caseViewModel = new CaseViewModel();
                caseViewModel.InstitutionDate = DateTime.Now;
                caseViewModel.CourtTypes = await LoadCourtTypes();
                caseViewModel.CourtDistricts = await DdlLoadCourtDistricts(1);

                caseViewModel.CaseNatures = await LoadCaseNature();
                caseViewModel.CaseKinds = await LoadCaseKinds();

                caseViewModel.CaseStages = await DdlCaseStages();
                caseViewModel.FirstTitleList = FirstTtitleList();
                caseViewModel.SecondTitleList = SecondTtitleList();
                caseViewModel.Years = DdlYears();
                caseViewModel.CaseStatusList = DdlCaseStatus();
                caseViewModel.LinkedBy = DdlClient().Result;
                caseViewModel.Cadres = DdlCadres();
                caseViewModel.Strengths = DdlStrength();
                caseViewModel.States = await LoadStates();
                return View("_CreateOrEdit", caseViewModel);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid Id, CaseViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CreateCaseCommand>(ViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                    {
                        ViewModel.StatusMessage = "Record created successfully";
                        ViewModel.Id = result.Data;
                        _notify.Success($"Case created with ID {result.Data} Created.");

                    }
                    else _notify.Error(result.Message);
                    return View("_CreateOrEdit", ViewModel);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateBookMasterCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Book type with ID {result.Data} Updated.");
                }
                return View("_CreateOrEdit", null);

            }
            else
            {
                ViewModel.InstitutionDate = DateTime.Now;
                ViewModel.CaseNatures = await LoadCaseNature();
                ViewModel.CaseKinds = await LoadCaseKinds();
                ViewModel.CourtTypes = await LoadCourtTypes();
                ViewModel.CaseStages = await DdlCaseStages();
                ViewModel.FirstTitleList = FirstTtitleList();
                ViewModel.SecondTitleList = SecondTtitleList();
                ViewModel.Years = DdlYears();
                ViewModel.CaseStatusList = DdlCaseStatus();
                ViewModel.LinkedBy = DdlClient().Result;
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        #region Case Proceeding Area    
        public async Task<IActionResult> LoadCaseProceeding()
        {
            var response = await _mediator.Send(new GetCaseProceedingQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCaseProceeding(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new CaseProceedingViewModel();
                ViewModel.Heads = await DdlProcHeads();
                ViewModel.NextStages = await DdlCaseStages();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CaseProceeding", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetQueryByIdCaseCategory() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<CaseProceedingViewModel>(response.Data);

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCaseProceeding(Guid id, CaseProceedingViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CreateCaseWorkCommand>(ViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                        _notify.Success($"Case Proceeding with ID {result.Data} Created.");
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateCaseProceedingCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Case Proceeding with ID {result.Data} Updated.");
                }
                return new JsonResult(new { isValid = false, html = "" });
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        #endregion


        #region Case History Resion
        public async Task<JsonResult> OnGetCaseHistory(Guid CaseId)
        {
            var response = await _mediator.Send(new GetCaseHistoryQuery() { CaseId = CaseId });
            if (response.Succeeded)
            {               
                var model = _mapper.Map<CaseHistoryViewModel>(response.Data);                
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CaseHistory", model) });
            }
            return null;
        }
        #endregion

    }
}
