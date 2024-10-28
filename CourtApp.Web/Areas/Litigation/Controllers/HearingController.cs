using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseProceeding;
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
        #region Case Hearing 
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoadAll(string seleDate)
        {
            if (seleDate == null)
                seleDate = DateTime.Now.Date.ToString();
            var response = await _mediator.Send(new GetCaseDetailsQuery()
            {
                CallingFrm = "HTD",
                HearingDate = Convert.ToDateTime(seleDate)
            });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<HearingViewModel>>(response.Data);
                return PartialView("_ViewAll", TodayCaseList(viewModel));
            }
            return null;
        }
        public async Task<JsonResult> LoadCaseData(string SDate)
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery()
            {
                CallingFrm = "HTD",
                HearingDate = Convert.ToDateTime(SDate)
            });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<HearingViewModel>>(response.Data);
                return Json(TodayCaseList(viewModel));
            }
            return null;
        }

        #endregion

        #region Bring Today Hearing Case
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

        #endregion

        #region Add the case in today's hearing register select and save.

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
        #endregion

        #region Assign Work to the case
        public async Task<JsonResult> AssignWorkCase(Guid CaseId)
        {
            var ViewModel = new CaseWorkingViewModel();
            ViewModel.WorkTypes = await DdlWorks();
            ViewModel.CaseId = CaseId;
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_AssignWork", ViewModel) });
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAssignWork(CaseWorkingViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var cmd = _mapper.Map<CreateCaseWorkCommand>(ViewModel);
                var result = await _mediator.Send(cmd);
                if (result.Succeeded)
                    _notify.Success($"Case work with ID {result.Data} Created.");
                else _notify.Error(result.Message);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Today's Proceeding to the case
        public async Task<JsonResult> CaseProceeding(Guid CaseId)
        {
            var model = new CaseProceedingViewModel();
            model.CaseId = CaseId;
            model.ProceedingTypes = await DdlProcHeads();
            model.Stages = await DdlCaseStages();
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CaseProceeding", model) });
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCaseProceeding(CaseProceedingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cmd = _mapper.Map<CreateCaseProceedingCommand>(model);
                var result = await _mediator.Send(cmd);
                if (result.Succeeded)
                    _notify.Success($"Case proceeding with ID {result.Data} Created.");
                else _notify.Error(result.Message);
            }
            return RedirectToAction("Index");
            #endregion
        }
    }
}
