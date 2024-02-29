using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcFeat.Commands.Delete
{
    public record DeleteFeatCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteFeatCommandHandler : IRequestHandler<DeleteFeatCommand, Result>
    {
        private readonly IDbContext _dbContext;

        public DeleteFeatCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteFeatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcFeats.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.NpcFeats.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
