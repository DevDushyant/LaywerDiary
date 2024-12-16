using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class CreateCourtDistrictCommand : IRequest<Result<Guid>>
    {
        //public string Name_En { get; set; }
        //public string Name_Hn { get; set; }
        public int StateId { get; set; }
        //public int DistrictId { get; set; }
        //public string Abbreviation { get; set; }
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
            if (request.CourtDistricts.Count > 0)
            {
                Guid id = Guid.Empty;
                foreach (var c in request.CourtDistricts)
                {
                    var detail = repository.Entities
                                .Where(w => w.Name_En.ToLower()
                                .Equals(c.Name_En.ToLower()))
                                .FirstOrDefault() ?? null;
                    if (detail == null)
                    {
                        var cdt = new CourtDistrictEntity()
                        {
                            Name_En = c.Name_En,
                            Name_Hn = c.Name_Hn,
                            StateId = request.StateId                            
                        };
                        await repository.InsertAsync(cdt);
                        await _unitOfWork.Commit(cancellationToken);
                        id = cdt.Id;
                    }
                    else
                        return Result<Guid>.Fail("District court district is already exist!");
                }
                return Result<Guid>.Success(id);
            }
            return Result<Guid>.Fail("Court district is not supplied!");
            //var IsExist = repository.Entities.Where(w => w.StateId == request.StateId && w.Abbreviation.Equals(request.Abbreviation)).FirstOrDefault();
            //if (IsExist != null)
            //    return Result<Guid>.Fail("The given abbreviation is already exists!");
            //var bookType = mapper.Map<CourtDistrictEntity>(request);
            //await repository.InsertAsync(bookType);
            //await _unitOfWork.Commit(cancellationToken);
            //return Result<Guid>.Success(bookType.Id);
        }
    }
}
