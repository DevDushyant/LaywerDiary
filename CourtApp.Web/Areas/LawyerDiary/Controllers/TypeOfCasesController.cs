using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Application.Features.TypeOfCases.Query;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Application.Features.CaseCategory;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.Queries.States;

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
        private async Task BindDropdownAsync(TypeOfCasesViewModel ViewModel)
        {
            var courtTypeList = await _mediator.Send(new GetCourtTypeQuery());
            if (courtTypeList.Succeeded)
            {
                var DdlCourtTypes = _mapper.Map<List<CourtTypeViewModel>>(courtTypeList.Data);
                ViewModel.CourtTypes = new SelectList(DdlCourtTypes, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null); ;
            }
            var statelist = await _mediator.Send(new GetStateMasterQuery());
            if (statelist.Succeeded)
            {
                var DdlStates = _mapper.Map<List<StateViewModel>>(statelist.Data);
                ViewModel.States = new SelectList(DdlStates, nameof(StateViewModel.Code), nameof(StateViewModel.Name_En), null, null);
            }           

        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllTypeOfCasesQuery(1, 200));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TypeOfCasesViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {

            if (Id == Guid.Empty)
            {
                var ViewModel = new TypeOfCasesViewModel();
                await BindDropdownAsync(ViewModel);
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new TypeOfCasesByIdQuery() { Id = Id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<TypeOfCasesViewModel>(response.Data);
                    await BindDropdownAsync(brandViewModel);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, TypeOfCasesViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
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
        public async Task<JsonResult> OnPostDelete(Guid Id)
        {
            var deleteCommand = await _mediator.Send(new DeleteTypeOfCasesCommand { Id = Id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Case Nature with Id {Id} Deleted.");
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
