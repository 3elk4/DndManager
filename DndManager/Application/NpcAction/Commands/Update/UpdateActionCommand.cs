using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.NpcAction.Commands.Update
{
    [Authorize(Policy = Policies.OnlyOwnedNpcAction, ProperiesNames = ["Id"])]
    public record UpdateActionCommand : IRequest, ICommand
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public NpcAttackVM Attack { get; set; }
        public NpcDamageVM Damage { get; set; }
    }

    public class UpdateCombatActionCommandHandler : IRequestHandler<UpdateActionCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCombatActionCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcActions.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.Type = request.Type;
            entity.Description = request.Description;

            _dbContext.NpcAttacks.Update(_mapper.Map<Domain.Entities.NpcAttack>(request.Attack));
            _dbContext.NpcDamages.Update(_mapper.Map<Domain.Entities.NpcDamage>(request.Damage));

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
