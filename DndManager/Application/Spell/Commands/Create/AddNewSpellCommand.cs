using Application.Common.Interfaces;

namespace Application.Spell.Commands.Create
{
    public record AddNewSpellCommand : IRequest<string>, ICommand
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string CastingTime { get; init; }
        public string CastingRange { get; init; }
        public string Components { get; init; }
        public string Duration { get; init; }
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
                SpellLvlInfoId = request.SpellLvlInfoId
            };

            _dbContext.Spells.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
