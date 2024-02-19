using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcAction.Commands.Create
{
    public record AddNewActionCommand : IRequest<Result<NpcActionVM>>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public NpcAttackVM Attack { get; set; }
        public NpcDamageVM Damage { get; set; }
        public string NpcId { get; set; }
    }

    public class AddNewCombatActionCommandHandler : IRequestHandler<AddNewActionCommand, Result<NpcActionVM>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewCombatActionCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<NpcActionVM>> Handle(AddNewActionCommand request, CancellationToken cancellationToken)
        {
            var combatAction = new Domain.Entities.NpcAction()
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

            _dbContext.NpcActions.Add(combatAction);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                Result<NpcActionVM>.Success(_mapper.Map<NpcActionVM>(combatAction)) :
                Result<NpcActionVM>.Failure(null, new List<string>() { "Some problems occured during creating record." });
        }
    }
}
