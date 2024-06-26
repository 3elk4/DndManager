﻿using Application.Common.Interfaces;
using Application.Common.Security;
using Application.NpcAbility;
using Application.NpcSpellInfo;
using Domain.Constants;
using System.Collections.Generic;

namespace Application.Npc.Commands.Update
{
    [Authorize(Policy = Policies.OnlyOwnedNpc, ProperiesNames = ["Id"])]
    public record UpdateNpcCommand : IRequest, ICommand
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
        public string Size { get; set; }
        public string Alignment { get; init; }
        public byte[] Image { get; init; }
        public int AC { get; set; }
        public string AcType { get; set; }
        public int HP { get; set; }
        public string HpFormula { get; set; }
        public string Speed { get; set; }
        public int ProficiencyBonus { get; set; }
        public int PassivePerception { get; set; }
        public int Challange { get; init; }
        public int ChallangeXp { get; init; }
        public IList<NpcAbilityVM> Abilities { get; init; }
        public NpcSpellInfoVM SpellInfo { get; init; }
    }

    public class UpdateNpcCommandHandler : IRequestHandler<UpdateNpcCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateNpcCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateNpcCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Npcs.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            if (request.Image != null) entity.Image = request.Image;
            entity.Type = request.Type;
            entity.Size = request.Size;
            entity.Alignment = request.Alignment;
            entity.AC = request.AC;
            entity.AcType = request.AcType;
            entity.HP = request.HP;
            entity.HpFormula = request.HpFormula;
            entity.Speed = request.Speed;
            entity.ProficiencyBonus = request.ProficiencyBonus;
            entity.PassivePerception = request.PassivePerception;
            entity.Challange = request.Challange;
            entity.ChallangeXp = request.ChallangeXp;

            _dbContext.NpcSpellInfo.Update(_mapper.Map<Domain.Entities.NpcSpellInfo>(request.SpellInfo));

            foreach (var ability in _mapper.Map<List<Domain.Entities.NpcAbility>>(request.Abilities))
            {
                _dbContext.NpcAbilities.Update(ability);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
