using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Proficiency.Commands.Delete
{
    public record DeleteProficiencyCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteProficiencyCommandHandler : IRequestHandler<DeleteProficiencyCommand, Result>
    {
        private readonly IRepository<Domain.Entities.Proficiency> _repository;

        public DeleteProficiencyCommandHandler(IRepository<Domain.Entities.Proficiency> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteProficiencyCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
