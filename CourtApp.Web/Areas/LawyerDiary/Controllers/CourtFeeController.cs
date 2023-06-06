using CourtApp.Application.Features.BookMasters.Queries;
using CourtApp.Application.Features.CourtFeeStructure.Command;
using CourtApp.Application.Features.CourtFeeStructure.Queries;
using CourtApp.Application.Features.States.Queries;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class CourtFeeController : BaseController<CourtFeeController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAllAsync()
        {
            var response = await _mediator.Send(new GetCourtFeeStructAllQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CourtFeeStructureViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<IActionResult> CreateOrUpdate(Guid? id = null)
        {
            var ViewModel = new CourtFeeStructureViewModel();
            if (id != null)
            {
                var response = await _mediator.Send(new GetCourtFeeStructByIdQuery() { Id = id.Value });
                if (response.Succeeded == true)
                    ViewModel = _mapper.Map<CourtFeeStructureViewModel>(response.Data);
            }
            var statelist = await _mediator.Send(new GetStateMasterQuery());
            var stateViewModel = _mapper.Map<List<StateViewModel>>(statelist.Data);
            ViewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.StateCode), nameof(StateViewModel.StateName), null, null);
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CourtFeeStructureViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    var createCommand = _mapper.Map<CreateCourtFeeStructureCommand>(ViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                        _notify.Success($"Court Fee structure  with ID {result.Data} created");
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateCourtFeeStructureCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Court Fee structure updated with ID {result.Data}");
                }
                var response = await _mediator.Send(new GetCourtFeeStructAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CourtFeeStructureViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteCourtFeeStructureCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Court fee structure with Id {id} Deleted.");
                var response = await _mediator.Send(new GetCourtFeeStructAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CourtFeeStructureViewModel>>(response.Data);
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
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}
