using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feat.Command.Delete
{
    public record DeleteFeatCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteFeatCommandHandler : IRequestHandler<DeleteFeatCommand, Result>
    {
        private readonly IRepository<Domain.Entities.Feat> _repository;

        public DeleteFeatCommandHandler(IRepository<Domain.Entities.Feat> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteFeatCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
