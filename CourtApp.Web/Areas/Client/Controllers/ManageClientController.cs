using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.Queries.Districts;
using CourtApp.Application.Features.Queries.States;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Client")]
    public class ManageClientController : BaseController<ManageClientController>
    {

        public IActionResult Index()
        {
            var model = new GClientViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllClientCachedQuery() { });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GClientViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }        
        public async Task<IActionResult> CreateOrUpdateAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new ClientViewModel();
                await BindDropdownAsync(ViewModel);
                return View("_CreateOrEdit", ViewModel);
            }
            else
            {
                var response = await _mediator.Send(new GetClientByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var districtList = await _mediator.Send(new GetDistrictQuery() { StateCode = response.Data.StateCode });
                    var ViewModel = _mapper.Map<ClientViewModel>(response.Data);
                    await BindDropdownAsync(ViewModel);
                    return View("_CreateOrEdit", ViewModel);
                }
                return null;
            }
        }
        private async Task BindDropdownAsync(ClientViewModel ViewModel)
        {

            var statelist = await _mediator.Send(new GetStateMasterQuery());
            if (statelist.Succeeded)
            {
                var DdlStates = _mapper.Map<List<StateViewModel>>(statelist.Data);
                ViewModel.States = new SelectList(DdlStates, nameof(StateViewModel.Code), nameof(StateViewModel.Name_En), null, null);
            }

            var DistrictList = await _mediator.Send(new GetDistrictQuery { StateCode = ViewModel.StateCode });
            if (DistrictList.Succeeded)
            {
                var DdlDistrict = _mapper.Map<List<DistrictViewModel>>(DistrictList.Data);
                ViewModel.Districts = new SelectList(DdlDistrict, nameof(DistrictViewModel.Code), nameof(DistrictViewModel.Name_En), null, null);

            }

        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, ClientViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createClientCommand = _mapper.Map<CreateClientCommand>(btViewModel);
                    var result = await _mediator.Send(createClientCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Client with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateClientCommand = _mapper.Map<UpdateClientCommand>(btViewModel);
                    var result = await _mediator.Send(updateClientCommand);
                    if (result.Succeeded) _notify.Information($"Client with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllClientCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ClientViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteClientCommand { Id = Id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Client with Id {Id} Deleted.");
                var response = await _mediator.Send(new GetAllClientCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ClientViewModel>>(response.Data);
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
