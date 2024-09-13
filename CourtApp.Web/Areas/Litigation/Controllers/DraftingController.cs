using CourtApp.Application.Features.FormBuilder;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Admin.Models;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class DraftingController : BaseController<DraftingController>
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseDarftingQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<FormCaseMappingViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        public async Task<IActionResult> GetTemplateFields(Guid TemplateId, FormBuilderViewModel ViewModel)
        {
            var response = await _mediator.Send(new GetFormBuilderCachedByIdQuery()
            {
                Id = TemplateId
            });
            if (response.Succeeded)
            {
                var fieldPropeties = response.Data.FieldDetails;
                var Dt = _mapper.Map<List<FormProperties>>(fieldPropeties);
                ViewModel.FieldDetails = Dt;
                return PartialView("_FormFields", ViewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new FormBuilderViewModel();
                ViewModel.Templates = await PetTemplates();
                ViewModel.Cases = await UserCaseTitle();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                return null;
            }
        }

        public async Task<IActionResult> Petition(Guid id, FormBuilderViewModel ViewModel)
        {
            if (id == Guid.Empty)
            {
                ViewModel.Templates = await PetTemplates();
                ViewModel.Cases = await UserCaseTitle();
                return View("_CreateOrEdit", ViewModel);
            }
            else
            {
                var result = await _mediator.Send(new GetCaseDarftingCachedByIdQuery() { Id = id });
                if (result.Succeeded)
                {
                    ViewModel = _mapper.Map<FormBuilderViewModel>(result.Data);
                    ViewModel.Templates = await PetTemplates();
                    ViewModel.Cases = await UserCaseTitle();
                    ViewModel.FieldDetails = _mapper.Map<List<FormProperties>>(ViewModel.FieldDetails);
                    return View("_CreateOrEdit", ViewModel);
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid id, FormBuilderViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == Guid.Empty && TempData.ContainsKey("RecordExists") == false)
                    {
                        var Command = _mapper.Map<CreateCaseDraftingDetailCommand>(ViewModel);
                        var result = await _mediator.Send(Command);
                        if (result.Succeeded)
                            _notify.Success($"Case Drafting saved successfull!");
                        else
                        {
                            ViewModel.StatusMessage = result.Message;
                            ModelState.AddModelError(string.Empty, result.Message);
                            TempData["RecordExists"] = true;
                        }
                    }
                    else
                    {
                        var Command = _mapper.Map<UpdateCaseDraftingDetailCommand>(ViewModel);
                        var result = await _mediator.Send(Command);
                        if (result.Succeeded)
                            _notify.Success($"Case drafting information is updated successfully!");
                    }
                    ViewModel.Cases = await UserCaseTitle();
                    ViewModel.Templates = await PetTemplates();
                    return View("_CreateOrEdit", ViewModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html });
            }
        }
    }
}
