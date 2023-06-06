using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Features.Subjects.Commands
{
    public class UpdateSubjectCommand : IRequest<Result<int>>
    {
        public string Subject { get; set; }
        public int Id { get; set; }
    }

    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, Result<int>>
    {
        private readonly ISubjectRepository _repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateSubjectCommandHandler(IMapper mapper, ISubjectRepository _repository, IUnitOfWork _unitOfWork)
        {
            this.mapper = mapper;
            this._repository = _repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _repository.GetByIdAsync(request.Id);

            if (subject == null)
            {
                return Result<int>.Fail($"Subject Not Found.");
            }
            else
            {
                subject.Subject = request.Subject;
                await _repository.UpdateAsync(subject);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(subject.Id);
            }
        }
    }
}
