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
        private readonly IDbContext _dbContext;

        public DeleteItemCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Items.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.Items.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
