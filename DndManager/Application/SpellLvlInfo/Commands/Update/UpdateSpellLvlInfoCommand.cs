using Application.Common.Interfaces;

namespace Application.SpellLvlInfo.Commands.Update
{
    public record UpdateSpellLvlInfoCommand : IRequest, ICommand
    {
        public string Id { get; init; }
        public int Max { get; init; }
        public int Remaining { get; init; }
    }

    public class UpdateSpellLvlInfoCommandHandler : IRequestHandler<UpdateSpellLvlInfoCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateSpellLvlInfoCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateSpellLvlInfoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.SpellLvlInfo.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Max = request.Max;
            entity.Remaining = request.Remaining;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
