using AspNetCoreHero.Results;
using MediatR;
using System;

namespace CourtApp.Application.Features.CaseProceeding
{
    public class UpdateCaseProceedingCommand:IRequest<Result<Guid>>
    {

    }
}
