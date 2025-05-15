using Azure;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CourtForm;
using CourtApp.Application.Features.FormPrint;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static CourtApp.Application.Constants.Permissions;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class CaseInfoPrintingController : BaseController<CaseInfoPrintingController>
    {

        public async Task<IActionResult> Index()
        {
            FmpViewModel fmpViewModel = new FmpViewModel();
            var formsDataResponse = await _mediator.Send(new CourtFormSearchQuery() { StateId = 1 });
            if (formsDataResponse.Succeeded)
            {
                var formsDt = formsDataResponse.Data.Select(s => new
                {
                    Id = s.Id,
                    FormName = s.FormName.ToUpper()
                });
                fmpViewModel.FormTypes = new SelectList(formsDt, "Id", "FormName");
            }
            fmpViewModel.Cases = await UserCaseTitle(Guid.Empty);
            fmpViewModel.Titles = await UserCaseTitle(Guid.Empty);
            return View(fmpViewModel);
        }
        public async Task<IActionResult> Index1()
        {
            FmpViewModel fmpViewModel = new FmpViewModel();
            fmpViewModel.FormTypes = FormPrintingTypes();
            fmpViewModel.Cases = await UserCaseTitle(Guid.Empty);
            fmpViewModel.Titles = await UserCaseTitle(Guid.Empty);
            return View(fmpViewModel);
        }


        public async Task<IActionResult> LoadFormPrinting(Guid type, List<Guid> Cases, string AppNo)
        {
            var formTemplates = await _mediator.Send(new CourtFormSearchQuery() { StateId = 1, Id = type });
            string formTemplate = string.Empty;
            if (formTemplates.Succeeded)
                formTemplate = formTemplates.Data.Select(s => s.FormTemplate).FirstOrDefault();

            var caseDetail = await _mediator.Send(new GetFormPrintDataQuery { CaseIds = Cases });
            GlobalFormPrintViewModel prinatbleData = new GlobalFormPrintViewModel();
            if (caseDetail.Succeeded)
            {
                var caseInfoDetails = _mapper.Map<List<FormPrintData>>(caseDetail.Data);
                prinatbleData.CasesInfo = caseInfoDetails;
            }
            List<string> formRawHtml = new List<string>();
            foreach (var v in prinatbleData.CasesInfo)
            {
                var applicantDetail = v.Applicants;
                var agCaseDetail=v.AgainstCourtDetail;
                foreach (var ad in applicantDetail)
                {
                    // Clone the template each time to avoid overwriting previous replacements
                    var tempForm = formTemplate;
                    tempForm = tempForm.Replace("#InstitutionDate#", v.InstitutionDate)
                                       .Replace("#StateName#", v.State)
                                       .Replace("#CourtType#", v.CourtType)
                                       .Replace("#CourtDistrict#", v.CourtDistrict)
                                       .Replace("#CourtComplex#", v.CourtComplex)
                                       .Replace("#Court#", v.Court)
                                       .Replace("#Strength#", v.Strength)
                                       .Replace("#CaseNoYear#", v.CaseNoYear)
                                       .Replace("#CaseCategory#", v.CaseCategory)
                                       .Replace("#CaseType#", v.CaseType)
                                       .Replace("#CisNoYear#", v.CisNoYear)
                                       .Replace("#PetitionerAppearance#", v.PetitionerAppearance)
                                       .Replace("#Petitioner#", v.Petitioner)
                                       .Replace("#RespondantAppearance#", v.RespondantAppearance)
                                       .Replace("#Respondant#", v.Respondent)
                                       .Replace("#NextDate#", v.NextDate)
                                       .Replace("#CaseStage#", v.CaseStage)
                                       .Replace("#DisposalDate#", v.DisposalDate)
                                       .Replace("#CnrNo#", v.CnrNo)
                                       .Replace("#CurrentDate#", DateTime.Now.Date.ToString("dd/MM/yyyy"))
                                       .Replace("#ApplicantNo#", ad.ApplicantNo.ToString())
                                       .Replace("#ApplicantDetail#", ad.Applicant)
                                       .Replace("#ImpugedOrder#", agCaseDetail.CourtBench)
                                       .Replace("#AgState#", agCaseDetail.State)
                                       .Replace("#AgCourtType#", agCaseDetail.CourtType)
                                       .Replace("#AgCourtDistrict#", agCaseDetail.CourtDistrict)
                                       .Replace("#AgCourtComplex#", agCaseDetail.CourtComplex)
                                       .Replace("#AgCourtBench#", agCaseDetail.CourtBench)
                                       .Replace("#AgCaseNoYear#", agCaseDetail.CaseNo +"/"+ agCaseDetail.CaseYear)
                                       .Replace("#AgCaseType#", agCaseDetail.CaseType)
                                       .Replace("#AgCnrNo#", agCaseDetail.CnrNo)
                                       .Replace("#Cadre#", agCaseDetail.Cadre)
                                       .Replace("#OfficerName#", agCaseDetail.OfficerName)
                                       .Replace("#OfficerName#", agCaseDetail.OfficerName)
                                       .Replace("#AgCaseCategory#", agCaseDetail.CaseCategory)
                                    ;
                                       


                    

                    formRawHtml.Add(tempForm);
                }
            }

            // You don’t need HtmlDecode unless you're decoding entities — use it if needed:
            var decodedList = formRawHtml.Select(html => HttpUtility.HtmlDecode(html)).ToList();

            return PartialView("_GlobalFormPrintPartial", decodedList);

        }

        public async Task<IActionResult> LoadFormPrinting1(string type, List<Guid> Cases, string AppNo)
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
                else if (type == "FTLW")
                {
                    return PartialView("_TalwanaForm", null);
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
