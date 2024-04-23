using Application.Ability;
using Application.Bio;
using Application.Common.Interfaces;
using Application.DndClass;
using Application.SpellInfo;
using System.Collections.Generic;

namespace Application.Pc.Commands.Create
{
    public record AddNewPcCommand : IRequest<string>, ICommand
    {
        public string Name { get; init; }
        public byte[] Image { get; init; }
        public string RaceName { get; init; }
        public string BackgroundName { get; init; }

        public int AC { get; init; }
        public string Speed { get; init; }
        public int HP { get; init; }
        public string HitDice { get; init; }
        public IList<AbilityVM> Abilities { get; init; }
        public IList<DndClassVM> DndClasses { get; init; }
        public BioVM Bio { get; init; }
        public SpellInfoVM SpellInfo { get; init; }
    }

    public class AddNewPcCommandHandler : IRequestHandler<AddNewPcCommand, string>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewPcCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<string> Handle(AddNewPcCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Pc()
            {
                Name = request.Name,
                Image = request.Image,
                RaceName = request.RaceName,
                BackgroundName = request.BackgroundName,
                AC = request.AC,
                Speed = request.Speed,
                HP = request.HP,
                HitDice = request.HitDice,
                Bio = _mapper.Map<Domain.Entities.Bio>(request.Bio),
                SpellInfo = _mapper.Map<Domain.Entities.SpellInfo>(request.SpellInfo),
                Abilities = _mapper.Map<List<Domain.Entities.Ability>>(request.Abilities),
                DndClasses = _mapper.Map<List<Domain.Entities.DndClass>>(request.DndClasses),
                Money = new Domain.Entities.Money()
            };

            _dbContext.Pcs.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
