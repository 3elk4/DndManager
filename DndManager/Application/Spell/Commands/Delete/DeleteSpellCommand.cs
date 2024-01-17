using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Spell.Commands.Delete
{
    public record DeleteSpellCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    //public class DeleteSpellCommandHandler : IRequestHandler<DeleteSpellCommand, Result>
    //{
    //    private readonly IRepository<Domain.Entities.Spell> _repository;

    //    public DeleteSpellCommandHandler(IRepository<Domain.Entities.Spell> repository)
    //    {
    //        _repository = repository;
    //    }

    //    public async Task<Result> Handle(DeleteSpellCommand request, CancellationToken cancellationToken)
    //    {
    //        _repository.Delete(request.Id);
    //        var result = await _repository.SaveAsync(cancellationToken);

    //        return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
    //    }
    //}
}
