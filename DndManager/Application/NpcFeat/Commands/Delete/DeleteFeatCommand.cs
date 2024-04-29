using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.NpcFeat.Commands.Delete
{
    [Authorize(Policy = Policies.OnlyOwnedNpcFeat, ProperiesNames = ["Id"])]
    public record DeleteFeatCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteFeatCommandHandler : IRequestHandler<DeleteFeatCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteFeatCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteFeatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcFeats.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.NpcFeats.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
