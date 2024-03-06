﻿using Application.Common.Interfaces;

namespace Application.Feat.Command.Delete
{
    public record DeleteFeatCommand : IRequest, ICommand
    {
        public string Id { get; init; }
    }

    public class DeleteFeatCommandHandler : IRequestHandler<DeleteFeatCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteFeatCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteFeatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Feats.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _dbContext.Feats.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
