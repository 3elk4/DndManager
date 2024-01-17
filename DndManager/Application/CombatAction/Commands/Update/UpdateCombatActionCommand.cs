
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CombatAction.Commands.Update
{
    public record UpdateCombatActionCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public string Type { get; set; }
        public CombatAttackVM CombatAttack { get; set; }
        public CombatDamageVM CombatDamage { get; set; }
        public CombatSavingThrowVM CombatSavingThrow { get; set; }
    }

    public class UpdateCombatActionCommandHandler : IRequestHandler<UpdateCombatActionCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.CombatAction> _repository;

        public UpdateCombatActionCommandHandler(IRepository<Domain.Entities.CombatAction> repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(UpdateCombatActionCommand request, CancellationToken cancellationToken)
        {
            var combatAction = new Domain.Entities.CombatAction()
            {
                Id = request.Id,
                Type = request.Type,
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

            _repository.Update(combatAction);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
