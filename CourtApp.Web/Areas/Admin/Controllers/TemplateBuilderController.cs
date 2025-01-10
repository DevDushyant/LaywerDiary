using CourtApp.Application.Features.BookMasters.Command;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Application.Features.FormBuilder;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Admin.Models;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace CourtApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TemplateBuilderController : BaseController<TemplateBuilderController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetTemplateInfoQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TemplateDlViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<IActionResult> OnGetCreateOrEdit(Guid id, TemplateViewModel ViewModel)
        {
            if (id == Guid.Empty)
            {
                ViewModel = new TemplateViewModel();
                ViewBag.BtText = "Save";
                return View("_CreateOrEdit", ViewModel);
            }
            else
            {
                var response = await _mediator.Send(new GetTemplateInfoByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    string FilePath = response.Data.TemplatePath;
                    string FileName = response.Data.TemplateName;
                    var dt = new TemplateViewModel();
                    string TempName = FileName.Split('.')[0];
                    dt.TemplateName = TempName;
                    string TemplateBody = ReadTemplate(FilePath, FileName);
                    dt.TemplateBody = TemplateBody;
                    ViewBag.BtText = "Update";
                    return View("_CreateOrEdit", dt);
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid id, TemplateViewModel ViewModel)
        {
            if (id == Guid.Empty)
            {
                List<string> Tags = GetTags(ViewModel.TemplateBody);
                string FilePath = SaveTemplate(ViewModel);
                string docPath = FilePath.Split(';')[0];
                string docName = FilePath.Split(';')[1];
                List<TemplateTags> templateTags = new List<TemplateTags>();
                foreach (string tag in Tags)
                    templateTags.Add(new TemplateTags() { Tag = tag });
                var result = await _mediator.Send(new CreateTemplateInfoCommand()
                {
                    TemplateName = docName,
                    TemplatePath = "documents/Templates/",
                    Tags = templateTags
                });
                if (result.Succeeded)
                    _notify.Success($"Template info saved successfully.");
                else _notify.Error(result.Message);
                return RedirectToAction("OnGetCreateOrEdit", ViewModel);
            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteTemplateQueryCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Selected template is deleted successfully!");
                var response = await _mediator.Send(new GetTemplateInfoQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TemplateDlViewModel>>(response.Data);
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

        #region Template Save and Read
        private string SaveTemplate(TemplateViewModel ViewModel)
        {
            // Create a new Word document
            WordDocument document = new WordDocument();
            IWSection section = document.AddSection();
            IWParagraph paragraph = section.AddParagraph();
            paragraph.AppendText(ViewModel.TemplateBody.Trim());
            // Save the document to the wwwroot folder
            string fileName = ViewModel.TemplateName + ".docx";
            string folderPath = Path.Combine("wwwroot/documents", "Templates");
            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
            // Check if the directory exists, if not, create it
            if (!Directory.Exists(wwwRootPath))
            {
                Directory.CreateDirectory(wwwRootPath);
            }
            string filePath = Path.Combine(wwwRootPath, ViewModel.TemplateName + ".docx");
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                document.Save(fileStream, FormatType.Docx);
            }
            document.Close();
            return folderPath + ";" + fileName;
        }
        
        private List<string> GetTags(string TemplateBody)
        {
            List<string> tg = new List<string>();
            Regex regex = new Regex(@"#(.*?)#");
            MatchCollection matches = regex.Matches(TemplateBody);
            foreach (Match match in matches)
                tg.Add(match.Value);
            var Tags = tg.Distinct().ToList();
            return Tags;
        }
        #endregion

        #region Template and drafting form Field Mapping 
        public async Task<JsonResult> MapFields(Guid id)
        {
            var response = await _mediator.Send(new GetTemplateInfoCachedByIdQuery() { Id = id });
            var ViewModel = new FormTemplateMapViewModel();
            ViewModel.TemplateId = id;
            ViewModel.Forms = await GetForms();
            if (response.Succeeded)
            {
                var tempFields = response.Data.Tags;
                var mpf = new List<Mapping>();
                foreach (var field in tempFields)
                    mpf.Add(new Mapping() { Tag = field.Tag });
                ViewModel.Tags = mpf;
            }
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_TemplateFormMapping", ViewModel) });
        }

        [HttpPost]
        public async Task<JsonResult> OnPostMapping(Guid id, FormTemplateMapViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CreateTemplateFormMappingCommand>(ViewModel);
                    var mfields = _mapper.Map<List<MappingDto>>(ViewModel.Tags);
                    createCommand.FieldsMapping = mfields;
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                        _notify.Success($"Template form mapping saved successfully!");
                    else _notify.Error(result.Message);
                }
                else
                {

                }
                var response = await _mediator.Send(new GetTemplateInfoQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TemplateDlViewModel>>(response.Data);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_TemplateFormMapping", ViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }

        }
        #endregion
    }
}
