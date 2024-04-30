using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.Spell.Commands.Delete
{
    [Authorize(Policy = Policies.OnlyOwnedSpell, ProperiesNames = ["Id"])]
    public record DeleteSpellCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteSpellCommandHandler : IRequestHandler<DeleteSpellCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteSpellCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteSpellCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Spells.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.Spells.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
