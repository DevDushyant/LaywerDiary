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

namespace CourtApp.Application.Features.BookMasters.Commands
{
   public class CreateBookMasterCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string PublisherName { get; set; }
        public int BookTypeId { get; set; }        
        public int PublisherId { get; set; }
        
    }
    public class CreateBookMasterCommandHandler : IRequestHandler<CreateBookMasterCommand, Result<int>>
    {
        private readonly IBookMasterRepository _Repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateBookMasterCommandHandler(IBookMasterRepository _Repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateBookMasterCommand request, CancellationToken cancellationToken)
        {
            var bookType = _mapper.Map<LDBookEntity>(request);
            await _Repository.InsertAsync(bookType);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(bookType.Id);
        }
    }
}
