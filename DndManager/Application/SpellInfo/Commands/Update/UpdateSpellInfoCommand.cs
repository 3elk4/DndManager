using Application.Common.Interfaces;

namespace Application.SpellInfo.Commands.Update
{

    public record UpdateSpellInfoCommand : IRequest, ICommand
    {
        public string Id { get; init; }
        public string AbilityId { get; set; }
    }

    public class UpdateSpellInfoCommandHandler : IRequestHandler<UpdateSpellInfoCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateSpellInfoCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateSpellInfoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.SpellInfo.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.AbilityId = request.AbilityId;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
