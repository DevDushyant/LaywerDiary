﻿using CourtApp.Application.Features.Cadre;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Web.Abstractions;
using Microsoft.AspNetCore.Mvc;
using CourtApp.Web.Areas.LawyerDiary.Models.CourtForm;
using CourtApp.Application.Features.CourtForm;
using System;
using CourtApp.Application.Features.Languages;
using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{

    [Area("LawyerDiary")]
    public class CourtFormController : BaseController<CourtFormController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new CourtFormGetAllQuery { StateId = 0 });

            var modelList = response.Succeeded
                ? _mapper.Map<List<CourtFormGetViewModel>>(response.Data)
                : new List<CourtFormGetViewModel>();

            return PartialView("_ViewAll", modelList);
        }


        public async Task<JsonResult> LoadLanguages(int StateId)
        {
            var dt = await _mediator.Send(new LangaugeAllQuery() { StateId = StateId });
            if (dt.Succeeded)
            {
                var lans = dt.Data;
                var data = Json(lans);
                return data;
            }
            return null;
        }

        private async Task PopulateDropdownsAsync(CourtFormAddUpdateViewModel viewModel)
        {
            viewModel.States = await LoadStates();
            viewModel.CourtTypes = await LoadCourtTypes();
            viewModel.CaseCategory = await LoadCaseNature();
            if (viewModel.StateId != 0)
            {
                var dt = await _mediator.Send(new LangaugeAllQuery() { StateId = viewModel.StateId });
                viewModel.Languages = new SelectList(dt.Data, "Code", "Name");
            }
        }

        [HttpGet]
        public async Task<IActionResult> OnGetCreateOrEdit(Guid id)
        {
            var viewModel = new CourtFormAddUpdateViewModel();
            if (id == Guid.Empty)
                await PopulateDropdownsAsync(viewModel);
            else
            {
                // Edit mode
                var response = await _mediator.Send(new GetCourtFormByIdQuery { Id = id });
                if (!response.Succeeded)
                    return null;

                viewModel = _mapper.Map<CourtFormAddUpdateViewModel>(response.Data);
                await PopulateDropdownsAsync(viewModel);
            }

            //var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", viewModel);
            //return new JsonResult(new { isValid = true, html });


            return View("_CreateOrEdit", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid id, CourtFormAddUpdateViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html });
            }

            IResult result;

            if (id == Guid.Empty)
            {
                var createCommand = _mapper.Map<CreateCourtFormCommand>(ViewModel);
                result = await _mediator.Send(createCommand);
            }
            else
            {
                var updateCommand = _mapper.Map<UpdateCourtFormCommand>(ViewModel);
                result = await _mediator.Send(updateCommand);
            }

            if (!result.Succeeded)
                _notify.Error(result.Message);
            else
                _notify.Information(result.Message);
            ViewModel = ViewModel.Id == Guid.Empty ? new CourtFormAddUpdateViewModel() : ViewModel;
            await PopulateDropdownsAsync(ViewModel);
            return View("_CreateOrEdit", ViewModel);

            // Load updated list after successful create/update
            //var response = await _mediator.Send(new CourtFormGetAllQuery());

            //if (!response.Succeeded)
            //{
            //    _notify.Error(response.Message);
            //    return null;
            //}

            //var viewModel = _mapper.Map<List<CourtFormGetViewModel>>(response.Data);
            //var viewHtml = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);

            //return new JsonResult(new { isValid = true, html = viewHtml });

        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            if (id == Guid.Empty)
                return new JsonResult(new { isValid = false, message = "Invalid record ID." });

            var result = await _mediator.Send(new DeleteCourtFormCommand { Id = id });

            if (!result.Succeeded)
                return new JsonResult(new { isValid = false, message = result.Message });

            // Refresh list after successful delete
            var response = await _mediator.Send(new CourtFormGetAllQuery { StateId = 0 });

            var viewModel = response.Succeeded
                ? _mapper.Map<List<CourtFormGetViewModel>>(response.Data)
                : new List<CourtFormGetViewModel>();

            var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);

            return new JsonResult(new { isValid = true, html, message = "Record deleted successfully." });
        }


    }
}
