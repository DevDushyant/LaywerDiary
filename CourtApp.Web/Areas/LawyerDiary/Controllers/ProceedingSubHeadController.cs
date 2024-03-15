using Microsoft.AspNetCore.Mvc;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    public class ProceedingSubHeadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
