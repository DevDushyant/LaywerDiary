using CourtApp.Web.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CourtApp.Web.Areas.Admin.Models;
using CourtApp.Application.Features.FormBuilder;
using CourtApp.Application.DTOs.FormBuilder;
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
                var result = await _mediator.Send(new GetFormBuilderCachedByIdQuery() { Id = id });
                if (result.Succeeded)
                {
                    var ViewModel = _mapper.Map<GenerateFormViewModel>(result.Data);
                    FormViewModel fm = new FormViewModel();
                    fm.Fields = _mapper.Map<List<FormFields>>(result.Data.FieldDetails);
                    ViewModel.Form = fm;
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
            }
            return null;
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
                        List<FieldDetailsDto> ff = new List<FieldDetailsDto>();
                        foreach (var f in ViewModel.Form.Fields)
                        {
                            FieldDetailsDto fdd = new FieldDetailsDto();
                            fdd.Key = Guid.NewGuid();
                            fdd.Name = f.Name;
                            fdd.Type = f.Type;
                            ff.Add(fdd);
                        }
                        FormFieldsDto formFieldsDto = new FormFieldsDto();
                        formFieldsDto.Fields = ff;
                        Command.Form = formFieldsDto;
                        var result = await _mediator.Send(Command);
                        if (result.Succeeded)
                            _notify.Success($"Template data fields saved successfully!");
                        else _notify.Error(result.Message);
                    }
                    else
                    {
                        var court = _mapper.Map<UpdateFormBuilderCommand>(ViewModel);
                        var result = await _mediator.Send(court);
                        if (result.Succeeded)
                            _notify.Information($"Template Attribute updated successfully.");
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

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteFormBuilderQueryCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Selected template is deleted successfully!");
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
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }

    }
}
