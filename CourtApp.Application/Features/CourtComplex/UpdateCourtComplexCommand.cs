using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CourtApp.Application.Features.CourtComplex
{
    public class UpdateCourtComplexCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public Guid CourtDistrictId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
    public class UpdateCourtComplexCommandHandler : IRequestHandler<UpdateCourtComplexCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtComplexRepository repository;
        public UpdateCourtComplexCommandHandler(ICourtComplexRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork= _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCourtComplexCommand cmd, CancellationToken cancellationToken)
        {
            var detailById = await repository.GetByIdAsync(cmd.Id);
            if (detailById == null)
                return Result<Guid>.Fail($"Court complex not found.");
            else
            {
                //detailById.DistrictCode = cmd.DistrictId;
                detailById.StateId = cmd.StateId;
                detailById.Abbreviation = cmd.Abbreviation;
                detailById.Name_En = cmd.Name_En;
                detailById.Name_Hn = cmd.Name_Hn;
                detailById.CourtDistrictId = cmd.CourtDistrictId;
                await repository.UpdateAsync(detailById);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detailById.Id);
            }
        }
    }
}
