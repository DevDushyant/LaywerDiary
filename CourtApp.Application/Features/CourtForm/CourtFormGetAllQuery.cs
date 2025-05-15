﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CourtForm;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.FormBuilder;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourtApp.Application.Features.CourtForm
{
    public class CourtFormGetAllQuery : IRequest<Result<List<CourtFormDto>>>
    {
        public int StateId { get; set; }
    }
    public class CourtFormGetAllQueryHandler : IRequestHandler<CourtFormGetAllQuery, Result<List<CourtFormDto>>>
    {
        private readonly ICourtFormTypeRepository repository;
        private readonly ILanguageRepository langRepository;
        public CourtFormGetAllQueryHandler(ICourtFormTypeRepository repository, ILanguageRepository langRepository  )
        {
            this.repository = repository;
            this.langRepository = langRepository;
        }
        public async Task<Result<List<CourtFormDto>>> Handle(CourtFormGetAllQuery request, CancellationToken cancellationToken)
        {

            var frmDetails = await (
                        from form in repository.Entities.AsNoTracking()
                        join lang in langRepository.Entities.AsNoTracking()
                            on form.StateId equals lang.StateId into langGroup
                        from lg in langGroup.DefaultIfEmpty()
                        from langItem in lg.Languages
                            .Where(l => l.Code == form.LanguageCode)
                            .DefaultIfEmpty()

                        where request.StateId == 0 || form.StateId == request.StateId

                        select new CourtFormDto
                        {
                            Id = form.Id,
                            CaseCategory = form.CaseCategory != null ? form.CaseCategory.Name_En : null,
                            CourtType = form.CourtType != null ? form.CourtType.CourtType : null,
                            Language = langItem != null ? langItem.Name : "", // resolved language name
                            FormName = form.FormName,
                            StateName = form.State != null ? form.State.Name_En : null
                        }
                    ).ToListAsync(cancellationToken);

            return frmDetails.Any()
                ? await Result<List<CourtFormDto>>.SuccessAsync(frmDetails)
                : await Result<List<CourtFormDto>>.FailAsync("No records found.");
        }
    }
}
