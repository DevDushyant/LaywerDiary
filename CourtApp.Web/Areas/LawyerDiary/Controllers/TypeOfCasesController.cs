using CourtApp.Application.Features.CaseNatures.Query;
using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Application.Features.TypeOfCases.Query;
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
    public class TypeOfCasesController : BaseController<TypeOfCasesController>
    {
        public IActionResult Index()
        {
            var model = new TypeOfCasesViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllTypeOfCasesQuery(1, 100));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TypeOfCasesViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var CaseNatures = await _mediator.Send(new CaseNatureByAllCachedQuery());
            if (id == 0)
            {
                var ViewModel = new TypeOfCasesViewModel();
                if (CaseNatures.Succeeded)
                {
                    var bookTypeViewModel = _mapper.Map<List<CaseNatureViewModel>>(CaseNatures.Data);
                    ViewModel.CaseNatures = new SelectList(bookTypeViewModel, nameof(CaseNatureViewModel.Id), nameof(CaseNatureViewModel.CaseNature), null, null);
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new TypeOfCasesByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<TypeOfCasesViewModel>(response.Data);
                    if (CaseNatures.Succeeded)
                    {
                        var bookTypeViewModel = _mapper.Map<List<CaseNatureViewModel>>(CaseNatures.Data);
                        brandViewModel.CaseNatures = new SelectList(bookTypeViewModel, nameof(CaseNatureViewModel.Id), nameof(CaseNatureViewModel.CaseNature), null, null);
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, TypeOfCasesViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createTypeofCasesCommand = _mapper.Map<CreateTypeOfCasesCommand>(btViewModel);
                    var result = await _mediator.Send(createTypeofCasesCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Case type with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateBookCommand = _mapper.Map<UpdateTypeOfCasesCommand>(btViewModel);
                    var result = await _mediator.Send(updateBookCommand);
                    if (result.Succeeded) _notify.Information($"Case kind with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllTypeOfCasesQuery(1, 100));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TypeOfCasesViewModel>>(response.Data);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", btViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteTypeOfCasesCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Case Nature with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllTypeOfCasesQuery(1, 100));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TypeOfCasesViewModel>>(response.Data);
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
