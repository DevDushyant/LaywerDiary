using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Web.Abstractions;
using CourtApp.Application.Features.ProceedingSubHead;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using CourtApp.Web.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class ProceedingSubHeadController : BaseController<ProceedingSubHeadController>
    {
        public IActionResult Index()
        {
            var model = new ProceedingSubHeadViewModel();
            return View(model);
        }
        public async Task<IActionResult> LoadAllAsync(int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetProceedingSubHeadQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            if (response.Succeeded)
            {
                var result = _mapper.Map<List<ProceedingSubHeadViewModel>>(response.Data);
                var viewModel = new PaginationViewModel<ProceedingSubHeadViewModel>();
                viewModel.Data = result;
                viewModel.HasPreviousPage = response.HasPreviousPage;
                viewModel.HasNextPage = response.HasNextPage;
                viewModel.TotalPages = response.TotalPages;
                viewModel.TotalCount = response.TotalCount;
                viewModel.PageSize = 10;
                viewModel.PageNumber = 1;
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                var ViewModel = new ProceedingSubHeadViewModel();
                ViewModel.PHeads = await DdlProcHeads();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetProceedingSubHeadGetByIdQuery() { Id = Id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<ProceedingSubHeadViewModel>(response.Data);
                    ViewModel.PHeads = await DdlProcHeads();
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid Id, ProceedingSubHeadViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    var cmd = _mapper.Map<CreateProcSubHeadCommand>(viewModel);
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
                    var cmd = _mapper.Map<UpdateProcSubHeadCommand>(viewModel);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded) _notify.Information($"Proceeding Head with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetProceedingSubHeadQuery());
                if (response.Succeeded)
                {
                    var btviewModel = _mapper.Map<List<ProceedingSubHeadViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", btviewModel);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", viewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteProcSubHeadCommand { Id = id });

            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Proc sub Head with ID {id} Deleted.");
                var response = await _mediator.Send(new GetProceedingSubHeadQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ProceedingSubHeadViewModel>>(response.Data);
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
