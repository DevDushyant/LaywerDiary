using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Typeofcasess.Commands
{
    public class UpdateTypeOfCasesCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int CaseNatureId { get; set; }        
        public string Typeofcases { get; set; }
        public UpdateTypeOfCasesCommand()
        {

        }

    }

    public class UpdateTypeofcasesCommandHandler : IRequestHandler<UpdateTypeOfCasesCommand, Result<int>>
    {
        private readonly ITypeOfCasesRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateTypeofcasesCommandHandler(ITypeOfCasesRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateTypeOfCasesCommand request, CancellationToken cancellationToken)
        {
            var naturedetail = await repository.GetByIdAsync(request.Id);
            if (naturedetail == null)
                return Result<int>.Fail($"Case kind detail Not Found.");
            else
            {
                naturedetail.Typeofcases = request.Typeofcases;
                naturedetail.CaseNatureId = request.CaseNatureId;
                await repository.UpdateAsync(naturedetail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(naturedetail.Id);
            }
        }
    }
}
