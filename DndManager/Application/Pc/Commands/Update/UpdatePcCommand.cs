using Application.Ability;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Commands.Update
{
    public record UpdatePcCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public byte[] Image { get; init; }
        public string RaceName { get; init; }
        public string BackgroundName { get; init; }

        public bool Inspiration { get; init; }
        public int AC { get; init; }
        public string Speed { get; init; }
        public int HP { get; init; }
        public int CurrentHP { get; init; }
        public int TempHP { get; init; }
        public string HitDice { get; init; }

        public IList<AbilityVM> Abilities { get; init; }
    }

    public class UpdatePcCommandHandler : IRequestHandler<UpdatePcCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdatePcCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdatePcCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Pcs.FindAsync(new object[] { request.Id }, cancellationToken);

            if(entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find a character with id {request.Id}." });

            entity.Name = request.Name;
            if(request.Image != null) entity.Image = request.Image;
            entity.RaceName = request.RaceName;
            entity.BackgroundName = request.BackgroundName;
            entity.Inspiration = request.Inspiration;
            entity.AC = request.AC;
            entity.Speed = request.Speed;
            entity.HP = request.HP;
            entity.CurrentHP = request.CurrentHP;
            entity.TempHP = request.TempHP;
            entity.HitDice = request.HitDice;

            foreach(var ability in _mapper.Map<List<Domain.Entities.Ability>>(request.Abilities))
            {
                _dbContext.Abilities.Update(ability);
            }

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
