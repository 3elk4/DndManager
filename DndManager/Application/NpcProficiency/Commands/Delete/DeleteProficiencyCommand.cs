using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcProficiency.Commands.Delete
{
    public record DeleteProficiencyCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteProficiencyCommandHandler : IRequestHandler<DeleteProficiencyCommand, Result>
    {
        private readonly IDbContext _dbContext;

        public DeleteProficiencyCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteProficiencyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcProficiencies.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.NpcProficiencies.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
