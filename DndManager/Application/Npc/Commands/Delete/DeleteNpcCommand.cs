using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Npc.Commands.Delete
{
    public record DeleteNpcCommand : IRequest<Result>, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteNpcCommandHandler : IRequestHandler<DeleteNpcCommand, Result>
    {
        private readonly IDbContext _dbContext;

        public DeleteNpcCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteNpcCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Npcs.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.Npcs.Remove(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some errors occured during deleting record" });
        }
    }
}
