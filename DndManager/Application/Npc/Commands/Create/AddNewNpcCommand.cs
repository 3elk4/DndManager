using Application.Common.Interfaces;
using Application.Common.Models;
using Application.NpcAbility;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Npc.Commands.Create
{
    public record AddNewNpcCommand : IRequest<Result>, ICommand
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
    }

    public class AddNewNpcCommandHandler : IRequestHandler<AddNewNpcCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewNpcCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result> Handle(AddNewNpcCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.Npc()
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
                SpellInfo = new Domain.Entities.NpcSpellInfo(),
                Abilities = _mapper.Map<List<Domain.Entities.NpcAbility>>(request.Abilities),
            };

            _dbContext.Npcs.Add(item);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ? Result.Success() : Result.Failure(new List<string>() { "Some problems occured during creating record." });
        }
    }
}
