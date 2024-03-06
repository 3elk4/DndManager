using Application.Common.Interfaces;

namespace Application.Pc.Commands.Delete
{
    public record DeletePcCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeletePcCommandHandler : IRequestHandler<DeletePcCommand>
    {
        private readonly IDbContext _dbContext;

        public DeletePcCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeletePcCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Pcs.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.Pcs.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
