using Application.Common.Interfaces;

namespace Application.NpcProficiency.Commands.Delete
{
    public record DeleteProficiencyCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteProficiencyCommandHandler : IRequestHandler<DeleteProficiencyCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteProficiencyCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteProficiencyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcProficiencies.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.NpcProficiencies.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
