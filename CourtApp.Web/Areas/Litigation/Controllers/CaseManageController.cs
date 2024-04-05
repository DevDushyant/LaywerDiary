using CourtApp.Application.Constants;
using CourtApp.Application.Enums;
using CourtApp.Application.Features.BookMasters.Command;
using CourtApp.Application.Features.Case;
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
            var model = new GetCaseViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery(1, 100));
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
                caseViewModel.CaseNatures = await LoadCaseNature();
                caseViewModel.CaseKinds = await LoadCaseKinds();
                caseViewModel.CourtTypes = await LoadCourtTypes();
                caseViewModel.CaseStages = await DdlCaseStages();
                caseViewModel.FirstTitleList = FirstTtitleList();
                caseViewModel.SecondTitleList = SecondTtitleList();
                caseViewModel.Years = DdlYears();
                caseViewModel.CaseStatusList = DdlCaseStatus();
                caseViewModel.LinkedBy =DdlClient().Result;
                caseViewModel.Cadres =DdlCadres();            
                return View("_CreateOrEdit", caseViewModel);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid Id, CaseViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CreateCaseCommand>(ViewModel);
                    //createCommand.ActionType = ((int)ActionTypes.Add);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded) {
                        _notify.Success($"Case created with ID {result.Data} Created.");
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", ViewModel);
                        return new JsonResult(new { isValid = true, html = html });
                    }                    
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateBookMasterCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Book type with ID {result.Data} Updated.");
                }
                //var response = await _mediator.Send(new QueryGetAllCaseEntry() { PageNumber = 1, PageSize = 100 });
                //if (response.Succeeded)
                //{
                //    var viewModel = _mapper.Map<List<CaseViewModel>>(response.Data);
                //    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                //    return new JsonResult(new { isValid = true, html = html });
                //}
                //else
                //{
                //    _notify.Error(response.Message);
                //    return null;
                //}
                return new JsonResult(new { isValid = false, html = "" });
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
    }
}
