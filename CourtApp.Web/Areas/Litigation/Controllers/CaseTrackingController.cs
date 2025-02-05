using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using CourtApp.Application.Constants;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.DOType;
using CourtApp.Application.Features.Queries.States;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.Litigation.Models;
using CourtApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class CaseTrackingController : BaseController<CaseManageController>
    {
        public async Task<IActionResult> Index()
        {
            List<GetCaseViewModel> m = new List<GetCaseViewModel>();
            return View(m);
        }
        public async Task<IActionResult> LoadAll(string type, string value)
        {
            var response = await _mediator.Send(new GetCaseSearchQuery()
            {
                Type = type,
                Value = value,
                PageSize = 10000,
                PageNumber = 1,
                UserId = CurrentUser.Id
            });

            if (response.Succeeded)
            {
                var viewmodel = _mapper.Map<List<CaseSearchViewModel>>(response.Data);
                return PartialView("_ViewAll", viewmodel);
            }
            return null;

        }
    }
}