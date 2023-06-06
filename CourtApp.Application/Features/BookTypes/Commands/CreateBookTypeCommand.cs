using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Features.BookTypes.Commands
{
    public partial class CreateBookTypeCommand : IRequest<Result<int>>
    {
        public string BookType { get; set; }        
    }

    public class CreateBooTypeCommandHandler : IRequestHandler<CreateBookTypeCommand, Result<int>>
    {
        private readonly IBookTypeRepository _bookTypeRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateBooTypeCommandHandler(IBookTypeRepository _bookTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._bookTypeRepository = _bookTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateBookTypeCommand request, CancellationToken cancellationToken)
        {
            var bookType = _mapper.Map<BookTypeEntity>(request);
            await _bookTypeRepository.InsertAsync(bookType);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(bookType.Id);
        }
    }
}