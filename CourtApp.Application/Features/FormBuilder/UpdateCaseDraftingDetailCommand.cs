﻿using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.FormBuilder;
using static CourtApp.Application.Constants.Permissions;
using Microsoft.AspNetCore.Identity;
using CourtApp.Application.Interfaces.Shared;

namespace CourtApp.Application.Features.FormBuilder
{
    public class UpdateCaseDraftingDetailCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public Guid TemplateId { get; set; }
        public Guid DraftingFormId { get; set; }
        public List<TemplateFields> FieldDetails { get; set; }
    }

    public class UpdateCaseDraftingDetailCommandHanlder : IRequestHandler<UpdateCaseDraftingDetailCommand, Result<Guid>>
    {
        private readonly ICaseDraftingRepository repository;
        private IUnitOfWork _UoW { get; set; }
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _userService;
        public UpdateCaseDraftingDetailCommandHanlder(ICaseDraftingRepository repository
            , IUnitOfWork _UoW, IMapper _mapper,
            IAuthenticatedUserService _userService)
        {
            this.repository = repository;
            this._mapper = _mapper;
            this._UoW = _UoW;
            this._userService = _userService;
        }
        public async Task<Result<Guid>> Handle(UpdateCaseDraftingDetailCommand request, CancellationToken cancellationToken)
        {
            var CaseFormDt = repository.Entities
                    .Where(w =>w.CaseId == request.CaseId && w.TemplateId == request.TemplateId)
                    .First();
            if (CaseFormDt == null)
                return Result<Guid>.Fail($"Record is not found.");
            else
            {
                CaseFormDt.CaseId = request.CaseId;
                CaseFormDt.TemplateId = request.TemplateId;
                CaseFormDt.DraftingFormId = request.DraftingFormId;
                CaseFormDt.FieldDetails = _mapper.Map<List<FormFieldValueEntity>>(request.FieldDetails);
                
                await repository.UpdateAsync(CaseFormDt);
                await _UoW.Commit(cancellationToken);
                return Result<Guid>.Success(CaseFormDt.Id);
            }
        }
    }
}
