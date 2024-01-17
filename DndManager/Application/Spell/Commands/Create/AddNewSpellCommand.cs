using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Spell.Commands.Create
{
    public record AddNewSpellCommand : IRequest<Result>, ICommand
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string CastingTime { get; init; }
        public string CastingRange { get; init; }
        public string Components { get; init; }
        public string Duration { get; init; }
        public string SpellLvlInfoId { get; init; }
    }

    public class AddNewSpellCommandHandler : IRequestHandler<AddNewSpellCommand, Result>
    {
        private readonly IRepository<Domain.Entities.Spell> _repository;

        public AddNewSpellCommandHandler(IRepository<Domain.Entities.Spell> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(AddNewSpellCommand request, CancellationToken cancellationToken)
        {
            var spell = new Domain.Entities.Spell()
            {
                Name = request.Name,
                Description = request.Description,
                CastingRange = request.CastingRange,
                CastingTime = request.CastingTime,
                Components = request.Components,
                Duration = request.Duration,
                SpellLvlInfoId = request.SpellLvlInfoId
            };

            _repository.Insert(spell);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some problems occured during creating record." });
        }
    }
}
