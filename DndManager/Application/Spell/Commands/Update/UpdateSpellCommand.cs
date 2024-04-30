using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;


namespace Application.Spell.Commands.Update
{
    [Authorize(Policy = Policies.OnlyOwnedSpell, ProperiesNames = ["Id"])]
    public record UpdateSpellCommand : IRequest, ICommand
    {
        public string Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CastingTime { get; init; }
        public string CastingRange { get; init; }
        public string Components { get; init; }
        public string Duration { get; init; }
        public string Target { get; set; }
        public bool Concentration { get; set; }
        public bool Ritual { get; set; }
        public string School { get; set; }
        public string HigherLvl { get; set; }
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
            entity.Target = request.Target;
            entity.School = request.School;
            entity.Concentration = request.Concentration;
            entity.Ritual = request.Ritual;
            entity.HigherLvl = request.HigherLvl;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
