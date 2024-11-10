using AspNetCoreHero.Results;
using CourtApp.Application.Features.Lawyer;
using CourtApp.Application.Features.UserCase;
using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Admin.Models;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.LawyerDiary.Models.Lawyer;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class LawyerController : BaseController<LawyerController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public LawyerController(UserManager<ApplicationUser> _userManager)
        {
            this._userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new LawyerGetAllQuery() { PageNumber = 1, PageSize = 5000 });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<LawyerLViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new LawyerUpsertViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new LawyerGetByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<LawyerUpsertViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, LawyerUpsertViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<LawyerCreateCommand>(ViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                        _notify.Success($"Lawyer with ID {result.Data} Created.");
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<LawyerUpdateCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Lawyer with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new LawyerGetAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<LawyerLViewModel>>(response.Data);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new LawyerDeleteCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Lawyer with Id {id} Deleted.");
                var response = await _mediator.Send(new LawyerGetAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<LawyerLViewModel>>(response.Data);
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

        #region Bring Case Detail By Lawyer 
        public async Task<IActionResult> GetCaseByLawyer()
        {
            var ViewModel = new BringCaseViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id && a.EnrollmentNo != null).ToListAsync();
            var model = _mapper.Map<IEnumerable<UserViewModel>>(allUsersExceptCurrentUser);
            ViewModel.Lawyers = new SelectList(model, nameof(UserViewModel.Id), nameof(UserViewModel.FirstName), null, null); ;
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_BringCaseDetail", ViewModel) });
        }
        [HttpPost]
        public IActionResult OnPostCaseByLawyer(BringCaseViewModel vmodel)
        {           
            _notify.Success("Case detail fetched!");
            return RedirectToAction("BindLawyerCaseDetail", "CaseManage", new { area = "Litigation",id=vmodel.CaseId });
        }
        #endregion
    }
}
