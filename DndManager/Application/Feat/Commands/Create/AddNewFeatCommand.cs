using Application.Common.Interfaces;

namespace Application.Feat.Command.Create
{
    public record AddNewFeatCommand : IRequest<string>, ICommand
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Definition { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewFeatCommandHandler : IRequestHandler<AddNewFeatCommand, string>
    {
        private readonly IDbContext _dbContext;

        public AddNewFeatCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(AddNewFeatCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Feat()
            {
                Title = request.Title,
                Source = request.Source,
                SourceType = request.SourceType,
                Definition = request.Definition ?? "",
                PcId = request.PcId
            };

            _dbContext.Feats.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
