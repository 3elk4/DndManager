using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcAction.Commands.Delete
{
    public record DeleteActionCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteCombatActionCommandHandler : IRequestHandler<DeleteActionCommand, Result>
    {
        private readonly IDbContext _dbContext;

        public DeleteCombatActionCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcActions.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.NpcActions.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
