using CourtApp.Application.Features.DOType;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.LawyerDiary.Models.Title;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class TitleController : BaseController<TitleController>
    {
        #region Detail Title 
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TitleGetViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        #endregion 

        #region First & Secound Title 
        public IActionResult FSTitle()
        {
            return View();
        }
        public async Task<IActionResult> LoadFSTitle()
        {
            var response = await _mediator.Send(new FSTitleGetAllQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<FSTitleLViewModel>>(response.Data);
                return PartialView("_FSTitles", viewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var viewModel = new FSTitleMViewModel();
                viewModel.FSTypes = FSTypes();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateUpdateFSTitle", viewModel) });
            }
            else
            {
                var response = await _mediator.Send(new FSTitleGetByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<FSTitleMViewModel>(response.Data);
                    ViewModel.FSTypes = FSTypes();
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateUpdateFSTitle", ViewModel) });
                }
                return null;
            }
        }
        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, FSTitleMViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var cmd = _mapper.Map<FSTitleCreateCommand>(model);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"First & Second with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<FSTitleUpdateCommand>(model);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded) _notify.Information($"First & Second with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new FSTitleGetAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<FSTitleLViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_FSTitles", viewModel);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateUpdateFSTitle", model);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new FSTitleDeleteCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Draft & Order with Id {id} Deleted.");
                var response = await _mediator.Send(new FSTitleGetAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<FSTitleLViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_FSTitles", viewModel);
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
        #endregion
    }
}
