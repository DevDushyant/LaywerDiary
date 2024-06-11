using CourtApp.Application.Features.Registers;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Report.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Report")]
    public class RegisterController : BaseController<RegisterController>
    {
        #region Work Register Area
        public IActionResult DispCopyRegister(string t)
        {
            if (t == "d")
            {
                ViewBag.Title = "Disposal Register";
                ViewBag.Caption = "User's disposal register";
            }
            else
            {
                ViewBag.Title = "Copying Register";
                ViewBag.Caption = "User's copying register";

            }
            ViewBag.type = t;
            return View();
        }
        public async Task<IActionResult> LoadDisposalCasesAsync(string type)
        {
            var response = await _mediator.Send(new RegistCopyDisposalQuery()
            {
                PageNumber = 1,
                PageSize = 100,
                FromDt = Convert.ToDateTime("2024-05-01"),
                ToDt = DateTime.Now,
                RegiserType = type
            });
            CaseRegisterViewModel model = new CaseRegisterViewModel();
            List<RegisterDetail> rdd = new List<RegisterDetail>();
            if (response != null && response.Succeeded)
            {
                foreach (var d in response.Data)
                {
                    RegisterDetail rd = new RegisterDetail();
                    rd.Id = d.Id;
                    rd.Court = d.CourtBench;
                    rd.CaseType = d.CaseType;
                    rd.Year = d.CaseYear.ToString();
                    rd.CaseNo = d.CaseNo.ToString();
                    rd.Remark = d.Reason;
                    rd.Title = d.FirstTitle + "Vs" + d.SecondTitle;
                    rdd.Add(rd);
                }
            }
            model.registerDetails = rdd;
            return PartialView("_Register", model);
        }

        #endregion


        #region Institution Register
        public async Task<IActionResult> InstitionRegister()
        {
            var response = await _mediator.Send(new InstitutionRegisterQuery()
            {
                PageNumber = 1,
                PageSize = 100,
                FromDt = Convert.ToDateTime("2024-05-01"),
                ToDt = DateTime.Now                
            });
            InstitutionRegisterViewMode model = new InstitutionRegisterViewMode();
            List<InstituteModel> inmd = new List<InstituteModel>();
            if (response != null && response.Succeeded)
            {
                foreach (var d in response.Data)
                {
                    InstituteModel rd = new InstituteModel();
                    rd.Id = d.Id;
                    rd.CourtBench = d.CourtBench;
                    rd.CaseType = d.CaseType;
                    rd.CaseYear = d.CaseYear;
                    rd.CaseNo = d.CaseNo.ToString();
                    rd.FirstTitle = d.FirstTitle;
                    rd.SecondTitle = d.SecondTitle;
                    rd.InsititutionDate = d.InsititutionDate; 
                    inmd.Add(rd);
                }
            }          
            model.dtmodel = inmd;   
            return View(model);
        }
        #endregion
    }

}
