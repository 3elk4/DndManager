using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bio.Commands.Update
{
    public record UpdateBioCommand() : IRequest<Result<int>>, ICommand
    {
        public BioVM Bio { get; init; }
    }

    public class UpdateBioCommandHandler : IRequestHandler<UpdateBioCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateBioCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateBioCommand request, CancellationToken cancellationToken)
        {
            var bio = _mapper.Map<Domain.Entities.Bio>(request.Bio);

            _dbContext.Bio.Update(bio);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ?
                    Result<int>.Success(result) :
                    Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
