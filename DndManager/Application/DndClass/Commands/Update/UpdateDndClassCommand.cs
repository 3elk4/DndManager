using Application.Common.Interfaces;

namespace Application.DndClass.Commands.Update
{
    public record UpdateDndClassCommand : IRequest, ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string SubclassName { get; set; }
    }

    public class UpdateDndClassCommandHandler : IRequestHandler<UpdateDndClassCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateDndClassCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateDndClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.DndClasses.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.SubclassName = request.SubclassName ?? "";
            entity.Lvl = request.Lvl;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
