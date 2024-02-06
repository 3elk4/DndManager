using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Spell.Commands.Delete
{
    public record DeleteSpellCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteSpellCommandHandler : IRequestHandler<DeleteSpellCommand, Result>
    {
        private readonly IDbContext _dbContext;

        public DeleteSpellCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteSpellCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Spells.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.Spells.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
