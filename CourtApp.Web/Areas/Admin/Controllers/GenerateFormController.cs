using CourtApp.Web.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CourtApp.Web.Areas.Admin.Models;
using CourtApp.Application.Features.FormBuilder;

namespace CourtApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenerateFormController : BaseController<GenerateFormController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetFormBuilderCachedQuery());            
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GenFormAttrViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;            
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new GenerateFormViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, GenerateFormViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == Guid.Empty)
                    {
                        var Command = _mapper.Map<CreateFormBuilderCommand>(ViewModel);
                        var result = await _mediator.Send(Command);
                        if (result.Succeeded)
                            _notify.Success($"Template data fields saved successfully!");
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
