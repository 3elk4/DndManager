using Application.Common.Interfaces;
using Application.Common.Security;

namespace Application.CombatAction.Command.Create
{
    [Authorize]
    public record AddNewCombatActionCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public CombatAttackVM CombatAttack { get; set; }
        public CombatDamageVM CombatDamage { get; set; }
        public CombatSavingThrowVM CombatSavingThrow { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewCombatActionCommandHandler : IRequestHandler<AddNewCombatActionCommand, string>
    {
        private readonly IDbContext _dbContext;

        public AddNewCombatActionCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(AddNewCombatActionCommand request, CancellationToken cancellationToken)
        {
            var combatAction = new Domain.Entities.CombatAction()
            {
                Name = request.Name,
                Type = request.Type,
                PcId = request.PcId,
                CombatAttack = new Domain.Entities.CombatAttack()
                {
                    IsProficient = request.CombatAttack.IsProficient,
                    Range = request.CombatAttack.Range,
                    AdditionalBonus = request.CombatAttack.AdditionalBonus,
                    AbilityId = request.CombatAttack.AbilityId
                },
                CombatDamage = new Domain.Entities.CombatDamage()
                {
                    AbilityId = request.CombatDamage.AbilityId,
                    DamageDice = request.CombatDamage.DamageDice,
                    DamageType = request.CombatDamage.DamageType,
                    AdditionalBonus = request.CombatDamage.AdditionalBonus
                },
                CombatSavingThrow = new Domain.Entities.CombatSavingThrow()
                {
                    AbilityId = request.CombatSavingThrow.AbilityId
                }
            };

            _dbContext.CombatActions.Add(combatAction);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return combatAction.Id;
        }
    }
}
