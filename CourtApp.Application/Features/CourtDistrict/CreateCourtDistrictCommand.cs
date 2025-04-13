using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class CreateCourtDistrictCommand : IRequest<Result<Guid>>
    {

        public int StateId { get; set; }
        public List<StateCourtDistrict> CourtDistricts { get; set; }
    }
    public class StateCourtDistrict
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class CreateCourtDistrictCommandHandler : IRequestHandler<CreateCourtDistrictCommand, Result<Guid>>
    {
        private readonly ICourtDistrictRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCourtDistrictCommandHandler(ICourtDistrictRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public async Task<Result<Guid>> Handle(CreateCourtDistrictCommand request, CancellationToken cancellationToken)
        {
            if (request.CourtDistricts == null || request.CourtDistricts.Count == 0)
            {
                return Result<Guid>.Fail("Court district is not supplied!");
            }

            // Use HashSet for faster lookup and remove unnecessary trims inside the loop
            var inputNameMap = request.CourtDistricts
                .Select(cd => new
                {
                    Original = cd,
                    NormalizedName = cd.Name_En?.Trim().ToLower()
                })
                .Where(x => !string.IsNullOrWhiteSpace(x.NormalizedName))
                .ToList();

            var normalizedNames = inputNameMap.Select(x => x.NormalizedName).ToHashSet();

            // Fetch existing names (case-insensitive match)
            var existingNames = repository.Entities
                .Where(w => w.StateId == request.StateId && normalizedNames.Contains(w.Name_En.ToLower()))
                .Select(w => w.Name_En)
                .ToList();

            if (existingNames.Any())
            {
                return Result<Guid>.Fail("Record is already exist: " + string.Join(", \n", existingNames));
            }

            // Map to entities using original objects
            var newEntities = inputNameMap.Select(x => new CourtDistrictEntity
            {
                Name_En = x.Original.Name_En?.Trim(),
                Name_Hn = x.Original.Name_Hn?.Trim(),
                StateId = request.StateId
            }).ToList();

            // Bulk insert
            await repository.InsertRangeAsync(newEntities);
            await _unitOfWork.Commit(cancellationToken);

            // Return last inserted ID
            return Result<Guid>.Success(newEntities.Last().Id);
        }
    }
}
