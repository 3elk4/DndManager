using Application.Common.Interfaces;

namespace Application.Feat.Command.Update
{
    public record UpdateFeatCommand : IRequest, ICommand
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Definition { get; set; }
        public string Id { get; set; }
    }


    public class UpdateFeatCommandHandler : IRequestHandler<UpdateFeatCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateFeatCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateFeatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Feats.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Title = request.Title;
            entity.Source = request.Source;
            entity.SourceType = request.SourceType;
            entity.Definition = request.Definition ?? "";

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
