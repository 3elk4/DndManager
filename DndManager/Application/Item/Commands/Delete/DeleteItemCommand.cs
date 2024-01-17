using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Item.Commands.Delete
{
    public record DeleteItemCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Result>
    {
        private readonly IRepository<Domain.Entities.Item> _repository;

        public DeleteItemCommandHandler(IRepository<Domain.Entities.Item> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
