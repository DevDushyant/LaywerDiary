using Azure;
using CourtApp.Application.Features.BookMasters.Command;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseProceeding;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GetCaseViewModel>>(response.Data);
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
                //caseViewModel.CourtDistricts = await DdlLoadCourtDistricts(1);

                caseViewModel.CaseNatures = await LoadCaseNature();
                //caseViewModel.CaseKinds = await LoadCaseKinds();

                //caseViewModel.CaseStages = await DdlCaseStages();
                caseViewModel.FirstTitleList = FirstTtitleList();
                caseViewModel.SecondTitleList = SecondTtitleList();
                caseViewModel.Years = DdlYears();
                caseViewModel.CaseStatusList = await DdlCaseStages();
                caseViewModel.LinkedBy = DdlClient().Result;
                caseViewModel.Cadres = DdlCadres();
                caseViewModel.Strengths = DdlStrength();
                caseViewModel.States = await LoadStates();
                return View("_CreateOrEdit", caseViewModel);
            }
            else
            {
                return View("_CreateOrEdit", caseViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid Id, CaseViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    ViewModel.CourtBenchId = ViewModel.BenchId == null ? ViewModel.CourtId.Value : ViewModel.BenchId.Value;
                    var createCommand = _mapper.Map<CreateCaseCommand>(ViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                    {
                        ViewModel.StatusMessage = "Record created successfully";
                        ViewModel.Id = result.Data;
                        _notify.Success($"Case created with ID {result.Data} Created.");

                    }
                    else _notify.Error(result.Message);
                    return View("_CreateOrEdit", ViewModel);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateBookMasterCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Book type with ID {result.Data} Updated.");
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
            var response = await _mediator.Send(new GetCaseDetailInfoQuery() { CaseId=id});
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<CaseDetailInfoViewModel>(response.Data);
                return View(viewModel);
            }
            return null;            
        }
        #endregion

    }
}
