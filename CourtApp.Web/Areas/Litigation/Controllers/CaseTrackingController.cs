using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Application.Constants;
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
            var viewModel = new SearchViewModel();
            viewModel.SearchTypes = new SelectList(StaticDropDownDictionaries.CaseSearchBy(), "Key", "Value");
            //viewModel.CourtTypes = new SelectList(StaticDropDownDictionaries.CourtTypeDictionary(), "Key", "Value");
            var statelist = (await _mediator.Send(new GetStateMasterQuery())).Data;
            if (statelist != null)
            {
                var stateViewModel = _mapper.Map<List<StateViewModel>>(statelist);
                viewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.Code), nameof(StateViewModel.Name_En), null, null);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CaseSearch(SearchViewModel obj)
        {
            List<CaseViewModel> caseViewModels = new List<CaseViewModel>();
            var viewModel = caseViewModels;
            return PartialView("_ViewAll", viewModel);
        }
        public IActionResult LoadAll()
        {
            List<CaseViewModel> caseViewModels = new List<CaseViewModel>();
            var viewModel = caseViewModels;
            return PartialView("_ViewAll", viewModel);
        }

    }
}