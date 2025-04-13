using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseNatures.Command
{
    public class CreateCaseNatureCommand : IRequest<Result<Guid>>
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public Guid CourtTypeId { get; set; }
    }
    public class CreateCaseNatureCommandHandler : IRequestHandler<CreateCaseNatureCommand, Result<Guid>>
    {
        private readonly ICaseNatureRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseNatureCommandHandler(ICaseNatureRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateCaseNatureCommand request, CancellationToken cancellationToken)
        {

            // Build dynamic predicate
            var predicate = PredicateBuilder.True<NatureEntity>();

            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtTypeId.Equals(request.CourtTypeId));

            if (!string.IsNullOrWhiteSpace(request.Name_En))
                predicate = predicate.And(b => b.Name_En.ToLower().Trim() == request.Name_En.ToLower().Trim());

            // Check for duplicates
            var isExists = await repository.CaseNatures
                .Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (isExists != null)
                return Result<Guid>.Fail($"Record is already exists.");

            // Insert new record
            var entity = mapper.Map<NatureEntity>(request);
            await repository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(entity.Id);
        }
    }

}
