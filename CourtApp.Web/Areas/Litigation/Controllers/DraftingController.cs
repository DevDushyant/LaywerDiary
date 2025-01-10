using CourtApp.Application.Features.FormBuilder;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Litigation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;


namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Litigation")]
    public class DraftingController : BaseController<DraftingController>
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCaseDarftingQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<FormCaseMappingViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        public async Task<IActionResult> GetTemplateFields(Guid TemplateId, FormBuilderViewModel ViewModel)
        {
            var response = await _mediator.Send(new GetFormBuilderCachedByIdQuery()
            {
                Id = TemplateId
            });
            if (response.Succeeded)
            {
                var fieldPropeties = response.Data.FieldDetails;
                var Dt = _mapper.Map<List<FormProperties>>(fieldPropeties);
                ViewModel.FieldDetails = Dt;
                return PartialView("_FormFields", ViewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new FormBuilderViewModel();
                ViewModel.DraftingForms = await GetDraftings();
                ViewModel.Templates = await GetTemplates();
                ViewModel.Cases = await UserCaseTitle(Guid.Empty);
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                return null;
            }
        }
        public async Task<IActionResult> Petition(Guid id, FormBuilderViewModel ViewModel)
        {
            if (id == Guid.Empty)
            {
                ViewModel.Templates = await GetTemplates();
                ViewModel.DraftingForms = await GetDraftings();
                ViewModel.Cases = await UserCaseTitle(Guid.Empty);
                return View("_CreateOrEdit", ViewModel);
            }
            else
            {
                var result = await _mediator.Send(new GetCaseDarftingCachedByIdQuery() { Id = id });
                if (result.Succeeded)
                {
                    ViewModel = _mapper.Map<FormBuilderViewModel>(result.Data);
                    ViewModel.Templates = await GetTemplates();
                    ViewModel.DraftingForms = await GetDraftings();
                    ViewModel.Cases = await UserCaseTitle(Guid.Empty);
                    ViewModel.FieldDetails = _mapper.Map<List<FormProperties>>(ViewModel.FieldDetails);
                    return View("_CreateOrEdit", ViewModel);
                }
            }
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid id, FormBuilderViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == Guid.Empty && TempData.ContainsKey("RecordExists") == false)
                    {
                        var Command = _mapper.Map<CreateCaseDraftingDetailCommand>(ViewModel);
                        var result = await _mediator.Send(Command);
                        if (result.Succeeded)
                            _notify.Success($"Case Drafting saved successfull!");
                        else
                        {
                            ViewModel.StatusMessage = result.Message;
                            ModelState.AddModelError(string.Empty, result.Message);
                            TempData["RecordExists"] = true;
                        }
                    }
                    else
                    {
                        var Command = _mapper.Map<UpdateCaseDraftingDetailCommand>(ViewModel);
                        var result = await _mediator.Send(Command);
                        if (result.Succeeded)
                            _notify.Success($"Case drafting information is updated successfully!");
                    }
                    ViewModel.Cases = await UserCaseTitle(Guid.Empty);
                    ViewModel.Templates = await GetDraftings();
                    return View("_CreateOrEdit", ViewModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html });
            }
        }

        public async Task<IActionResult> GetReport(Guid id)
        {
            if (id != Guid.Empty)
            {
                var response = await _mediator.Send(new GetCaseMappingDetailInfoQuery() { Id = id });
                if (response.Succeeded)
                {
                    var dt = response.Data;
                    var Content = ReadTemplate(dt.TemplatePath, dt.TemplateName);
                    string FinalContent = string.Empty;
                    foreach (var tg in dt.TagValues)
                    {
                        FinalContent = Content.Replace(tg.Key.Trim(), tg.Value.Trim());
                        Content = FinalContent;
                    }
                    // Create a memory stream to hold the document
                    using (var stream = new MemoryStream())
                    {
                        // Create a Wordprocessing document
                        using (var document = WordprocessingDocument.Create(stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
                        {
                            // Add a main document part
                            var mainPart = document.AddMainDocumentPart();
                            mainPart.Document = new Document();
                            var body = mainPart.Document.AppendChild(new Body());

                            // Add content to the document
                            //var paragraph = body.AppendChild(new Paragraph());
                            //var run = paragraph.AppendChild(new Run());
                            //run.AppendChild(new Text("Hello, this is a sample Word document created using Open XML SDK."));
                            //run.AppendChild(new Text(Content));

                            // Save the document
                            // Convert HTML to Open XML elements
                            ConvertHtmlToOpenXml(body, Content);
                            mainPart.Document.Save();
                        }
                        // Return the document as a file
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "GeneratedDocument.docx");
                    }
                }

            }
            return null;
        }
    }
}
