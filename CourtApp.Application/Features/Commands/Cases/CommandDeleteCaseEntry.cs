using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Commands.Cases
{
    public class CommandDeleteCaseEntry : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
