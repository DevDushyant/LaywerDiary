using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    public class CreateCourtComplexCommand : IRequest<Result<Guid>>
    {
        public int StateId { get; set; }
        public Guid CourtDistrictId { get; set; }
        //public string Name_En { get; set; }
        //public string Name_Hn { get; set; }
        //public string Abbreviation { get; set; }
        public List<DistrictComplex> Complexes { get; set; }
    }
    public class DistrictComplex
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class CreateCourtComplexCommandHandler : IRequestHandler<CreateCourtComplexCommand, Result<Guid>>
    {
        private readonly ICourtComplexRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCourtComplexCommandHandler(ICourtComplexRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateCourtComplexCommand request, CancellationToken cancellationToken)
        {
            if (request.Complexes.Count > 0)
            {
                Guid id = Guid.Empty;
                foreach (var c in request.Complexes)
                {
                    var detail = repository.Entities
                                .Where(w => w.Name_En.ToLower()
                                .Equals(c.Name_En.ToLower())
                                && w.CourtDistrictId == request.CourtDistrictId
                                && w.StateId == request.StateId
                                )
                                .FirstOrDefault() ?? null;
                    if (detail == null)
                    {
                        var cdt = new CourtComplexEntity()
                        {
                            Name_En = c.Name_En,
                            Name_Hn = c.Name_Hn,
                            StateId = request.StateId,
                            CourtDistrictId = request.CourtDistrictId
                        };
                        await repository.InsertAsync(cdt);
                        await _unitOfWork.Commit(cancellationToken);
                        id = cdt.Id;
                    }
                    else
                        return Result<Guid>.Fail("Error! the Given name is already exist! " + detail.Name_En + " ");
                }
                return Result<Guid>.Success(id);
            }
            return Result<Guid>.Fail("Court complexes is not supplied!");
        }
    }
}
