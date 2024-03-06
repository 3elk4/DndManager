using Application.Common.Interfaces;


namespace Application.Spell.Commands.Update
{
    public record UpdateSpellCommand : IRequest, ICommand
    {
        public string Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CastingTime { get; init; }
        public string CastingRange { get; init; }
        public string Components { get; init; }
        public string Duration { get; init; }
    }

    public class UpdateSpellCommandHandler : IRequestHandler<UpdateSpellCommand>
    {
        private readonly IDbContext _dbContext;
        public UpdateSpellCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateSpellCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Spells.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.CastingRange = request.CastingRange;
            entity.CastingTime = request.CastingTime;
            entity.Components = request.Components;
            entity.Duration = request.Duration;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
