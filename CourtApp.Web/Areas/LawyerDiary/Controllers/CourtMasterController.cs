using CourtApp.Application.Constants;
using CourtApp.Application.Features.CourtMasters.Command;
using CourtApp.Application.Features.CourtMasters.Query;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.Districts.Queries;
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
    public class CourtMasterController : BaseController<CourtMasterController>
    {
        public IActionResult Index()
        {
            var model = new CourtMasterViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCourtMasterAllQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CourtMasterViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        private async Task BindDropdownAsync(CourtMasterViewModel ViewModel)
        {
            var statelist = await _mediator.Send(new GetStateMasterQuery());
            var courtTypeList = await _mediator.Send(new GetCourtTypeQuery());
            if (statelist.Succeeded)
            {
                var DdlStates = _mapper.Map<List<StateViewModel>>(statelist.Data);
                ViewModel.States = new SelectList(DdlStates, nameof(StateViewModel.StateCode), nameof(StateViewModel.StateName), null, null);
}
            if (courtTypeList.Succeeded)
            {
                var DdlCourtTypes = _mapper.Map<List<CourtTypeViewModel>>(courtTypeList.Data);
                ViewModel.CourtTypes = new SelectList(DdlCourtTypes, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null); ;

            }
        }

        public async Task<IActionResult> CreateOrUpdateAsync(Guid? id = null)
        {
            
            if (id == null)
            {
                var ViewModel = new CourtMasterViewModel();
                await BindDropdownAsync(ViewModel);
                return View("_CreateOrEdit", ViewModel);
            }
            else
            {
                var response = await _mediator.Send(new GetCourtMasterDataByIdQuery() { Id = id.Value });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<CourtMasterViewModel>(response.Data);
                    await BindDropdownAsync(ViewModel);
                    return View("_CreateOrEdit", ViewModel);
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CourtMasterViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    var createBookTypeCommand = _mapper.Map<CreateCourtMasterCommand>(btViewModel);
                    var result = await _mediator.Send(createBookTypeCommand);
                    if (result.Succeeded)
                        _notify.Success($"Case type with ID {result.Data} Created.");
                    else _notify.Error(result.Message);
                }
                else
                {
                    var court = _mapper.Map<UpdateCourtMasterCommand>(btViewModel);
                    var result = await _mediator.Send(court);
                    if (result.Succeeded) _notify.Information($"Case kind with unique id {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetCourtMasterAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CourtMasterViewModel>>(response.Data);
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

        //[HttpPost]
        //public async Task<JsonResult> OnPostDelete(Guid id)
        //{
        //    var deleteCommand = await _mediator.Send(new DeleteCourtMasterCommand { UniqueId = id });
        //    if (deleteCommand.Succeeded)
        //    {
        //        _notify.Information($"Case Nature with Id {id} Deleted.");
        //        var response = await _mediator.Send(new GetCourtMasterAllQuery());
        //        if (response.Succeeded)
        //        {
        //            var viewModel = _mapper.Map<List<CourtMasterViewModel>>(response.Data);
        //            var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
        //            return new JsonResult(new { isValid = true, html = html });
        //        }
        //        else
        //        {
        //            _notify.Error(response.Message);
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        _notify.Error(deleteCommand.Message);
        //        return null;
        //    }
        //}
    }
}
