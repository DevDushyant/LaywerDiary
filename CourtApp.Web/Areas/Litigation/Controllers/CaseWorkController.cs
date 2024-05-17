using CourtApp.Application.Features.CaseStages.Command;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Application.Features.CaseWork;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class CaseWorkController : BaseController<CaseWorkController>
    {
        public IActionResult Index()
        {
            var model = new CaseWorkingViewModel();
            return View(model);
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAssignedWorkQuery { PageSize = 10, PageNumber = 1 });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CaseWorkingViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new CaseWorkingViewModel();
                ViewModel.CaseTitles = await UserCaseTitle();
                ViewModel.Works = await DdlWorks();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new CaseStageByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<CaseWorkingViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CaseWorkingViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var cmd = _mapper.Map<CreateCaseWorkCommand>(ViewModel);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Case work with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateBookCommand = _mapper.Map<UpdateCaseStageCommand>(ViewModel);
                    var result = await _mediator.Send(updateBookCommand);
                    if (result.Succeeded) _notify.Information($"Case Nature with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAssignedWorkQuery { PageSize = 10, PageNumber = 1 });
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CaseWorkingViewModel>>(response.Data);
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

        public async Task<JsonResult> UpdateCaseWorkStatus(List<Guid> CWorkId)
        {
            if (CWorkId != null)
            {
                var result = await _mediator.Send(new UpdateCWorkStatusCommand { CWorkId = CWorkId });
                if (result.Succeeded) _notify.Information($"Case Work with ID {result.Data} Updated.");
                else _notify.Error(result.Message);
            }
            var response = await _mediator.Send(new GetAssignedWorkQuery { PageSize = 10, PageNumber = 1 });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CaseWorkingViewModel>>(response.Data);
                var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                _notify.Error(response.Message);
                return null;
            }
        }
    }
}
