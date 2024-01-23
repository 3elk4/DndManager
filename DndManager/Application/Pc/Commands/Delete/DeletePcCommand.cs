using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Commands.Delete
{
    public record DeletePcCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeletePcCommandHandler : IRequestHandler<DeletePcCommand, Result>
    {
        private readonly IDbContext _dbContext;

        public DeletePcCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeletePcCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Pcs.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.Pcs.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
