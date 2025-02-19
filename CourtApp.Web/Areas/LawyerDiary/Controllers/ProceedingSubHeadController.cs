using CourtApp.Application.Features.ProceedingSubHead;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class ProceedingSubHeadController : BaseController<ProceedingSubHeadController>
    {
        public IActionResult Index()
        {
            var model = new ProceedingSubHeadViewModel();
            return View(model);
        }
        //public async Task<IActionResult> LoadAllAsync1(int pageNumber, int pageSize)
        //{
        //    var response = await _mediator.Send(new GetProceedingSubHeadQuery()
        //    {
        //        PageNumber = pageNumber,
        //        PageSize = pageSize
        //    });
        //    if (response.Succeeded)
        //    {
        //        var result = _mapper.Map<List<ProceedingSubHeadViewModel>>(response.Data);
        //        var viewModel = new PaginationViewModel<ProceedingSubHeadViewModel>();
        //        viewModel.Data = result;
        //        viewModel.HasPreviousPage = response.HasPreviousPage;
        //        viewModel.HasNextPage = response.HasNextPage;
        //        viewModel.TotalPages = response.TotalPages;
        //        viewModel.TotalCount = response.TotalCount;
        //        viewModel.PageSize = pageSize;
        //        viewModel.PageNumber = pageNumber;
        //        return PartialView("_ViewAll", viewModel);
        //    }
        //    return null;
        //}

        [HttpPost]
        public async Task<IActionResult> LoadAllAsync([FromForm] DataTableRequest request)
        {
            var response = await _mediator.Send(new GetProceedingSubHeadQuery
            {
                PageNumber = 1,
                PageSize = 10
            });

            if (response.Succeeded)
            {
                var result = _mapper.Map<List<ProceedingSubHeadViewModel>>(response.Data);
                var totalRecords = response.TotalCount;

                // Filter data if needed (based on request.Search.Value)
                //if (!string.IsNullOrEmpty(request.Search.Value))
                //{
                //    result = result.Where(r => r.Name_En.Contains(request.Search.Value)).ToList();
                //}

                //var filteredRecords = result.Count;

                //// Apply sorting
                //if (request.Order.Count > 0)
                //{
                //    var sortColumn = request.Columns[request.Order[0].Column].Data;
                //    var sortDirection = request.Order[0].Dir;

                //    if (sortDirection == "asc")
                //    {
                //        result = result.OrderBy(x => x.GetType().GetProperty(sortColumn)?.GetValue(x, null)).ToList();
                //    }
                //    else
                //    {
                //        result = result.OrderByDescending(x => x.GetType().GetProperty(sortColumn)?.GetValue(x, null)).ToList();
                //    }
                //}

                // Apply pagination to the filtered and sorted data
                //result = result.Skip(request.Start).Take(request.Length).ToList();

                // Create DataTableResponse
                var dataTableResponse = new
                {
                    draw = 10,
                    recordsTotal = 1000,
                    recordsFiltered = (int)totalRecords,
                    data = result,
                    totalPages = response.TotalPages
                };

                return Json(dataTableResponse);
            }

            return Json(new { error = "Unable to load data" });
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                var ViewModel = new ProceedingSubHeadViewModel();
                ViewModel.PHeads = await DdlProcHeads();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetProceedingSubHeadGetByIdQuery() { Id = Id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<ProceedingSubHeadViewModel>(response.Data);
                    ViewModel.PHeads = await DdlProcHeads();
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Edit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid Id, ProceedingSubHeadViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    var cmd = _mapper.Map<CreateProcSubHeadCommand>(viewModel);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                    {
                        Id = result.Data;
                        _notify.Success($"Proceeding Head with ID {result.Data} Created.");
                    }
                    else
                    {
                        viewModel.Message = result.Message;
                        var html = await _viewRenderer.RenderViewToStringAsync("_Create", viewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    var cmd = _mapper.Map<UpdateProcSubHeadCommand>(viewModel);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded) _notify.Information($"Proceeding Head with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetProceedingSubHeadQuery()
                {
                    PageNumber = 1,
                    PageSize = 1000
                });
                if (response.Succeeded)
                {
                    var result = _mapper.Map<List<ProceedingSubHeadViewModel>>(response.Data);
                    var vm = new PaginationViewModel<ProceedingSubHeadViewModel>();
                    vm.Data = result;
                    vm.HasPreviousPage = response.HasPreviousPage;
                    vm.HasNextPage = response.HasNextPage;
                    vm.TotalPages = response.TotalPages;
                    vm.TotalCount = response.TotalCount;
                    vm.PageSize = 10;
                    vm.PageNumber = 1;
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", vm);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", viewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteProcSubHeadCommand { Id = id });

            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Proc sub Head with ID {id} Deleted.");
                var response = await _mediator.Send(new GetProceedingSubHeadQuery()
                {
                    PageNumber = 1,
                    PageSize = 1000
                });
                if (response.Succeeded)
                {
                    var result = _mapper.Map<List<ProceedingSubHeadViewModel>>(response.Data);
                    var vm = new PaginationViewModel<ProceedingSubHeadViewModel>();
                    vm.Data = result;
                    vm.HasPreviousPage = response.HasPreviousPage;
                    vm.HasNextPage = response.HasNextPage;
                    vm.TotalPages = response.TotalPages;
                    vm.TotalCount = response.TotalCount;
                    vm.PageSize = 10;
                    vm.PageNumber = 1;
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", vm);
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
    }
}
