using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.CombatAction.Commands.Update
{
    [Authorize(Policy = Policies.OnlyOwnedCombatAction, ProperiesNames = ["Id"])]
    public record UpdateCombatActionCommand : IRequest, ICommand
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public string Type { get; set; }
        public CombatAttackVM CombatAttack { get; set; }
        public CombatDamageVM CombatDamage { get; set; }
        public CombatSavingThrowVM CombatSavingThrow { get; set; }
    }

    public class UpdateCombatActionCommandHandler : IRequestHandler<UpdateCombatActionCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCombatActionCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCombatActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.CombatActions.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.Type = request.Type;

            _dbContext.CombatAttacks.Update(_mapper.Map<Domain.Entities.CombatAttack>(request.CombatAttack));
            _dbContext.CombatDamages.Update(_mapper.Map<Domain.Entities.CombatDamage>(request.CombatDamage));
            _dbContext.CombatSavingThrows.Update(_mapper.Map<Domain.Entities.CombatSavingThrow>(request.CombatSavingThrow));

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
