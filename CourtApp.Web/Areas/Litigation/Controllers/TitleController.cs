using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class TitleController : BaseController<TitleController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery(1, 100));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TitleGetViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
    }
}
