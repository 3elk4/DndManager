using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CombatAction.Commands.Delete
{
    public record DeleteCombatActionCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteCombatActionCommandHandler : IRequestHandler<DeleteCombatActionCommand, Result>
    {
        private readonly IRepository<Domain.Entities.CombatAction> _repository;

        public DeleteCombatActionCommandHandler(IRepository<Domain.Entities.CombatAction> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteCombatActionCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
