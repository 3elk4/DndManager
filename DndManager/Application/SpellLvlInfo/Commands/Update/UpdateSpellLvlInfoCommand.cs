using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Spell;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellLvlInfo.Commands.Update
{
    public record UpdateSpellLvlInfoCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public int Max { get; init; }
        public int Remaining { get; init; }
        public int Lvl { get; init; }

        public IReadOnlyCollection<SpellVM> Spells { get; init; }
    }

    public class UpdateSpellLvlInfoCommandHandler : IRequestHandler<UpdateSpellLvlInfoCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.SpellLvlInfo> _repository;

        public UpdateSpellLvlInfoCommandHandler(IRepository<Domain.Entities.SpellLvlInfo> repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(UpdateSpellLvlInfoCommand request, CancellationToken cancellationToken)
        {
            var spellLvlInfo = new Domain.Entities.SpellLvlInfo()
            {
                Id = request.Id,
                Max = request.Max,
                Remaining = request.Remaining,
                Lvl = request.Lvl,
               // Spells = request.Spells todo: edit spells separatly?
            };

            _repository.Update(spellLvlInfo);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ?
                    Result<int>.Success(result) :
                    Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
