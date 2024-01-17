using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Commands.Delete
{
    public record DeletePcCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeletePcCommandHandler : IRequestHandler<DeletePcCommand, Result>
    {
        private readonly IRepository<Domain.Entities.Pc> _repository;

        public DeletePcCommandHandler(IRepository<Domain.Entities.Pc> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeletePcCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
