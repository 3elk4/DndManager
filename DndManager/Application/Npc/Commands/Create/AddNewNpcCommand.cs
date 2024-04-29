using Application.Common.Interfaces;
using Application.Common.Security;
using Application.NpcAbility;
using Application.NpcSpellInfo;
using System.Collections.Generic;

namespace Application.Npc.Commands.Create
{
    [Authorize]
    public record AddNewNpcCommand : IRequest<string>, ICommand
    {
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

    public class AddNewNpcCommandHandler : IRequestHandler<AddNewNpcCommand, string>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewNpcCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<string> Handle(AddNewNpcCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Npc()
            {
                Name = request.Name,
                Image = request.Image,
                Type = request.Type,
                Size = request.Size,
                Alignment = request.Alignment,
                AC = request.AC,
                AcType = request.AcType,
                HP = request.HP,
                HpFormula = request.HpFormula,
                Speed = request.Speed,
                ProficiencyBonus = request.ProficiencyBonus,
                PassivePerception = request.PassivePerception,
                Challange = request.Challange,
                ChallangeXp = request.ChallangeXp,
                Abilities = _mapper.Map<List<Domain.Entities.NpcAbility>>(request.Abilities),
                SpellInfo = _mapper.Map<Domain.Entities.NpcSpellInfo>(request.SpellInfo)
            };

            _dbContext.Npcs.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
