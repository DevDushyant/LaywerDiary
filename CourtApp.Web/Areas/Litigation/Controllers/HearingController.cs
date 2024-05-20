using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseStages.Command;
using CourtApp.Application.Features.CaseWork;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class HearingController : BaseController<HearingController>
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery() { CallingFrm = "HTD" });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<HearingViewModel>>(response.Data);
                return PartialView("_ViewAll", TodayCaseList(viewModel));
            }
            return null;
        }
        private BringTodayCaseViewModel TodayCaseList(dynamic viewModel)
        {
            var model = new BringTodayCaseViewModel();
            List<HearingViewModel> cdt = new List<HearingViewModel>();
            foreach (var item in viewModel)
            {
                var hearing = new HearingViewModel();
                hearing.Id = item.Id;
                hearing.CaseTitle = item.CaseTitle;
                cdt.Add(hearing);
            }
            model.CaseList = cdt;
            return model;
        }

        public async Task<JsonResult> GetCaseHearing()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery() { CallingFrm = "BTD" });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseViewModel>>(response.Data);
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_BringToday", TodayCaseList(viewModel)) });
            }
            return null;
        }

        public async Task<IActionResult> UpdateCaseDate(BringTodayCaseViewModel model)
        {
            if (model.CaseList != null)
            {
                var CaseIds = model.CaseList.Where(s => s.Selected == true).Select(s => s.Id).ToList();
                var result = await _mediator.Send(new UpdateCaseNextDateCommand { CaseIds = CaseIds });
                if (result.Succeeded) _notify.Information($"Case Next hearing date with ID {result.Data} Updated.");
                else _notify.Error(result.Message);
            }
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> AssignWorkCase(Guid CaseId)
        {
            var ViewModel = new CaseWorkingViewModel();
            ViewModel.WorkTypes = await DdlWorks();
            ViewModel.CaseId = CaseId;
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_AssignWork", ViewModel) });
        }

        [HttpPost]
        public async Task<JsonResult> OnPostAssignWork(Guid id, CaseWorkingViewModel ViewModel)
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

    }
}
