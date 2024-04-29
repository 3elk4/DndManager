using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.DndClass.Commands.Delete
{
    [Authorize(Policy = Policies.OnlyOwnedDndClass, ProperiesNames = ["Id"])]
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
