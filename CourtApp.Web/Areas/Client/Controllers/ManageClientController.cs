using CourtApp.Application.Features.CaseNatures.Query;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Application.Features.Districts.Queries;
using CourtApp.Application.Features.States.Queries;
using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Client")]
    public class ManageClientController : BaseController<ManageClientController>
    {
        
        public IActionResult Index()
        {
            var model = new ClientsViewModel();
            return View(model); 
        }

         public async Task<IActionResult> LoadAll()
        {
           
            var response = await _mediator.Send(new GetAllClientCachedQuery() { });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<ClientsViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var statelist = await _mediator.Send(new GetStateMasterQuery());
            if (id == 0)
            {
                var ViewModel = new ClientsViewModel();
                var stateViewModel = _mapper.Map<List<StateViewModel>>(statelist.Data);
                ViewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.StateCode), nameof(StateViewModel.StateName), null, null);
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                //var response = await _mediator.Send(new CaseNatureByIdQuery() { Id = id });
                //if (response.Succeeded)
                //{
                //    var brandViewModel = _mapper.Map<CaseNatureViewModel>(response.Data);
                //    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                //}
                return null;
            }
        }
        public async Task<IActionResult> CreateOrUpdateAsync(int id = 0)
        {
            var statelist = await _mediator.Send(new GetStateMasterQuery());
            
            if (id == 0)
            {
                var ViewModel = new ClientsViewModel();
                var stateViewModel = _mapper.Map<List<StateViewModel>>(statelist.Data);              
                ViewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.StateCode), nameof(StateViewModel.StateName), null, null);
                return View("_CreateOrEdit", ViewModel);
            }
            else
            {
                var response = await _mediator.Send(new GetClientByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var districtList = await _mediator.Send(new GetDistrictQuery() { StateCode=response.Data.StateCode});
                    var viewmodel=_mapper.Map<ClientsViewModel>(response.Data);
                    var stateViewModel = _mapper.Map<List<StateViewModel>>(statelist.Data);
                    var districtViewModel = _mapper.Map<List<DistrictViewModel>>(districtList.Data);
                    viewmodel.States= new SelectList(stateViewModel, nameof(StateViewModel.StateCode), nameof(StateViewModel.StateName), null, null);
                    viewmodel.Districts = new SelectList(districtViewModel, nameof(DistrictViewModel.DistrictCode), nameof(DistrictViewModel.DistrictName), null, null);
                    return View("_CreateOrEdit", viewmodel);
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, ClientsViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
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
                    var updateClientCommand = _mapper.Map<UpdateCreateClientCommand>(btViewModel);
                    var result = await _mediator.Send(updateClientCommand);
                    if (result.Succeeded) _notify.Information($"Client with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllClientCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ClientsViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteCreateClientCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Client with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllClientCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ClientsViewModel>>(response.Data);
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
