using Application.Common.Interfaces;
using Application.Common.Security;

namespace Application.Spell.Commands.Create
{
    [Authorize]
    public record AddNewSpellCommand : IRequest<string>, ICommand
    {
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
        public string SpellLvlInfoId { get; init; }
    }

    public class AddNewSpellCommandHandler : IRequestHandler<AddNewSpellCommand, string>
    {
        private readonly IDbContext _dbContext;

        public AddNewSpellCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(AddNewSpellCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Spell()
            {
                Name = request.Name,
                Description = request.Description,
                CastingRange = request.CastingRange,
                CastingTime = request.CastingTime,
                Components = request.Components,
                Duration = request.Duration,
                Target = request.Target,
                School = request.School,
                Concentration = request.Concentration,
                Ritual = request.Ritual,
                HigherLvl = request.HigherLvl,
                SpellLvlInfoId = request.SpellLvlInfoId
            };

            _dbContext.Spells.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
