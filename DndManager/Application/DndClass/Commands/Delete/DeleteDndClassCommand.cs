using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DndClass.Commands.Delete
{
    public record DeleteDndClassCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteDndClassCommandHandler : IRequestHandler<DeleteDndClassCommand, Result>
    {
        private readonly IRepository<Domain.Entities.DndClass> _repository;

        public DeleteDndClassCommandHandler(IRepository<Domain.Entities.DndClass> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteDndClassCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
