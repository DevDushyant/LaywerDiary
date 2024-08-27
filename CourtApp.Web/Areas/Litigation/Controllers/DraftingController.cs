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
                var Dt = _mapper.Map<List<FormProperties>>(response.Data);
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

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, FormBuilderViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == Guid.Empty)
                    {
                        var Command = _mapper.Map<CreateCaseDraftingDetailCommand>(ViewModel);
                        var result = await _mediator.Send(Command);
                        if (result.Succeeded)
                            _notify.Success($"Case Drafting saved successfull!");
                        else _notify.Error(result.Message);
                    }
                    else
                    {
                        // var court = _mapper.Map<UpdateCourtMasterCommand>(btViewModel);
                        // var result = await _mediator.Send(court);
                        //if (result.Succeeded) 
                        _notify.Information($"Case kind with unique id  Updated.");
                    }
                    var response = await _mediator.Send(new GetFormBuilderCachedQuery());
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<GenFormAttrViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                        return new JsonResult(new { isValid = true, html = html });
                    }
                    else
                    {
                        _notify.Error(response.Message);
                        return null;
                    }
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
