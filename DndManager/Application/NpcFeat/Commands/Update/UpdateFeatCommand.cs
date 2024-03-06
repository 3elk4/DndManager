using Application.Common.Interfaces;

namespace Application.NpcFeat.Commands.Update
{
    public record UpdateFeatCommand : IRequest, ICommand
    {
        public string Name { get; set; }
        public string TimeRegeneration { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }


    public class UpdateFeatCommandHandler : IRequestHandler<UpdateFeatCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateFeatCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateFeatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcFeats.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.TimeRegeneration = request.TimeRegeneration;
            entity.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
