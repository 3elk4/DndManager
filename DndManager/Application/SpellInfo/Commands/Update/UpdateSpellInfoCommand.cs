using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellInfo.Commands.Update
{

    public record UpdateSpellInfoCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public string AbilityId { get; set; }
    }

    public class UpdateSpellInfoCommandHandler : IRequestHandler<UpdateSpellInfoCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.SpellInfo> _repository;

        public UpdateSpellInfoCommandHandler(IRepository<Domain.Entities.SpellInfo> repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(UpdateSpellInfoCommand request, CancellationToken cancellationToken)
        {
            var spellInfo = new Domain.Entities.SpellInfo()
            {
                Id = request.Id,
                AbilityId = request.AbilityId
            };

            _repository.Update(spellInfo);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ?
                    Result<int>.Success(result) :
                    Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
