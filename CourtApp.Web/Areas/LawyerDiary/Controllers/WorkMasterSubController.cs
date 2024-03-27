using CourtApp.Application.Enums;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Application.Features.WorkMasterSub;
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
    public class WorkMasterSubController : BaseController<WorkMasterSubController>
    {
        public IActionResult Index()
        {
            var model = new WorkMasterSubViewModel();
            return View(model);
        }
        public async Task<IActionResult> LoadAllAsync()
        {
            var response = await _mediator.Send(new GetWorkMasterSubCommand());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<WorkMasterSubViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {
            var wMasterList = await _mediator.Send(new GetWorkMasterCommand());
            if (Id == Guid.Empty)
            {
                var ViewModel = new WorkMasterSubViewModel();
                var wMasterViewModel = _mapper.Map<List<WorkMasterViewModel>>(wMasterList.Data);
                ViewModel.WMasters = new SelectList(wMasterViewModel, nameof(WorkMasterViewModel.Id), nameof(WorkMasterViewModel.Work_En), null, null);
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetWorkMasterSubCommand() { Id = Id });
                if (response.Succeeded)
                {
                    var data = response.Data.Where(o => o.Id == Id).FirstOrDefault();
                    var brandViewModel = _mapper.Map<WorkMasterSubViewModel>(data);
                    var wMasterViewModel = _mapper.Map<List<WorkMasterViewModel>>(wMasterList.Data);
                    brandViewModel.WMasters = new SelectList(wMasterViewModel, nameof(WorkMasterViewModel.Id), nameof(WorkMasterViewModel.Work_En), null, null);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid Id, WorkMasterSubViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    try
                    {
                        var cmd = _mapper.Map<WorkMasterSubCommand>(viewModel);
                        cmd.ActionType = ((int)ActionTypes.Add);
                        var result = await _mediator.Send(cmd);
                        if (result.Succeeded)
                        {
                            Id = result.Data;
                            _notify.Success($"Work Master Sub with ID {result.Data} Created.");
                            var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                            return new JsonResult(new { isValid = true, html = html });

                        }
                        else _notify.Error(result.Message);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    var cmd = _mapper.Map<WorkMasterSubCommand>(viewModel);
                    cmd.ActionType = ((int)ActionTypes.Update);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                    {
                        _notify.Information($"Work Master with ID {result.Data} Updated.");
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                        return new JsonResult(new { isValid = true, html = html });
                    }
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
            var deleteCommand = await _mediator.Send(new WorkMasterSubCommand { Id = id, ActionType = ((int)ActionTypes.Update) });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Work Master Sub with ID {id} Deleted.");
                var response = await _mediator.Send(new GetWorkMasterSubCommand());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<WorkMasterSubViewModel>>(response.Data);
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
