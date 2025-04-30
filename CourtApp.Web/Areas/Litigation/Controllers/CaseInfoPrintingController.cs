using CourtApp.Application.Features.FormPrint;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class CaseInfoPrintingController : BaseController<CaseInfoPrintingController>
    {
        public async Task<IActionResult> Index()
        {
            FmpViewModel fmpViewModel = new FmpViewModel();
            fmpViewModel.FormTypes = FormPrintingTypes();
            fmpViewModel.Cases = await UserCaseTitle(Guid.Empty);
            fmpViewModel.Titles = await UserCaseTitle(Guid.Empty);
            return View(fmpViewModel);
        }

        public async Task<IActionResult> LoadFormPrinting(string type, List<Guid> Cases, string AppNo)
        {
            //List<Guid> CaseIds = new List<Guid>();
            if (Cases != null && Cases.Count > 0)
            {
                //CaseIds = Cases.Split(',').Select(Guid.Parse).ToList();
                if (type == "FINS") //Inspection form 
                {
                    var response = await _mediator.Send(new GetInspectionQuery() { CaseIds = Cases });
                    if (response.Succeeded)
                    {
                        var viewmodel = _mapper.Map<List<InspectionViewModel>>(response.Data);
                        FmpInspectionFormViewModel fmpViewModel = new FmpInspectionFormViewModel();
                        fmpViewModel.Cases = viewmodel;
                        return PartialView("_InspectionForm", fmpViewModel);
                    }
                }

                else if (type == "FPRS") //permission slip need to modify logic for template
                {
                    var response = await _mediator.Send(new GetPermissionSlipQuery() { CaseIds = Cases });
                    if (response.Succeeded)
                    {
                        var viewmodel = _mapper.Map<List<PermissionSlipDataModel>>(response.Data);
                        FmpPermissionSlipFormViewModel fmpViewModel = new FmpPermissionSlipFormViewModel();
                        fmpViewModel.PerSlipInfo = viewmodel;
                        return PartialView("_PermissionSlip", fmpViewModel);
                    }
                }

                else if (type == "COPA") //Copying application
                {
                    var response = await _mediator.Send(new GetCopyingAppQuery() { CaseIds = Cases });
                    if (response.Succeeded)
                    {
                        var viewmodel = _mapper.Map<List<CopyingAppViewModel>>(response.Data);
                        FmpCopyingAppViewModel fmpViewModel = new FmpCopyingAppViewModel();
                        fmpViewModel.Cases = viewmodel;
                        return PartialView("_CopyingApplication", fmpViewModel);
                    }
                }

                else if (type == "FNOA") //Notice of admission or civil admission
                {
                    var response = await _mediator.Send(new GetNoticeOfAdmissionQuery() { CaseIds = Cases });
                    if (response.Succeeded)
                    {
                        var viewmodel = _mapper.Map<List<NoticeAdmissionViewModel>>(response.Data);
                        FmpNoticeAdmissionViewModel fmpViewModel = new FmpNoticeAdmissionViewModel();
                        fmpViewModel.Cases = viewmodel;
                        var WritCases = viewmodel.Where(x => x.CaseCategory.ToLower() == "writ");
                        var CivilCases = viewmodel.Where(x => x.CaseCategory.ToLower() == "civil");
                        if (WritCases.Count() > 0)
                        {
                            fmpViewModel.Cases = WritCases.ToList();
                            return PartialView("_AdmissionWrit", fmpViewModel);
                        }
                        else
                        {
                            fmpViewModel.Cases = CivilCases.ToList();
                            return PartialView("_AdmissionCivil", fmpViewModel);
                        }
                    }
                }

                else if (type == "FNSA") //Notice of stay application
                {
                    var response = await _mediator.Send(new GetNoticeOfStayAppQuery() { CaseIds = Cases });
                    if (response.Succeeded)
                    {
                        var viewmodel = _mapper.Map<List<NoticeOfStayApplication>>(response.Data);
                        FmpNoticeOfStayApplicationViewModel fmpViewModel = new FmpNoticeOfStayApplicationViewModel();
                        fmpViewModel.NoticeStayApps = viewmodel;
                        return PartialView("_NoticeOfStayApplication", fmpViewModel);
                    }
                }

                else if (type == "FNSC") //Notice of show cause
                {
                    var response = await _mediator.Send(new GetShowCauseNoticeQuery() { CaseIds = Cases, ApplicantNo = AppNo });
                    if (response.Succeeded)
                    {
                        var viewmodel = _mapper.Map<List<ShowCauseViewModel>>(response.Data);
                        FmpShowCauseViewModel fmpViewModel = new FmpShowCauseViewModel();
                        var WritCases = viewmodel.Where(x => x.CaseType.ToLower().Contains("writ"));
                        var CivilCases = viewmodel.Where(x => x.CaseType.ToLower().Contains("civil"));
                        if (WritCases.Count() > 0)
                        {
                            fmpViewModel.ShowCauses = WritCases.ToList();
                            return PartialView("_ShowCauseNoticeWrit", fmpViewModel);
                        }
                        else
                        {
                            fmpViewModel.ShowCauses = CivilCases.ToList();
                            return PartialView("_ShowCauseNoticeCivil", fmpViewModel);
                        }

                    }
                }

                else
                    return null;

            }
            return null;
        }
    }
}
