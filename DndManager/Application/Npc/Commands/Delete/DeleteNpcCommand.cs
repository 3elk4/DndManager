using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.Npc.Commands.Delete
{
    [Authorize(Policy = Policies.OnlyOwnedNpc, ProperiesNames = ["Id"])]
    public record DeleteNpcCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteNpcCommandHandler : IRequestHandler<DeleteNpcCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteNpcCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteNpcCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Npcs.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.Npcs.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
