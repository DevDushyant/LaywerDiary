using AspNetCoreHero.Results;
using CourtApp.Application.Features.BookMasters.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Command
{
    public class UpdateCourtTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string CourtType { get; set; }
    }

    public class UpdateCourtTypeCommandHandler :IRequestHandler<UpdateCourtTypeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtTypeRepository _Repository;

        public UpdateCourtTypeCommandHandler(ICourtTypeRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdateCourtTypeCommand command, CancellationToken cancellationToken)
        {
            var courtType = await _Repository.GetByIdAsync(command.Id);

            if (courtType == null)
            {
                return Result<int>.Fail($"Court Type Not Found.");
            }
            else
            {
                courtType.Id = command.Id;
                courtType.CourtType = command.CourtType;
                await _Repository.UpdateAsync(courtType);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(courtType.Id);
            }
        }
    }
}
