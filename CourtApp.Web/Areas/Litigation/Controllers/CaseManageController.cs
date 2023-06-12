using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Commands.BookMasters;
using CourtApp.Application.Features.Commands.Cases;
using CourtApp.Application.Features.Queries.Cases;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var model = new CaseViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new QueryGetAllCaseEntry() { PageNumber=1,PageSize=100});
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CaseViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<IActionResult> CreateOrUpdateAsync(Guid? id=null)
        {
            var ClientList = await _mediator.Send(new GetAllClientCachedQuery() { });
            if (id == null)
            {
                var clientiewModel = _mapper.Map<List<ClientsViewModel>>(ClientList.Data);
                var caseViewModel = new CaseViewModel();
                caseViewModel.InstitutionDate= DateTime.Now;
                caseViewModel.CaseNatures = await LoadCaseNature();
                caseViewModel.CaseTypes=await LoadCaseTypes();
                caseViewModel.CourtTypes=await LoadCourtTypes();
                caseViewModel.CaseStages=await DdlCaseStages();
                caseViewModel.FirstTitleList= FirstTtitleList();
                caseViewModel.SecondTitleList= SecondTtitleList();
                caseViewModel.Year= DdlYears();
                caseViewModel.CaseStatusList = DdlCaseStatus();
                caseViewModel.LinkedBy = DdlClientCases().Result;
                caseViewModel.ClientList = new SelectList(clientiewModel, nameof(ClientsViewModel.Id), nameof(ClientsViewModel.FullName), null, null);
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
                if (Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    var createCommand = _mapper.Map<CommandCreateCaseEntry>(ViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                        _notify.Success($"Case created with ID {result.Data} Created.");
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateBookMasterCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Book type with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new QueryGetAllCaseEntry() { PageNumber = 1, PageSize = 100 });
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CaseViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
    }
}
