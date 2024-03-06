using Application.Common.Interfaces;

namespace Application.CombatAction.Commands.Delete
{
    public record DeleteCombatActionCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteCombatActionCommandHandler : IRequestHandler<DeleteCombatActionCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteCombatActionCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteCombatActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.CombatActions.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.CombatActions.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
