using Application.Common.Interfaces;

namespace Application.NpcAction.Commands.Delete
{
    public record DeleteActionCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteCombatActionCommandHandler : IRequestHandler<DeleteActionCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteCombatActionCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcActions.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.NpcActions.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
