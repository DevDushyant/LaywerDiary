using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseProceeding;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const long MaxFileSize = 200 * 1024 * 1024; // 200MB
        public CaseManageController(IWebHostEnvironment _webHostEnvironment)
        {
            this._webHostEnvironment = _webHostEnvironment;
        }

        #region Case Management Area
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseInfoQuery()
            {
                UserId = CurrentUser.Id,
                PageSize = 10000,
                PageNumber = 1
            });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseInfoViewModel>>(response.Data);
                _logger.LogInformation("Load all the user's cases successfully!");
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        //[HttpPost]
        //public async Task<IActionResult> LoadAll([FromBody] DataTableRequest request)
        //{
        //    var response = await _mediator.Send(new GetCaseInfoQuery()
        //    {
        //        UserId = CurrentUser.Id,
        //        PageSize = request.PageSize,
        //        PageNumber = request.PageNumber
        //    });
        //    if (response.Succeeded)
        //    {
        //        var viewModel = _mapper.Map<List<GetCaseInfoViewModel>>(response.Data);
        //        _logger.LogInformation("Loaded paginated cases successfully!");

        //        return Json(new
        //        {
        //            draw = request.PageNumber,
        //            recordsTotal = response.TotalCount, // Total records count before filtering
        //            recordsFiltered = response.TotalPages, // Records after filtering
        //            data = viewModel
        //        });
        //    }
        //    return null;
        //}


        public async Task<IActionResult> CreateOrUpdateAsync(Guid id, string from)
        {
            _logger.LogInformation("Case create or edit form!");
            try
            {
                bool showHighCourt = false;
                bool AgIsHighCourt = false;
                var caseViewModel = new CaseUpseartViewModel();
                caseViewModel.from = from;
                caseViewModel.ClientId = TempData["ClientId"] != null ? (Guid)TempData["ClientId"] : Guid.Empty;
                CaseAgainstModel cam = new CaseAgainstModel();
                cam.AStates = await LoadStates();
                cam.ACourtTypes = await LoadCourtTypes();
                cam.AYears = DdlYears();
                cam.ACaseStatusList = await DdlCaseStages();
                cam.ALinkedBy = await UserCaseTitle(Guid.Empty);
                cam.ACadres = await DdlCadres();
                cam.AStrengths = DdlStrength();
                cam.ACaseNatures = await LoadCaseNature();
                if (id == Guid.Empty && (from == null || from == ""))
                {
                    //caseViewModel.Courts = await DdlCourts();
                    caseViewModel.InstitutionDate = DateTime.Now;
                    caseViewModel.States = await LoadStates();
                    caseViewModel.CourtTypes = await LoadCourtTypes();
                    caseViewModel.CaseNatures = await LoadCaseNature();
                    caseViewModel.FirstTitleList = await DdlFSTypes(1);
                    caseViewModel.SecondTitleList = await DdlFSTypes(2);
                    caseViewModel.Years = DdlYears();
                    caseViewModel.CaseStatusList = await DdlCaseStages();
                    caseViewModel.LinkedBy = await UserCaseTitle(Guid.Empty);
                    caseViewModel.Cadres = await DdlCadres();
                    caseViewModel.Strengths = DdlStrength();
                    ViewBag.ActionType = "Save";
                    ViewBag.ShowHighCourt = showHighCourt;
                    ViewBag.AgIsHighCourt = AgIsHighCourt;
                    caseViewModel.ClientList = await DdlClient(CurrentUser.Id);
                    var agcl = new List<CaseAgainstModel>();
                    agcl.Add(cam);
                    caseViewModel.AgainstCaseDetails = agcl;
                    ViewBag.Id = id.ToString();
                    _logger.LogInformation("Case entry form loaded successfully!");
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
                        CaseDetail.ClientList = await DdlClient(CurrentUser.Id);
                        CaseDetail.States = await LoadStates();
                        CaseDetail.CourtTypes = await LoadCourtTypes();
                        CaseDetail.CaseNatures = await LoadCaseNatureByCourtType(CaseDetail.CourtTypeId); ;
                        CaseDetail.TypeOfCases = await CaseTypes(CaseDetail.CaseCategoryId);
                        CaseDetail.Years = DdlYears();
                        CaseDetail.FirstTitleList = await DdlFSTypes(1);
                        CaseDetail.SecondTitleList = await DdlFSTypes(2);
                        CaseDetail.CaseStatusList = await DdlCaseStages();
                        CaseDetail.LinkedBy = await UserCaseTitle(id);
                        if (CaseDetail.IsHighCourt == true)
                        {
                            CaseDetail.Courts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, Guid.Empty, Guid.Empty);
                            CaseDetail.Strengths = DdlStrength();
                            showHighCourt = true;
                        }
                        else
                        {
                            CaseDetail.CourtDistricts = await DdlLoadCourtDistricts(CaseDetail.StateId);
                            CaseDetail.ComplexBenchs = await GetCourtComplex(CaseDetail.CourtDistrictId.Value);
                            CaseDetail.Courts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, CaseDetail.ComplexId.Value, CaseDetail.CourtDistrictId.Value);
                        }
                        if (CaseDetail.AgainstCaseDetails.Count > 0)
                        {
                            foreach (var item in CaseDetail.AgainstCaseDetails)
                            {
                                item.CaseId = id;
                                cam.StateId = item.StateId;
                                cam.ImpugedOrderDate = item.ImpugedOrderDate;
                                cam.CourtTypeId = item.CourtTypeId;
                                if (item.IsAgHighCourt == true)
                                {
                                    cam.ACourts = await LoadBenches(item.CourtTypeId.Value, item.StateId.Value, Guid.Empty, Guid.Empty);
                                    cam.AStrengths = DdlStrength();
                                    AgIsHighCourt = true;
                                    cam.BenchId = item.BenchId;
                                }
                                else
                                {
                                    cam.CourtDistrictId = item.CourtDistrictId;
                                    cam.ComplexId = item.ComplexId;
                                    cam.CourtId = item.CourtId;
                                    cam.ACourtDistricts = await DdlLoadCourtDistricts(item.StateId.Value);
                                    cam.AComplexBenchs = await GetCourtComplex(item.CourtDistrictId.Value);
                                    cam.ACourts = await LoadBenches(item.CourtTypeId.Value, item.StateId.Value, item.ComplexId.Value, item.CourtDistrictId.Value);
                                }
                                cam.CaseCategoryId = item.CaseCategoryId;
                                cam.CaseTypeId = item.CaseTypeId;
                                cam.CaseNo = item.CaseNo;
                                cam.CaseYear = item.CaseYear;
                                cam.CisNo = item.CisNo;
                                cam.CisYear = item.CisYear;
                                cam.CnrNo = item.CnrNo;
                                cam.OfficerName = item.OfficerName;
                                cam.CadreId = item.CadreId;
                                cam.ACaseNatures = await LoadCaseNatureByCourtType(item.CourtTypeId.Value);
                                cam.ATypeOfCases = await CaseTypes(item.CaseCategoryId.Value);
                            }
                        }
                        var agcl = new List<CaseAgainstModel>();
                        agcl.Add(cam);
                        CaseDetail.AgainstCaseDetails = agcl;
                        ViewBag.ShowHighCourt = showHighCourt;
                        ViewBag.AgIsHighCourt = AgIsHighCourt;
                        CaseDetail.Cadres = await DdlCadres();
                        if (from != "repeat")
                        {
                            ViewBag.ActionType = "Update";
                            ViewBag.Id = CaseDetail.Id.ToString();
                        }
                        else
                        {
                            _logger.LogInformation("Case form is access by Repeat");
                            ViewBag.ActionType = "Save";
                            ViewBag.Id = Guid.Empty.ToString();
                            CaseDetail.Id = Guid.Empty;
                            CaseDetail.LinkedCaseId = Guid.Empty;
                            ViewBag.from = from;
                        }
                        return View("_CreateOrEdit", CaseDetail);
                    }
                    ViewBag.ShowHighCourt = showHighCourt;
                    ViewBag.AgIsHighCourt = AgIsHighCourt;
                    caseViewModel.ClientList = await DdlClient(CurrentUser.Id);
                    caseViewModel.InstitutionDate = DateTime.Now;
                    return View("_CreateOrEdit", caseViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
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
                        ViewModel.StatusMessage = "Case information created successfully!";
                        _notify.Success($"Case created with ID {result.Data} Created.");
                    }
                    else
                    {
                        _notify.Error(result.Message);
                        ViewBag.from = "repeat";
                        if (ViewModel.BenchId != null)
                            showHighCourt = true;
                        if (ViewModel.AgainstCaseDetails[0].BenchId != null)
                        {
                            AgIsHighCourt = true;
                        }
                    }
                    ViewBag.ShowHighCourt = showHighCourt;
                    ViewBag.AgIsHighCourt = AgIsHighCourt;
                    return RedirectToAction("CreateOrUpdate", "CaseManage", new { area = "Litigation" });
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateCaseDetailCommand>(ViewModel);
                    updateCommand.AgainstCaseDetails = _mapper.Map<List<UpseartAgainstCaseDto>>(ViewModel.AgainstCaseDetails);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                    {
                        _notify.Information($"Case information with ID {result.Data} Updated.");
                        return RedirectToAction("getcasedetail", new { id = Id });
                    }
                }
            }

            ViewModel.InstitutionDate = DateTime.Now;
            ViewModel.CaseNatures = await LoadCaseNature();
            ViewModel.CaseKinds = await LoadCaseKinds();
            ViewModel.CourtTypes = await LoadCourtTypes();
            ViewModel.CaseStages = await DdlCaseStages();
            ViewModel.FirstTitleList = FirstTtitleList();
            ViewModel.SecondTitleList = SecondTtitleList();
            ViewModel.Years = DdlYears();
            ViewModel.CaseStatusList = DdlCaseStatus();
            ViewModel.LinkedBy = DdlClient(CurrentUser.Id).Result;
            ViewBag.ShowHighCourt = showHighCourt;
            ViewBag.AgIsHighCourt = AgIsHighCourt;
            var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
            return new JsonResult(new { isValid = false, html = html });
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteCaseDetailCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Case info with Id {id} Deleted.");
                var response = await _mediator.Send(new GetCaseInfoQuery()
                {
                    UserId = CurrentUser.Id
                });
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<GetCaseInfoViewModel>>(response.Data);
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
                        DocDate = item.DocDate
                    });
                }
                var model = _mapper.Map<CaseHistoryViewModel>(response.Data);
                var ProcDts = response.Data.History
                    .Select(s => s.Date).Distinct().OrderByDescending(o => o.Date);
                var ProcHis = new List<ProcHistory>();
                foreach (var h in ProcDts)
                {
                    var ph = new ProcHistory();
                    ph.Date = h.Date.ToString("dd/MM/yyyy");
                    var Hisdt = response.Data.History.Where(w => w.Date == h.Date);
                    var ProcWiseHis = new List<HistoryDetail>();
                    foreach (var hd in Hisdt)
                    {
                        var his = new HistoryDetail();
                        his.Activity = hd.Activity;
                        his.NextDate = hd.NextDate;
                        his.Date = hd.Date.ToString("dd/MM/yyyy");
                        his.Stage = hd.Stage;
                        his.Type = hd.Type;
                        his.WorkDetail = _mapper.Map<List<Models.CaseWorkDetail>>(hd.WorkDetail);
                        ProcWiseHis.Add(his);
                        ph.History = ProcWiseHis;
                    }
                    ProcHis.Add(ph);
                }
                model.Docs = UDocs;
                model.ProcHis = ProcHis;
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CaseHistory", model) });
            }
            return null;
        }
        #endregion

        #region Document Upload 
        public async Task<IActionResult> GetFileUploadModel(Guid CaseId, string w)
        {
            var response = await _mediator.Send(new GetCaseHistoryQuery() { CaseId = CaseId });
            List<CaseDoc> UDocs = new List<CaseDoc>();
            var model = new CaseAttacheDocumentViewModel();
            if (response.Succeeded)
            {
                var docs = response.Data.Docs;
                var CaseInfo = response.Data;
                foreach (var item in docs)
                {
                    string ext = item.DocFilePath.Split(".")[1];
                    string Icon = "";
                    if (ext == "doc" || ext == "docx") Icon = "fa fa-file-word-o";
                    else Icon = "fa fa-file-pdf";
                    UDocs.Add(new CaseDoc
                    {
                        DocFilePath = item.DocFilePath,
                        DocName = item.DocName,
                        DocType = item.DocType,
                        DocDate = item.DocDate,
                        Id = item.Id,
                        FIcon = Icon
                    });
                }

                model.CaseId = CaseId;
                model.DocTypes = DOTypes();
                model.Docs = UDocs;
                model.CaseNoYear = CaseInfo.CaseNoYear;
                model.Title = CaseInfo.Title;
                model.Court = CaseInfo.Court;
                model.Where = w;
            }
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_UploadCaseDoc", model) });
        }

        [RequestSizeLimit(MaxFileSize)]
        public async Task<IActionResult> UploadCaseDocs(CaseAttacheDocumentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { isValid = false, message = "Invalid request data." });
            }
            List<CaseDocumentModel> ddoc = new List<CaseDocumentModel>();
            string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (model.Documents.Count > 0)
            {
                foreach (var f in model.Documents)
                {
                    // Step 1: Validate File
                    if (!IsValidFileType(f.Document))
                    {
                        return BadRequest($"Invalid file type: {Path.GetExtension(f.Document.FileName)}. Only PDF and DOCX are allowed.");
                    }
                    if (f.Document.Length > MaxFileSize)
                    {
                        return BadRequest("File size exceeds the 200MB limit.");
                    }
                    try
                    {
                        // Step 2: Determine Folder Path
                        string dcFld = f.TypeId == 1 ? "Draft" : "Order";
                        string docPath = Path.Combine("documents", dcFld, model.CaseId.ToString());
                        string fullPath = Path.Combine(root, docPath);
                        // Step 3: Check & Create Directory If It Doesn't Exist
                        if (!Directory.Exists(fullPath))
                        {
                            Directory.CreateDirectory(fullPath);
                            _logger.LogInformation($"Directory created: {fullPath}");
                        }

                        // Step 3: Generate Unique File Name
                        string uniqueFileName = $"{Path.GetFileNameWithoutExtension(f.Document.FileName)}_{Guid.NewGuid()}{Path.GetExtension(f.Document.FileName)}";
                        string fileNameWithPath = Path.Combine(fullPath, uniqueFileName);
                        // Step 4: Compress & Save File
                        string compressedFilePath = await CompressFileAsync(f.Document, fileNameWithPath);
                        ddoc.Add(new CaseDocumentModel
                        {
                            DocId = f.DocId,
                            TypeId = f.TypeId,
                            DocPath = Path.Combine(docPath, Path.GetFileName(compressedFilePath)),
                            DocDate = f.DocDate
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error uploading file {f.Document.FileName}: {ex.Message}");
                        return StatusCode(500, "Internal Server Error while processing the file.");
                    }
                }
            }
            // Step 5: Map and Save to Database
            var docMapper = _mapper.Map<List<DocumentAttachmentModel>>(ddoc);
            var response = await _mediator.Send(new CaseDocsCreateCommand()
            {
                CaseId = model.CaseId,
                Documents = docMapper
            });
            if (response.Succeeded)
            {
                if (model.Where == "mc")
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("institionregister", "register", new { area = "report" });
            }
            return new JsonResult(new { isValid = false, message = "Failed to process the request." });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDoc(Guid id, string fPath)
        {
            var respose = await _mediator.Send(new DeleteCaseDocumentCommand()
            {
                DocId = id
            });
            if (respose.Succeeded)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, fPath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return Json(new { success = true });
            }
            return Json(new { success = respose.Failed, respose.Message });

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
        public async Task<IActionResult> OnCreateClientInfoAsync()
        {
            var ViewModel = new ClientViewModel();
            //ViewModel.OppositCounsels = await DdlLawyerAsync();
            ViewModel.Appearences = await DdlFSTypes(0);
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateCaseClient", ViewModel) });
        }
        public async Task<JsonResult> OnGetClientInfo(Guid CaseId)
        {
            var ViewModel = new ClientViewModel();
            ViewModel.CaseId = CaseId;
            //ViewModel.OppositCounsels = await DdlLawyerAsync();
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
                        TempData["ClientId"] = id;
                        //var updateClientInfo = await _mediator.Send(new CaseClientInfoUpdateCommand()
                        //{
                        //    ClientId = id,
                        //    CaseId = btViewModel.CaseId,
                        //});
                        _notify.Success($"Client with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                    return RedirectToAction("CreateOrUpdate");
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
                var response = await _mediator.Send(new GetUserCaseDetailByIdQuery { CaseId = id, UserId = CurrentUser.Id });
                if (response.Succeeded)
                {
                    _notify.Success($"Client with ID {response.Data} Created.");
                    var CaseDetail = _mapper.Map<CaseUpseartViewModel>(response.Data);
                    //Set the primary case detail to against in upper case
                    var agnstVM = new CaseAgainstModel();
                    var agnstVML = new List<CaseAgainstModel>();
                    agnstVM.ImpugedOrderDate = CaseDetail.DisposalDate != null ? CaseDetail.DisposalDate : null;
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
                    if (CaseDetail.IsHighCourt == true)
                    {
                        agnstVM.ACourts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, Guid.Empty, Guid.Empty);
                        agnstVM.AStrengths = DdlStrength();
                        showHighCourt = true;
                    }
                    else
                    {
                        agnstVM.ACourtDistricts = await DdlLoadCourtDistricts(CaseDetail.StateId);
                        agnstVM.AComplexBenchs = await GetCourtComplex(CaseDetail.CourtDistrictId.Value);
                        agnstVM.ACourts = await LoadBenches(CaseDetail.CourtTypeId, CaseDetail.StateId, CaseDetail.ComplexId.Value, CaseDetail.CourtDistrictId.Value);
                    }
                    agnstVM.AStates = await LoadStates();
                    agnstVM.ACourtTypes = await LoadCourtTypes();
                    agnstVM.AYears = DdlYears();
                    agnstVM.ACaseStatusList = await DdlCaseStages();
                    agnstVM.ALinkedBy = await UserCaseTitle(Guid.Empty);
                    agnstVM.ACadres = await DdlCadres();
                    agnstVM.ACaseNatures = await LoadCaseNatureByCourtType(CaseDetail.CourtTypeId);
                    agnstVM.ATypeOfCases = await CaseTypes(CaseDetail.CaseCategoryId);
                    agnstVML.Add(agnstVM);
                    CaseDetail.AgainstCaseDetails = agnstVML;
                    ViewBag.ShowHighCourt = showHighCourt;
                    ViewBag.AgIsHighCourt = AgIsHighCourt;
                    ViewBag.ActionType = "Save";
                    //Reset the main case variable. 
                    CaseDetail.ClientList = await DdlClient(CurrentUser.Id);
                    CaseDetail.States = await LoadStates();
                    CaseDetail.CourtTypes = await LoadCourtTypes();
                    CaseDetail.CaseNatures = await LoadCaseNatureByCourtType(CaseDetail.CourtTypeId);
                    CaseDetail.TypeOfCases = await CaseTypes(CaseDetail.CaseCategoryId);
                    CaseDetail.Years = DdlYears();
                    CaseDetail.FirstTitleList = await DdlFSTypes(1);
                    CaseDetail.SecondTitleList = await DdlFSTypes(2);
                    CaseDetail.CaseStatusList = await DdlCaseStages();
                    CaseDetail.LinkedBy = await UserCaseTitle(id);
                    CaseDetail.Cadres = await DdlCadres();
                    CaseDetail.Strengths = DdlStrength();
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
                    return View("_CreateOrEdit", CaseDetail);
                }

            }
            return null;
        }
        #endregion

        #region Without Next Date Case List
        public async Task<IActionResult> GetCaseWoh()
        {
            var response = await _mediator.Send(new GetCaseWohDateQuery()
            {
                UserId = CurrentUser.Id,
                PageSize = 10000,
                PageNumber = 1
            });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseInfoViewModel>>(response.Data);
                return View("_WohDateCases", viewModel);
            }
            return null;
        }
        [HttpPost]
        public async Task<JsonResult> UpdateHearingDate(List<UpdateHearingDtViewModel> casedts)
        {
            var response = await _mediator.Send(new UpdateCaseHearingDatesCommand()
            {
                CasesHearingDt = _mapper.Map<List<CaseHearingDto>>(casedts)
            });
            return Json(response);
        }
        #endregion
    }
}
