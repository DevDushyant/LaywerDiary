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

namespace CourtApp.Application.Features.Lawyer
{
    public class LawyerUpdateCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EnrollNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
    public class LawyerUpdateCommandHandler : IRequestHandler<LawyerUpdateCommand, Result<Guid>>
    {
        private readonly ILawyerRepository repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public LawyerUpdateCommandHandler(ILawyerRepository repository,
            IMapper _mapper, IUnitOfWork _uow)
        {
            this.repository = repository;
            this._mapper = _mapper;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(LawyerUpdateCommand request, CancellationToken cancellationToken)
        {
            var EntityDetail = await repository.GetByIdAsync(request.Id);
            if (EntityDetail != null)
            {
                EntityDetail.EnrollNumber = request.EnrollNumber;
                EntityDetail.Mobile = request.Mobile;
                EntityDetail.Email = request.Email;
                EntityDetail.Address = request.Address;
                EntityDetail.FirstName = request.FirstName;
                EntityDetail.LastName = request.LastName;
                EntityDetail.MiddleName = request.MiddleName;
                await _uow.Commit(cancellationToken);
                return Result<Guid>.Success(EntityDetail.Id);
            }
            return Result<Guid>.Fail("Record is not exists!");
        }
    }
}
