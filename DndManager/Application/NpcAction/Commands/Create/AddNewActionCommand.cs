using Application.Common.Interfaces;
using Application.Common.Security;

namespace Application.NpcAction.Commands.Create
{
    [Authorize]
    public record AddNewActionCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public NpcAttackVM Attack { get; set; }
        public NpcDamageVM Damage { get; set; }
        public string NpcId { get; set; }
    }

    public class AddNewCombatActionCommandHandler : IRequestHandler<AddNewActionCommand, string>
    {
        private readonly IDbContext _dbContext;

        public AddNewCombatActionCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(AddNewActionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.NpcAction()
            {
                Name = request.Name,
                Type = request.Type,
                Description = request.Description,
                NpcId = request.NpcId,
                Attack = new Domain.Entities.NpcAttack()
                {
                    ToHit = request.Attack.ToHit,
                    Type = request.Attack.Type,
                    Target = request.Attack.Target,
                    Range = request.Attack.Range
                },
                Damage = new Domain.Entities.NpcDamage()
                {
                    DamageDice = request.Damage.DamageDice,
                    DamageType = request.Damage.DamageType,
                }
            };

            _dbContext.NpcActions.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
