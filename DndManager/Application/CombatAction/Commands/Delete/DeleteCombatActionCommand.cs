using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CombatAction.Commands.Delete
{
    public record DeleteCombatActionCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteCombatActionCommandHandler : IRequestHandler<DeleteCombatActionCommand, Result>
    {
        private readonly IDbContext _dbContext;

        public DeleteCombatActionCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteCombatActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.CombatActions.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.CombatActions.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
