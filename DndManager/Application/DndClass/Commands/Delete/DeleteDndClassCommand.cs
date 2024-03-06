using Application.Common.Interfaces;

namespace Application.DndClass.Commands.Delete
{
    public record DeleteDndClassCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteDndClassCommandHandler : IRequestHandler<DeleteDndClassCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteDndClassCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteDndClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.DndClasses.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.DndClasses.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
