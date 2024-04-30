using Application.Common.Interfaces;
using Application.Common.Security;

namespace Application.NpcFeat.Commands.Create
{
    [Authorize]
    public record AddNewFeatCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string TimeRegeneration { get; set; }
        public string Description { get; set; }
        public string NpcId { get; set; }
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
            var entity = new Domain.Entities.NpcFeat()
            {
                Name = request.Name,
                TimeRegeneration = request.TimeRegeneration,
                Description = request.Description,
                NpcId = request.NpcId
            };

            _dbContext.NpcFeats.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
