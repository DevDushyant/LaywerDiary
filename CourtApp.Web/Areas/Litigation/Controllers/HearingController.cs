using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
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
                return PartialView("_ViewAll", model);
            }
            return null;
        }

        public async Task<JsonResult> GetCaseHearing()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery() { CallingFrm = "BTD" });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseViewModel>>(response.Data);
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
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_BringToday", model) });
            }
            return null;
        }

        public async Task<IActionResult> UpdateCaseDate(BringTodayCaseViewModel model)
        {
            if (model.CaseList != null)
            {
                var CaseIds = model.CaseList.Select(s => s.Id).ToList();
                var result = await _mediator.Send(new UpdateCaseNextDateCommand { CaseIds = CaseIds });
                if (result.Succeeded) _notify.Information($"Case Next hearing date with ID {result.Data} Updated.");
                else _notify.Error(result.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
