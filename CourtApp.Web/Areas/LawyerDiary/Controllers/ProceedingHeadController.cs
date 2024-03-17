using CourtApp.Application.Enums;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class ProceedingHeadController : BaseController<ProceedingHeadController>
    {
        public IActionResult Index()
        {
            var model = new ProceedingHeadViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAllAsync()
        {
            var response = await _mediator.Send(new GetProceedingHeadCommand());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<ProceedingHeadViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                var ViewModel = new ProceedingHeadViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetProceedingHeadCommand() { Id = Id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<ProceedingHeadViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid Id, ProceedingHeadViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    var cmd = _mapper.Map<ProceedingHeadCommand>(viewModel);
                    cmd.ActionType = ((int)ActionTypes.Add);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                    {
                        Id = result.Data;
                        _notify.Success($"Proceeding Head with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {                   
                    var cmd = _mapper.Map<ProceedingHeadCommand>(viewModel);
                    cmd.ActionType = ((int)ActionTypes.Update);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded) _notify.Information($"Proceeding Head with ID {result.Data} Updated.");
                }

            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", viewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
            return new JsonResult(new { isValid = false, html = "" });
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new ProceedingHeadCommand { Id = id,ActionType= ((int)ActionTypes.Update) });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Proceeding Head with ID {id} Deleted.");
                var response = await _mediator.Send(new GetProceedingHeadCommand());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ProceedingHeadViewModel>>(response.Data);
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
