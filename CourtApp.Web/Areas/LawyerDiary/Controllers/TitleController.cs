using CourtApp.Application.Features.CaseTitle;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Web.Abstractions;
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
            var response = await _mediator.Send(new GetCaseTitleQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TitleGetViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnCreatOrUpdateTitle(Guid id)
        {
            if (id == Guid.Empty)
            {
                var viewModel = new TitleViewModel();
                var ApplicantDetails = new List<ApplicantDetailViewModel>();
                ApplicantDetails.Add(new ApplicantDetailViewModel { ApplicantNo = 1, ApplicantDetail = "" });
                viewModel.Types = FSTypes();
                viewModel.CaseApplicants = ApplicantDetails;
                viewModel.Cases = await UserCaseTitle();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrUpdate", viewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetCaseTitleByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<TitleViewModel>(response.Data);
                    ViewModel.Types = FSTypes();
                    ViewModel.Cases = await UserCaseTitle();
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrUpdate", ViewModel) });
                }
                return null;
            }
        }
        [HttpPost]
        public async Task<JsonResult> OnCreatOrUpdateTitle(Guid id, TitleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var mapper = _mapper.Map<CreateCaseTitleCommand>(model);
                    mapper.CaseApplicants = _mapper.Map<List<CaseApplicantDetail>>(model.CaseApplicants);
                    var result = await _mediator.Send(mapper);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Case Title with ID {result.Data} Created.");
                    }
                    else
                        _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateCaseTitleCommand>(model);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Case Title ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetCaseTitleQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TitleGetViewModel>>(response.Data);
                    var chtml = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = chtml });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrUpdate", model);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnTitleDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteCaseTitleCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Complete title with Id {id} deleted successfully");
                var response = await _mediator.Send(new GetCaseTitleQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TitleGetViewModel>>(response.Data);
                    var chtml = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = chtml });
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
        public async Task<JsonResult> OnGetFSCreateOrUpdate(Guid id)
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
        public async Task<JsonResult> OnPostFSCreateOrUpdate(Guid id, FSTitleMViewModel model)
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
                    else
                        _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<FSTitleUpdateCommand>(model);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"First & Second with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new FSTitleGetAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<FSTitleLViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_FSTitles", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                return null;
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
                _notify.Information($"Title with Id {id} Deleted.");
                var response = await _mediator.Send(new FSTitleGetAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<FSTitleLViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_FSTitles", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
            return null;
        }

        private async Task<JsonResult> LoadFSDataAsync()
        {
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

        #endregion
    }
}
