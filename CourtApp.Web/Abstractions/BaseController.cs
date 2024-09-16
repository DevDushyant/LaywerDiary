using AspNetCoreHero.Results;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Azure;
using CourtApp.Application.Constants;
using CourtApp.Application.Enums;
using CourtApp.Application.Features.CaseCategory;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Application.Features.CaseTitle;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.CourtComplex;
using CourtApp.Application.Features.CourtDistrict;
using CourtApp.Application.Features.CourtMasters;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.DOType;
using CourtApp.Application.Features.FormBuilder;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Application.Features.Lawyer;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.ProceedingSubHead;
using CourtApp.Application.Features.Queries.Districts;
using CourtApp.Application.Features.Queries.States;
using CourtApp.Application.Features.TypeOfCases.Query;
using CourtApp.Application.Features.UserCase;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Application.Features.WorkMasterSub;
using CourtApp.Web.Areas.Admin.Models;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.LawyerDiary.Models.Lawyer;
using CourtApp.Web.Areas.LawyerDiary.Models.Title;
using CourtApp.Web.Areas.Litigation.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourtApp.Web.Abstractions
{
    public abstract class BaseController<T> : Controller
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;
        private IViewRenderService _viewRenderInstance;
        private IMapper _mapperInstance;
        private INotyfService _notifyInstance;
        protected INotyfService _notify => _notifyInstance ??= HttpContext.RequestServices.GetService<INotyfService>();

        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected IViewRenderService _viewRenderer => _viewRenderInstance ??= HttpContext.RequestServices.GetService<IViewRenderService>();
        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();


        #region Dropdown Select List
        public async Task<SelectList> LoadStates()
        {
            var response = await _mediator.Send(new GetStateMasterQuery());
            var ViewModel = _mapper.Map<List<StateViewModel>>(response.Data);
            return new SelectList(ViewModel, nameof(StateViewModel.Code), nameof(StateViewModel.Name_En), null, null);
        }
        public async Task<SelectList> LoadCourtTypes()
        {
            var response = await _mediator.Send(new GetCourtTypeQuery());
            var CaseKind = _mapper.Map<List<CourtTypeViewModel>>(response.Data);
            return new SelectList(CaseKind, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null);
        }
        public async Task<SelectList> DdlLoadCourtDistricts(int DistrictId)
        {
            var districts = await _mediator.Send(new GetCourtDistrictQuery() { DistrictId = DistrictId });
            var districtViewModel = _mapper.Map<List<CourtDistrictViewModel>>(districts.Data);
            return new SelectList(districtViewModel, nameof(CourtDistrictViewModel.Id), nameof(CourtDistrictViewModel.Name_En), null, null);
        }
        public async Task<SelectList> DdlCourt()
        {
            var response = await _mediator.Send(new GetCourtTypeQuery());
            var ViewModel = _mapper.Map<List<CourtTypeViewModel>>(response.Data);
            return new SelectList(ViewModel, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null);
        }
        public async Task<SelectList> LoadCaseNature()
        {
            var response = await _mediator.Send(new GetQueryCaseCategory());
            var CaseNatures = _mapper.Map<List<CaseNatureViewModel>>(response.Data);
            return new SelectList(CaseNatures, nameof(CaseNatureViewModel.Id), nameof(CaseNatureViewModel.Name_En), null, null);
        }
        public async Task<JsonResult> LoadCaseCategory(Guid CourtTypeId)
        {
            var response = await _mediator.Send(new GetQueryCaseCategory { CourtTypeId = CourtTypeId });
            var CaseNatures = _mapper.Map<List<CaseNatureViewModel>>(response.Data);
            var data = Json(CaseNatures);
            return data;
        }
        public async Task<SelectList> LoadCaseKinds()
        {
            var response = await _mediator.Send(new CaseKindAllCacheQuery());
            var CaseKind = _mapper.Map<List<CaseKindViewModel>>(response.Data);
            return new SelectList(CaseKind, nameof(CaseKindViewModel.Id), nameof(CaseKindViewModel.CaseKind), null, null);

        }
        public async Task<SelectList> DdlLoadDistrict(int StateCode)
        {
            var districts = await _mediator.Send(new GetDistrictQuery() { StateCode = StateCode });
            var districtViewModel = _mapper.Map<List<DistrictViewModel>>(districts.Data);
            return new SelectList(districtViewModel, nameof(DistrictViewModel.Code), nameof(DistrictViewModel.Name_En), null, null);
        }
        public async Task<SelectList> DdlCaseStages()
        {
            var stages = await _mediator.Send(new CaseStageCacheAllQuery());
            var caseStagesViewModel = _mapper.Map<List<CaseStageViewModel>>(stages.Data);
            return new SelectList(caseStagesViewModel, nameof(CaseStageViewModel.Id), nameof(CaseStageViewModel.CaseStage), null, null);
        }
        public async Task<SelectList> DdlClient()
        {
            var response = await _mediator.Send(new GetAllClientCachedQuery());
            var viewModel = _mapper.Map<List<GClientViewModel>>(response.Data);
            return new SelectList(viewModel, nameof(GClientViewModel.Id), nameof(GClientViewModel.Name), null, null);
        }

        public async Task<SelectList> UserCaseTitle()
        {
            var response = await _mediator.Send(new GetCaseDetailsQuery() { PageNumber = 1, PageSize = 5000 });
            var viewModel = _mapper.Map<List<GetCaseViewModel>>(response.Data);
            return new SelectList(viewModel, nameof(GetCaseViewModel.Id), nameof(GetCaseViewModel.CaseTitle), null, null);

        }
        #endregion

        public async Task<JsonResult> LoadDistricts(int StateCode)
        {
            var districts = await _mediator.Send(new GetDistrictQuery() { StateCode = StateCode });
            var data = Json(districts);
            return data;
        }
        public async Task<JsonResult> LoadCourtDistrict(int DistrictId)
        {
            var districts = await _mediator.Send(new GetCourtDistrictQuery() { DistrictId = DistrictId });
            var data = Json(districts);
            return data;
        }

        public async Task<JsonResult> LoadCourtDistrictByState(int StateId)
        {
            var districts = await _mediator.Send(new GetCourtDistrictCachedQuery() { StateId = StateId });
            var data = Json(districts);
            return data;
        }

        public async Task<JsonResult> LoadCourt(Guid CourtTypeId)
        {
            var response = await _mediator.Send(new GetCourtMasterAllQuery()
            {
                CourtTypeId = CourtTypeId

            });
            return Json(response);
        }
        public async Task<JsonResult> LoadCourtType()
        {
            var response = await _mediator.Send(new GetCourtTypeQuery());
            return Json(response);
        }

        public async Task<JsonResult> LoadCourtComplex(Guid CDistrictId)
        {
            var response = await _mediator.Send(new GetCourtComplexCacheQuery() { CourtDistrictId = CDistrictId });
            return Json(response);
        }
        public async Task<JsonResult> LoadTypeOfCase(Guid natureId)
        {
            var caseType = await _mediator.Send(new GetAllTypeOfCasesQuery(1, 100) { CategoryId = natureId });
            var data = Json(caseType);
            return data;
        }
        public async Task<JsonResult> LCCByCourtTypeStage(Guid CourtType, int StateId)
        {
            var CCatogories = await _mediator.Send(new GetQueryCaseCategory()
            {
                StateCode = StateId,
                CourtTypeId = CourtType
            });
            if (CCatogories.Succeeded)
            {
                var data = Json(CCatogories);
                return data;
            }
            return null;
        }

        public async Task<JsonResult> LoadCourtBench(Guid CourtTypeId, int StateId, Guid ComplexId)
        {
            var dt = await _mediator.Send(new GetCourtBenchQuery(1, 100) { StateId = StateId, CourtTypeId = CourtTypeId, CourtId = ComplexId });
            var data = Json(dt);
            return data;
        }

        #region Case Proceeding & Sub Proceeding
        public async Task<SelectList> DdlProcHeads()
        {
            var response = await _mediator.Send(new GetProceedingHeadQuery());
            var viewModel = _mapper.Map<List<ProceedingHeadViewModel>>(response.Data);
            return new SelectList(viewModel, nameof(ProceedingHeadViewModel.Id), nameof(ProceedingHeadViewModel.Name_En), null, null);
        }
        public async Task<JsonResult> DdlSubProcHeads(Guid Id)
        {
            var response = await _mediator.Send(new GetProceedingSubHeadQuery { HeadId = Id });
            return Json(response);
        }
        #endregion

        #region Case Work And Sub Work Area
        public async Task<SelectList> DdlWorks()
        {
            var response = await _mediator.Send(new GetWorkMasterCommand());
            var viewModel = _mapper.Map<List<WorkMasterViewModel>>(response.Data);
            return new SelectList(viewModel, nameof(WorkMasterViewModel.Id), nameof(WorkMasterViewModel.Work_En), null, null);
        }
        public async Task<JsonResult> DdlSubWorks(Guid WorkId)
        {
            var response = await _mediator.Send(new GWorkSubMstQuery { WorkId = WorkId });
            return Json(response);
        }
        #endregion

        #region DOType
        public SelectList DOTypes()
        {
            return new SelectList(StaticDropDownDictionaries.DOTypes(), "Key", "Value");
        }

        public JsonResult DOCTypes()
        {
            var dtcData = StaticDropDownDictionaries.DOTypes();
            return Json(dtcData);
        }

        public async Task<JsonResult> DdlDOTypes(int TypeId)
        {
            var response = await _mediator.Send(new GetAllDOTypeCachedQuery { TypeId = TypeId });
            return Json(response);
        }
        public async Task<SelectList> DdlCDOTypes(int TypeId)
        {
            var response = await _mediator.Send(new GetAllDOTypeCachedQuery { TypeId = TypeId });
            var ViewModel = _mapper.Map<List<DOTypeViewModel>>(response.Data);
            return new SelectList(ViewModel, nameof(DOTypeViewModel.Id), nameof(DOTypeViewModel.Name_En), null, null);
        }
        #endregion

        #region Static Dropdown Region

        public SelectList DdlYears()
        {
            return new SelectList(StaticDropDownDictionaries.Year(), "Key", "Value");
        }

        public SelectList FirstTtitleList()
        {
            return new SelectList(StaticDropDownDictionaries.FirstTitle(), "Key", "Value");
        }
        public SelectList SecondTtitleList()
        {
            return new SelectList(StaticDropDownDictionaries.SecoundTitle(), "Key", "Value");
        }

        public SelectList DdlCaseStatus()
        {
            return new SelectList(StaticDropDownDictionaries.CaseStatus().OrderBy(v => v.Value), "Key", "Value");
        }
        public SelectList DdlCaseTitle()
        {
            return new SelectList(StaticDropDownDictionaries.CaseTitle().OrderBy(v => v.Value), "Key", "Value");
        }
        public SelectList DdlCadres()
        {
            return new SelectList(StaticDropDownDictionaries.Cadres().OrderBy(v => v.Value), "Key", "Value");
        }
        public SelectList DdlStrength()
        {
            return new SelectList(StaticDropDownDictionaries.Stength().OrderBy(v => v.Key), "Key", "Value");
        }
        public SelectList FormPrintingTypes()
        {
            return new SelectList(StaticDropDownDictionaries.FormPrintingTypes(), "Key", "Value");
        }


        #endregion

        #region First & Secound Title
        public SelectList FSTypes()
        {
            return new SelectList(StaticDropDownDictionaries.FSType(), "Key", "Value");
        }

        public JsonResult FSJTypes()
        {
            var dtcData = StaticDropDownDictionaries.DOTypes();
            return Json(dtcData);
        }

        public async Task<JsonResult> DdlFSTitle(int TypeId)
        {
            var response = await _mediator.Send(new FSTitleGetAllCacheQuery { TypeId = TypeId });
            return Json(response);
        }
        public async Task<SelectList> DdlFSTypes(int TypeId)
        {
            var response = await _mediator.Send(new FSTitleGetAllCacheQuery { TypeId = TypeId });
            var ViewModel = _mapper.Map<List<FSTitleLViewModel>>(response.Data);
            return new SelectList(ViewModel, nameof(FSTitleLViewModel.Id), nameof(FSTitleLViewModel.Name_En), null, null);
        }
        #endregion

        #region Lawyer Master
        public async Task<SelectList> DdlLawyerAsync()
        {
            var response = await _mediator.Send(new LawyerGetAllCacheQuery());
            var ViewModel = _mapper.Map<List<LawyerLViewModel>>(response.Data);
            return new SelectList(ViewModel, nameof(LawyerLViewModel.Id), nameof(LawyerLViewModel.Name), null, null);
        }
        #endregion

        #region Generilize Dropdowns
        public JsonResult FieldType()
        {
            var years = StaticDropDownDictionaries.FieldType().OrderBy(o => o.Key);
            List<DropDownSViewModel> dt = new List<DropDownSViewModel>();
            foreach (var y in years)
                dt.Add(new DropDownSViewModel { Id = y.Key.ToString(), Name = y.Value });
            return Json(dt);
        }

        public async Task<SelectList> GetForms()
        {
            var response = await _mediator.Send(new GetFormBuilderCachedQuery());
            if (response.Succeeded)
            {
                var fields = _mapper.Map<List<GenFormAttrViewModel>>(response.Data.OrderBy(o => o.FormName));
                return new SelectList(fields, nameof(GenFormAttrViewModel.Id), nameof(GenFormAttrViewModel.FormName), null, null); ;
            }
            return null;
        }
        public async Task<JsonResult> GetFormFieldsById(Guid id)
        {
            var response = await _mediator.Send(new GetFormBuilderCachedByIdQuery() { Id = id });
            List<DropDownGViewModel> dt = new List<DropDownGViewModel>();
            if (response.Succeeded)
            {
                var fields = response.Data.FieldDetails;
                foreach (var y in fields)
                    dt.Add(new DropDownGViewModel { Id = y.Key, Name = y.Name });
            }
            return Json(dt);
        }
        public async Task<JsonResult> TemplateFields(Guid TemplateId)
        {
            var response = await _mediator.Send(new GetTemplateInfoCachedByIdQuery() { Id = TemplateId });
            List<DropDownSViewModel> dt = new List<DropDownSViewModel>();
            if (response.Succeeded)
            {
                var fields = response.Data.Tags;
                foreach (var y in fields)
                    dt.Add(new DropDownSViewModel { Id = y.Tag, Name = y.Tag });
            }
            return Json(dt);
        }
        public JsonResult SearchBy()
        {
            var years = StaticDropDownDictionaries.CaseSearchBy().OrderBy(o => o.Value);
            List<DropDownSViewModel> dt = new List<DropDownSViewModel>();
            foreach (var y in years)
                dt.Add(new DropDownSViewModel { Id = y.Key, Name = y.Value });
            return Json(dt);
        }
        public JsonResult DdJYears()
        {
            var years = StaticDropDownDictionaries.Year();
            List<DropDownIViewModel> dt = new List<DropDownIViewModel>();
            foreach (var y in years)
                dt.Add(new DropDownIViewModel { Id = y.Key, Name = y.Value });
            return Json(dt);
        }
        public async Task<JsonResult> GDdlDOTypes(int TypeId)
        {
            var response = await _mediator.Send(new GetAllDOTypeCachedQuery { TypeId = TypeId });
            if (response.Succeeded)
            {
                var dt = _mapper.Map<List<DropDownGViewModel>>(response.Data);
                return Json(dt);
            }
            return Json(null);
        }
        public async Task<JsonResult> GDdlStages()
        {
            var response = await _mediator.Send(new CaseStageCacheAllQuery());
            if (response.Succeeded)
            {
                var dt = _mapper.Map<List<DropDownGViewModel>>(response.Data);
                return Json(dt);
            }
            return Json(null);
        }
        public async Task<JsonResult> GDdlCaseCategory()
        {
            var response = await _mediator.Send(new GetQueryCaseCategory());
            if (response.Succeeded)
            {
                var dt = _mapper.Map<List<DropDownGViewModel>>(response.Data);
                return Json(dt);
            }
            return Json(null);
        }
        public async Task<JsonResult> GetCompTitlesByCases(List<Guid> caseIds)
        {
            var response = await _mediator.Send(new GetCaseTitleQuery() { CaseIds = caseIds });
            if (response.Succeeded)
            {
                var dtl = response.Data;
                string[] lines = dtl.First().Title.Split(
                            new string[] { Environment.NewLine },
                            StringSplitOptions.None
);
                var dt = _mapper.Map<List<DropDownGViewModel>>(response.Data);
                return Json(dt);
            }
            return Json(null);
        }
        public async Task<SelectList> GetDraftings()
        {
            var response = await _mediator.Send(new GetFormBuilderCachedQuery());
            if (response.Succeeded)
            {
                var dt = _mapper.Map<List<DropDownGViewModel>>(response.Data);
                return new SelectList(dt, nameof(DropDownGViewModel.Id), nameof(DropDownGViewModel.Name), null, null);
            }
            return null;
        }

        public async Task<SelectList> GetTemplates()
        {
            var response = await _mediator.Send(new GetTemplateInfoQuery());
            if (response.Succeeded)
            {
                var dt = _mapper.Map<List<DropDownGViewModel>>(response.Data);
                return new SelectList(dt, nameof(DropDownGViewModel.Id), nameof(DropDownGViewModel.Name), null, null);
            }
            return null;
        }
        #endregion

        #region Read File
        public string ReadTemplate(string fPath, string fName)
        {
            string DirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", "Templates");
            string filePath = Path.Combine(DirPath, fName);
            if (!System.IO.File.Exists(filePath))
                return "File Not found";
            string fileContent = string.Empty;
            using (FileStream inputFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // Load the file stream into a Word document
                using (WordDocument document = new WordDocument(inputFileStream, FormatType.Automatic))
                {
                    fileContent = document.GetText();
                }
            }
            return fileContent;
        }

        public void ConvertHtmlToOpenXml(Body body, string htmlContent)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            foreach (var node in htmlDoc.DocumentNode.ChildNodes)
            {
                if (node.Name == "h1")
                {
                    var paragraph = new Paragraph(new Run(new Text(node.InnerText.Replace("&nbsp;", " "))));
                    paragraph.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId() { Val = "Heading1" });
                    body.AppendChild(paragraph);
                }
                else if (node.Name == "p")
                {
                    var paragraph = new Paragraph(new Run(new Text(node.InnerText.Replace("&nbsp;", " "))));
                    body.AppendChild(paragraph);
                }                
                // Add more HTML tag handling as needed
            }
        }
       
        #endregion
    }
}