using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    public class ProceedingHeadController : BaseController<ProceedingHeadController>
    {
        public IActionResult Index()
        {
            var model = new ProceedingHeadViewModel();
            return View(model);
        }

        public IActionResult LoadAll()
        {
            //var response = await _mediator.Send(new GetProceedingHeadCommand());
            //if (response.Succeeded)
            //{
            ProceedingHeadViewModel viewModel = new ProceedingHeadViewModel();
            //var viewModel = _mapper.Map<List<ProceedingHeadViewModel>>(response.Data);
            return PartialView("_ViewAll", viewModel);
            //}
            //return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {            
            if (Id == Guid.Empty)
            {
                var ViewModel = new ProceedingHeadViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                //var response = await _mediator.Send(new GetBookTypeByIdQuery() { Id = Id });
                //if (response.Succeeded)
                //{
                //    var brandViewModel = _mapper.Map<BookTypeViewModel>(response.Data);
                //    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                //}
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid Id, ProceedingHeadViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id==Guid.Empty)
                {
                    //var createBookTypeCommand = _mapper.Map<CreateBookTypeCommand>(viewModel);
                    //var result = await _mediator.Send(createBookTypeCommand);
                    //if (result.Succeeded)
                    //{
                    //    Id = Guid.NewGuid()//result.Data;
                    //    _notify.Success($"Proceeding Head with ID {result.Data} Created.");
                    //}
                    //else _notify.Error(result.Message);
                }
                else
                {
                    //var updateBookCommand = _mapper.Map<UpdateCreateBookTypeCommand>(viewModel);
                    //var result = await _mediator.Send(updateBookCommand);
                    //if (result.Succeeded) _notify.Information($"Book type with ID {result.Data} Updated.");
                }
                
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", viewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
            return new JsonResult(new { isValid = false, html = "" });
        }
    }
}
