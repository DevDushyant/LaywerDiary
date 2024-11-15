using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseProceeding;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
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
            var response = await _mediator.Send(new GetCaseInfoQuery()
            {
                UserId = CurrentUser.Id
            });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseInfoViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }


        public async Task<IActionResult> CreateOrUpdateAsync(Guid id, string from)
        {
            bool showHighCourt = false;
            bool AgIsHighCourt = false;
            var caseViewModel = new CaseUpseartViewModel();
            caseViewModel.from = from;
            caseViewModel.ClientId = TempData["ClientId"] != null ? (Guid)TempData["ClientId"] : Guid.Empty;
            if (id == Guid.Empty && (from == null || from == ""))
            {
                caseViewModel.InstitutionDate = DateTime.Now;
                caseViewModel.CourtTypes = await LoadCourtTypes();
                caseViewModel.CaseNatures = await LoadCaseNature();
                caseViewModel.FirstTitleList = await DdlFSTypes(1);
                caseViewModel.SecondTitleList = await DdlFSTypes(2);
                caseViewModel.Years = DdlYears();
                caseViewModel.CaseStatusList = await DdlCaseStages();
                caseViewModel.LinkedBy = await UserCaseTitle(Guid.Empty);
                caseViewModel.Cadres = DdlCadres();
                caseViewModel.Strengths = DdlStrength();
                caseViewModel.States = await LoadStates();
                ViewBag.ActionType = "Create";
                ViewBag.ShowHighCourt = showHighCourt;
                ViewBag.AgIsHighCourt = AgIsHighCourt;
                caseViewModel.ClientList = await DdlClient();
                return View("_CreateOrEdit", caseViewModel);
            }
            else
            {
                var response = await _mediator.Send(new GetUserCaseDetailByIdQuery
                {
                    CaseId = id,
                    UserId = CurrentUser.Id
                });
                if (response.Succeeded)
                {
                    var CaseDetail = _mapper.Map<CaseUpseartViewModel>(response.Data);
                    CaseDetail.ClientList = await DdlClient();
                    if (CaseDetail.IsHighCourt == true)
                    {
                        CaseDetail.Courts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, Guid.Empty);
                        CaseDetail.Strengths = DdlStrength();
                        showHighCourt = true;
                    }
                    else
                    {
                        CaseDetail.CourtDistricts = await DdlLoadCourtDistricts(CaseDetail.StateId);
                        CaseDetail.ComplexBenchs = await GetCourtComplex(CaseDetail.CourtDistrictId.Value);
                        CaseDetail.Courts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, CaseDetail.ComplexId.Value);

                    }
                    if (CaseDetail.AgainstCaseDetails.Count == 0)
                        CaseDetail.AgainstCaseDetails = null;
                    else
                    {

                        foreach (var item in CaseDetail.AgainstCaseDetails)
                        {
                            item.CaseId = id;
                            if (item.IsAgHighCourt == true)
                            {

                                CaseDetail.Courts = await LoadBenches(item.CourtTypeId.Value, item.StateId.Value, Guid.Empty);
                                CaseDetail.Strengths = DdlStrength();
                                AgIsHighCourt = true;
                            }
                            else
                            {
                                CaseDetail.CourtDistricts = await DdlLoadCourtDistricts(item.StateId.Value);
                                CaseDetail.ComplexBenchs = await GetCourtComplex(item.CourtDistrictId.Value);
                                CaseDetail.Courts = await LoadBenches(item.CourtTypeId.Value, item.StateId.Value, item.ComplexId.Value);
                                CaseDetail.AgainstCaseDetails[0].CourtId = item.CourtId;
                            }
                        }

                    }
                    ViewBag.ShowHighCourt = showHighCourt;
                    ViewBag.AgIsHighCourt = AgIsHighCourt;
                    CaseDetail.States = await LoadStates();
                    CaseDetail.CourtTypes = await LoadCourtTypes();
                    CaseDetail.CaseNatures = await LoadCaseNature();
                    CaseDetail.TypeOfCases = await CaseTypes(CaseDetail.CaseCategoryId);
                    CaseDetail.Years = DdlYears();
                    CaseDetail.FirstTitleList = await DdlFSTypes(1);
                    CaseDetail.SecondTitleList = await DdlFSTypes(2);
                    CaseDetail.CaseStatusList = await DdlCaseStages();
                    CaseDetail.LinkedBy = await UserCaseTitle(id);
                    CaseDetail.Cadres = DdlCadres();
                    if (from != "repeat")
                    {
                        ViewBag.ActionType = "Update";
                        ViewBag.Id = CaseDetail.Id.ToString();
                    }
                    else
                    {
                        ViewBag.ActionType = "Create";
                        ViewBag.Id = Guid.Empty.ToString();
                        CaseDetail.Id = Guid.Empty;
                        CaseDetail.ClientList = await DdlClient();
                    }
                    return View("_CreateOrEdit", CaseDetail);
                }
                ViewBag.ShowHighCourt = showHighCourt;
                ViewBag.AgIsHighCourt = AgIsHighCourt;
                caseViewModel.ClientList = await DdlClient();
                caseViewModel.InstitutionDate = DateTime.Now;
                return View("_CreateOrEdit", caseViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid Id, CaseUpseartViewModel ViewModel)
        {
            bool showHighCourt = false;
            bool AgIsHighCourt = false;
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CreateCaseCommand>(ViewModel);
                    createCommand.AgainstCaseDetails = _mapper.Map<List<UpseartAgainstCaseDto>>(ViewModel.AgainstCaseDetails);
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
                        ViewModel.LinkedBy = await UserCaseTitle(Guid.Empty);
                        ViewModel.Cadres = DdlCadres();
                        ViewModel.Strengths = DdlStrength();
                        ViewModel.States = await LoadStates();
                        _notify.Success($"Case created with ID {result.Data} Created.");

                    }
                    else _notify.Error(result.Message);
                    ViewBag.ShowHighCourt = showHighCourt;
                    ViewBag.AgIsHighCourt = AgIsHighCourt;
                    return View("_CreateOrEdit", ViewModel);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateCaseDetailCommand>(ViewModel);
                    updateCommand.AgainstCaseDetails = _mapper.Map<List<UpseartAgainstCaseDto>>(ViewModel.AgainstCaseDetails);
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
                        ViewModel.LinkedBy = await UserCaseTitle(Id);
                        ViewModel.Cadres = DdlCadres();
                        ViewModel.Strengths = DdlStrength();
                        ViewModel.States = await LoadStates();
                        ViewModel.StatusMessage = "Case information updated successfully!";
                    }
                    ViewBag.ShowHighCourt = showHighCourt;
                    ViewBag.AgIsHighCourt = AgIsHighCourt;
                    return RedirectToAction("getcasedetail", new { id = Id });
                }
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
                ViewBag.ShowHighCourt = showHighCourt;
                ViewBag.AgIsHighCourt = AgIsHighCourt;
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
        #endregion

        #region Case History
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

        #region Selected Lawyer case detail
        public async Task<IActionResult> BindLawyerCaseDetail(Guid id)
        {
            bool showHighCourt = false;
            bool AgIsHighCourt = false;
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new GetUserCaseDetailByIdQuery { CaseId = id });
                if (response.Succeeded)
                {
                    _notify.Success($"Client with ID {response.Data} Created.");
                    var CaseDetail = _mapper.Map<CaseUpseartViewModel>(response.Data);
                    CaseDetail.ClientList = await DdlClient();
                    //Set the primary case detail to against in upper case
                    var agnstVM = new CaseAgainstModel();
                    var agnstVML = new List<CaseAgainstModel>();
                    agnstVM.ImpugedOrderDate = CaseDetail.InstitutionDate;
                    agnstVM.StateId = CaseDetail.StateId;
                    agnstVM.CourtTypeId = CaseDetail.CourtTypeId;
                    agnstVM.CourtDistrictId = CaseDetail.CourtDistrictId;
                    agnstVM.ComplexId = CaseDetail.ComplexId;
                    agnstVM.CourtId = CaseDetail.CourtId;
                    agnstVM.CaseCategoryId = CaseDetail.CaseCategoryId;
                    agnstVM.CaseTypeId = CaseDetail.CaseTypeId;
                    agnstVM.CaseNo = CaseDetail.CaseNo;
                    agnstVM.CaseYear = CaseDetail.CaseYear;
                    agnstVM.CisNo = CaseDetail.CisNumber;
                    agnstVM.CisYear = CaseDetail.CisYear;
                    agnstVM.CnrNo = CaseDetail.CnrNumber;
                    agnstVML.Add(agnstVM);
                    CaseDetail.AgainstCaseDetails = agnstVML;
                    //Reset the main case variable.
                    CaseDetail.Id = Guid.Empty;
                    CaseDetail.LCaseId = id;
                    CaseDetail.InstitutionDate = DateTime.Now;
                    CaseDetail.StateId = 0;
                    CaseDetail.CourtTypeId = Guid.Empty;
                    CaseDetail.CourtDistrictId = Guid.Empty;
                    CaseDetail.ComplexId = Guid.Empty;
                    CaseDetail.CourtId = Guid.Empty;
                    CaseDetail.CaseCategoryId = Guid.Empty;
                    CaseDetail.CaseTypeId = Guid.Empty;
                    CaseDetail.CaseStageId = Guid.Empty;
                    CaseDetail.CaseNo = string.Empty;
                    CaseDetail.CaseYear = 0;
                    CaseDetail.CisNumber = string.Empty;
                    CaseDetail.CisYear = 0;
                    CaseDetail.CnrNumber = string.Empty;
                    CaseDetail.NextDate = null;
                    if (CaseDetail.IsHighCourt == true)
                    {
                        CaseDetail.Courts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, Guid.Empty);
                        CaseDetail.Strengths = DdlStrength();
                        showHighCourt = true;
                    }
                    else
                    {
                        CaseDetail.CourtDistricts = await DdlLoadCourtDistricts(CaseDetail.StateId);
                        CaseDetail.ComplexBenchs = await GetCourtComplex(CaseDetail.CourtDistrictId.Value);
                        CaseDetail.Courts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, CaseDetail.ComplexId.Value);

                    }
                    if (CaseDetail.AgainstCaseDetails.Count == 0)
                        CaseDetail.AgainstCaseDetails = null;
                    else
                    {
                        foreach (var item in CaseDetail.AgainstCaseDetails)
                        {
                            item.CaseId = id;
                            if (item.IsAgHighCourt == true)
                            {

                                CaseDetail.Courts = await LoadBenches(item.CourtTypeId.Value, item.StateId.Value, Guid.Empty);
                                CaseDetail.Strengths = DdlStrength();
                                AgIsHighCourt = true;
                            }
                            else
                            {
                                CaseDetail.CourtDistricts = await DdlLoadCourtDistricts(item.StateId.Value);
                                CaseDetail.ComplexBenchs = await GetCourtComplex(item.CourtDistrictId.Value);
                                CaseDetail.Courts = await LoadBenches(item.CourtTypeId.Value, item.StateId.Value, item.ComplexId.Value);
                                CaseDetail.AgainstCaseDetails[0].CourtId = item.CourtId;
                            }
                        }

                    }
                    ViewBag.ShowHighCourt = showHighCourt;
                    ViewBag.AgIsHighCourt = AgIsHighCourt;
                    CaseDetail.States = await LoadStates();
                    CaseDetail.CourtTypes = await LoadCourtTypes();
                    CaseDetail.CaseNatures = await LoadCaseNature();
                    CaseDetail.TypeOfCases = await CaseTypes(CaseDetail.CaseCategoryId);
                    CaseDetail.Years = DdlYears();
                    CaseDetail.FirstTitleList = await DdlFSTypes(1);
                    CaseDetail.SecondTitleList = await DdlFSTypes(2);
                    CaseDetail.CaseStatusList = await DdlCaseStages();
                    CaseDetail.LinkedBy = await UserCaseTitle(id);
                    CaseDetail.Cadres = DdlCadres();
                    CaseDetail.Strengths = DdlStrength();
                    ViewBag.ActionType = "Save";
                    return View("_CreateOrEdit", CaseDetail);
                }

            }
            return null;
        }
        #endregion
    }
}
