using Application.Common.Interfaces;

namespace Application.Item.Commands.Delete
{
    public record DeleteItemCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteItemCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Items.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.Items.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
