using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IDbContext _dbContext;

        public DeleteDndClassCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteDndClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.DndClasses.FindAsync(new object[] { request.Id }, cancellationToken);

            if(_dbContext.DndClasses.Where(d => d.PcId.Equals(entity.PcId)).Count() <= 1) return Result.Failure(new List<string>() { "Pc should belong to at least one dnd class." });

            _dbContext.DndClasses.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
