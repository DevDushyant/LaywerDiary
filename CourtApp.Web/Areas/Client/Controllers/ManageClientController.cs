using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Application.Features.Queries.Districts;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Client.Model;
using Microsoft.AspNetCore.Mvc;
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
                ViewModel.OppositCounsels =await DdlLawyerAsync();
                ViewModel.Appearences = await DdlFSTypes(0);                
                return View("_CreateOrEdit", ViewModel);
            }
            else
            {
                var response = await _mediator.Send(new GetClientByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<ClientViewModel>(response.Data);
                    ViewModel.OppositCounsels = await DdlLawyerAsync();
                    ViewModel.Appearences = await DdlFSTypes(0);
                    return View("_CreateOrEdit", ViewModel);
                }
                return null;
            }
        }       

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid id, ClientViewModel btViewModel)
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
                        btViewModel.StatusMessage = "Record created successfully";
                    }
                    else _notify.Error(result.Message);
                    return View("_CreateOrEdit", btViewModel);
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

        public async Task<JsonResult> OnGetCreateOrEdit(Guid CaseId)
        {
            var ViewModel = new ClientViewModel();
            ViewModel.CaseId = CaseId;
            ViewModel.OppositCounsels = await DdlLawyerAsync();
            ViewModel.Appearences = await DdlFSTypes(0);
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
        }
    }
}
