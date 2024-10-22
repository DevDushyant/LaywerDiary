using CourtApp.Application.Features.BookMasters.Command;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseProceeding;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Areas.LawyerDiary.Validators;
using CourtApp.Web.Areas.Litigation.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class CaseManageController : BaseController<CaseManageController>
    {
        #region Case Management Area
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseInfoQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseInfoViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }


        public async Task<IActionResult> CreateOrUpdateAsync(Guid id)
        {
            var ClientList = await _mediator.Send(new GetAllClientCachedQuery() { });
            var caseViewModel = new CaseViewModel();

            if (id == Guid.Empty)
            {
                caseViewModel.InstitutionDate = DateTime.Now;
                caseViewModel.CourtTypes = await LoadCourtTypes();
                caseViewModel.CaseNatures = await LoadCaseNature();
                caseViewModel.FirstTitleList = await DdlFSTypes(1);
                caseViewModel.SecondTitleList = await DdlFSTypes(2);
                caseViewModel.Years = DdlYears();
                caseViewModel.CaseStatusList = await DdlCaseStages();
                caseViewModel.LinkedBy = await UserCaseTitle();
                caseViewModel.Cadres = DdlCadres();
                caseViewModel.Strengths = DdlStrength();
                caseViewModel.States = await LoadStates();
                ViewBag.ActionType = "Create";
                return View("_CreateOrEdit", caseViewModel);
            }
            else
            {
                var response = await _mediator.Send(new GetUserCaseDetailByIdQuery { CaseId = id });
                if (response.Succeeded)
                {
                    var CaseDetail = _mapper.Map<CaseViewModel>(response.Data);
                    if (CaseDetail.IsHighCourt == true)
                    {
                        CaseDetail.BenchId = CaseDetail.CourtBenchId;
                        CaseDetail.Courts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, Guid.Empty);
                        CaseDetail.BenchId = CaseDetail.CourtBenchId;
                        CaseDetail.Strengths = DdlStrength();
                    }
                    else
                    {
                        CaseDetail.CourtDistricts = await DdlLoadCourtDistricts(CaseDetail.StateId);
                        CaseDetail.ComplexBenchs = await GetCourtComplex(CaseDetail.CourtDistrictId.Value);
                        CaseDetail.Courts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, CaseDetail.ComplexBenchId.Value);
                        CaseDetail.CourtId = CaseDetail.CourtBenchId;
                    }
                    if (CaseDetail.AgainstCaseDetails.Count == 0)
                        CaseDetail.AgainstCaseDetails = null;
                    else
                    {
                        foreach (var item in CaseDetail.AgainstCaseDetails)
                        {
                            if (item.IsAgHighCourt == true)
                            {

                                CaseDetail.Courts = await LoadBenches(item.CourtTypeId.Value, item.StateId.Value, Guid.Empty);
                                CaseDetail.Strengths = DdlStrength();
                            }
                            else
                            {
                                CaseDetail.CourtDistricts = await DdlLoadCourtDistricts(item.StateId.Value);
                                CaseDetail.ComplexBenchs = await GetCourtComplex(item.CourtDistrictId.Value);
                                CaseDetail.Courts = await LoadBenches(item.CourtTypeId.Value, item.StateId.Value, item.ComplexId.Value);
                                CaseDetail.AgainstCaseDetails[0].CourtId=item.BenchId;
                            }
                        }
                    }


                    CaseDetail.States = await LoadStates();
                    CaseDetail.CourtTypes = await LoadCourtTypes();
                    CaseDetail.CaseNatures = await LoadCaseNature();
                    CaseDetail.TypeOfCases = await CaseTypes(CaseDetail.CaseCategoryId);
                    CaseDetail.Years = DdlYears();
                    CaseDetail.FirstTitleList = await DdlFSTypes(1);
                    CaseDetail.SecondTitleList = await DdlFSTypes(2);
                    CaseDetail.CaseStatusList = await DdlCaseStages();
                    CaseDetail.LinkedBy = await UserCaseTitle();
                    CaseDetail.Cadres = DdlCadres();

                    ViewBag.ActionType = "Update";
                    return View("_CreateOrEdit", CaseDetail);
                }
                return null;

            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid Id, CaseViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                ViewModel.CourtBenchId = ViewModel.BenchId == null ? ViewModel.CourtId.Value : ViewModel.BenchId.Value;
                if (Id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CreateCaseCommand>(ViewModel);                    
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                    {
                        ViewModel.StatusMessage = "Case created successfully!";
                        ViewModel.Id = result.Data;
                        ViewModel.InstitutionDate = DateTime.Now;
                        ViewModel.CourtTypes = await LoadCourtTypes();
                        ViewModel.CaseNatures = await LoadCaseNature();
                        ViewModel.FirstTitleList = await DdlFSTypes(1);
                        ViewModel.SecondTitleList = await DdlFSTypes(2);
                        ViewModel.Years = DdlYears();
                        ViewModel.CaseStatusList = await DdlCaseStages();
                        ViewModel.LinkedBy = await UserCaseTitle();
                        ViewModel.Cadres = DdlCadres();
                        ViewModel.Strengths = DdlStrength();
                        ViewModel.States = await LoadStates();
                        _notify.Success($"Case created with ID {result.Data} Created.");

                    }
                    else _notify.Error(result.Message);
                    return View("_CreateOrEdit", ViewModel);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateCaseDetailCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                    {
                        _notify.Information($"Book type with ID {result.Data} Updated.");
                        ViewModel.InstitutionDate = DateTime.Now;
                        ViewModel.CourtTypes = await LoadCourtTypes();
                        ViewModel.CaseNatures = await LoadCaseNature();
                        ViewModel.FirstTitleList = await DdlFSTypes(1);
                        ViewModel.SecondTitleList = await DdlFSTypes(2);
                        ViewModel.Years = DdlYears();
                        ViewModel.CaseStatusList = await DdlCaseStages();
                        ViewModel.LinkedBy = await UserCaseTitle();
                        ViewModel.Cadres = DdlCadres();
                        ViewModel.Strengths = DdlStrength();
                        ViewModel.States = await LoadStates();
                        ViewModel.StatusMessage = "Case information updated successfully!";
                    }
                    return RedirectToAction("getcasedetail", new { id = Id });
                }
                return View("_CreateOrEdit", null);
            }
            else
            {
                ViewModel.InstitutionDate = DateTime.Now;
                ViewModel.CaseNatures = await LoadCaseNature();
                ViewModel.CaseKinds = await LoadCaseKinds();
                ViewModel.CourtTypes = await LoadCourtTypes();
                ViewModel.CaseStages = await DdlCaseStages();
                ViewModel.FirstTitleList = FirstTtitleList();
                ViewModel.SecondTitleList = SecondTtitleList();
                ViewModel.Years = DdlYears();
                ViewModel.CaseStatusList = DdlCaseStatus();
                ViewModel.LinkedBy = DdlClient().Result;
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
        #endregion

        #region Case Proceeding Area    
        public async Task<IActionResult> LoadCaseProceeding()
        {
            var response = await _mediator.Send(new GetCaseProceedingQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        //public async Task<JsonResult> OnGetCaseProceeding(Guid id)
        //{
        //    if (id == Guid.Empty)
        //    {
        //        var ViewModel = new CaseProceedingViewModel();
        //        ViewModel.Heads = await DdlProcHeads();
        //        ViewModel.NextStages = await DdlCaseStages();
        //        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CaseProceeding", ViewModel) });
        //    }
        //    else
        //    {
        //        var response = await _mediator.Send(new GetQueryByIdCaseCategory() { Id = id });
        //        if (response.Succeeded)
        //        {
        //            var ViewModel = _mapper.Map<CaseProceedingViewModel>(response.Data);

        //            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
        //        }
        //        return null;
        //    }
        //}

        //[HttpPost]
        //public async Task<JsonResult> OnPostCaseProceeding(Guid id, CaseProceedingViewModel ViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id == Guid.Empty)
        //        {
        //            var createCommand = _mapper.Map<CreateCaseWorkCommand>(ViewModel);
        //            var result = await _mediator.Send(createCommand);
        //            if (result.Succeeded)
        //                _notify.Success($"Case Proceeding with ID {result.Data} Created.");
        //            else _notify.Error(result.Message);
        //        }
        //        else
        //        {
        //            var updateCommand = _mapper.Map<UpdateCaseProceedingCommand>(ViewModel);
        //            var result = await _mediator.Send(updateCommand);
        //            if (result.Succeeded)
        //                _notify.Information($"Case Proceeding with ID {result.Data} Updated.");
        //        }
        //        return new JsonResult(new { isValid = false, html = "" });
        //    }
        //    else
        //    {
        //        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
        //        return new JsonResult(new { isValid = false, html = html });
        //    }
        //}

        #endregion

        #region Case History Resion
        public async Task<JsonResult> OnGetCaseHistory(Guid CaseId)
        {
            var response = await _mediator.Send(new GetCaseHistoryQuery() { CaseId = CaseId });
            if (response.Succeeded)
            {
                var docs = response.Data.Docs;
                List<CaseDoc> UDocs = new List<CaseDoc>();
                foreach (var item in docs)
                {
                    UDocs.Add(new CaseDoc
                    {
                        DocFilePath = item.DocFilePath,
                        DocName = item.DocName,
                        DocType = item.DocType,
                    });
                }
                var model = _mapper.Map<CaseHistoryViewModel>(response.Data);
                model.Docs = UDocs;
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CaseHistory", model) });
            }
            return null;
        }
        #endregion

        #region Document Upload 
        public async Task<IActionResult> GetFileUploadModel(Guid CaseId)
        {
            var model = new CaseAttacheDocumentViewModel();
            model.CaseId = CaseId;
            model.DocTypes = DOTypes();
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_UploadCaseDoc", model) });
        }

        public async Task<IActionResult> UploadCaseDocs(CaseAttacheDocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<CaseDocumentModel> ddoc = new List<CaseDocumentModel>();
                string Root = "wwwroot";
                if (model.Documents.Count() > 0)
                {
                    foreach (var f in model.Documents)
                    {
                        string DcFld = f.TypeId == 1 ? "Draft" : "Order";
                        string DocPath = "documents/" + DcFld + "/" + model.CaseId;
                        string FullPath = Root + "/" + DocPath;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), FullPath);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        FileInfo fileInfo = new FileInfo(f.Document.FileName);
                        string fileName = f.Document.FileName;

                        string fileNameWithPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            f.Document.CopyTo(stream);
                        }
                        ddoc.Add(new CaseDocumentModel
                        {
                            DocId = f.DocId,
                            TypeId = f.TypeId,
                            DocPath = DocPath + "/" + fileName,
                        });
                    }
                }
                var docMapper = _mapper.Map<List<DocumentAttachmentModel>>(ddoc);
                var respose = await _mediator.Send(new CaseDocsCreateCommand()
                {
                    CaseId = model.CaseId,
                    Documents = docMapper
                });
                if (respose.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return null;
            }
            return new JsonResult(new { isValid = true });
        }
        #endregion

        #region Case Detail
        public async Task<IActionResult> GetCaseDetailAsync(Guid id)
        {
            var response = await _mediator.Send(new GetCaseDetailInfoQuery() { CaseId = id });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<CaseDetailInfoViewModel>(response.Data);
                return View(viewModel);
            }
            return null;
        }
        #endregion

        #region Case and Client 
        public async Task<JsonResult> OnGetClientInfo(Guid CaseId)
        {
            var ViewModel = new ClientViewModel();
            ViewModel.CaseId = CaseId;
            ViewModel.OppositCounsels = await DdlLawyerAsync();
            ViewModel.Appearences = await DdlFSTypes(0);
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_ClientInfoDetail", ViewModel) });
        }
        [HttpPost]
        public async Task<IActionResult> OnPostClientInfo(Guid id, ClientViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createClientCommand = _mapper.Map<CreateClientCommand>(btViewModel);
                    var result = await _mediator.Send(createClientCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        var updateClientInfo = await _mediator.Send(new CaseClientInfoUpdateCommand()
                        {
                            ClientId = id,
                            CaseId = btViewModel.CaseId,
                        });
                        _notify.Success($"Client with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                    return RedirectToAction("CreateOrUpdateAsync");
                }
                return null;
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", btViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
        #endregion

    }
}
