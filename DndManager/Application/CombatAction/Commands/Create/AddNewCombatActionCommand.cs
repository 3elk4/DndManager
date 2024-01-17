using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CombatAction.Command.Create
{
    public record AddNewCombatActionCommand : IRequest<Result<CombatActionVM>>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public CombatAttackVM CombatAttack { get; set; }
        public CombatDamageVM CombatDamage { get; set; }
        public CombatSavingThrowVM CombatSavingThrow { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewCombatActionCommandHandler : IRequestHandler<AddNewCombatActionCommand, Result<CombatActionVM>>
    {
        private readonly IRepository<Domain.Entities.CombatAction> _repository;
        private readonly IMapper _mapper;

        public AddNewCombatActionCommandHandler(IRepository<Domain.Entities.CombatAction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CombatActionVM>> Handle(AddNewCombatActionCommand request, CancellationToken cancellationToken)
        {
            var combatAction = new Domain.Entities.CombatAction()
            {
                Name = request.Name,
                Type = request.Type,
                PcId = request.PcId,
                CombatAttack = new Domain.Entities.CombatAttack() { 
                    IsProficient = request.CombatAttack.IsProficient,
                    Range = request.CombatAttack.Range,
                    AdditionalBonus = request.CombatAttack.AdditionalBonus,
                    AbilityId = request.CombatAttack.AbilityId
                },
                CombatDamage = new Domain.Entities.CombatDamage() {
                    AbilityId = request.CombatDamage.AbilityId,
                    DamageDice = request.CombatDamage.DamageDice,
                    DamageType = request.CombatDamage.DamageType,
                    AdditionalBonus = request.CombatDamage.AdditionalBonus
                },
                CombatSavingThrow = new Domain.Entities.CombatSavingThrow() { 
                    AbilityId = request.CombatSavingThrow.AbilityId
                }
            };

            _repository.Insert(combatAction);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ?
                Result<CombatActionVM>.Success(_mapper.Map<CombatActionVM>(combatAction)) :
                Result<CombatActionVM>.Failure(null, new List<string>() { "Some problems occured during creating record." });
        }
    }
}
