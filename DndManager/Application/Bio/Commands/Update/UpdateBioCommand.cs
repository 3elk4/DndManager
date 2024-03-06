using Application.Common.Interfaces;

namespace Application.Bio.Commands.Update
{
    public record UpdateBioCommand() : IRequest, ICommand
    {
        public string Id { get; init; }
        public int Age { get; init; }
        public string Size { get; init; }
        public string Weight { get; init; }
        public string Height { get; init; }
        public string Skin { get; init; }
        public string Eyes { get; init; }
        public string Hair { get; init; }
        public string Alignment { get; init; }
        public string Traits { get; init; }
        public string Flaws { get; init; }
        public string Bonds { get; init; }
        public string Ideals { get; init; }
        public string Allies { get; init; }
        public string Backstory { get; init; }
    }

    public class UpdateBioCommandHandler : IRequestHandler<UpdateBioCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateBioCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateBioCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Bio.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Age = request.Age;
            entity.Size = request.Size;
            entity.Weight = request.Weight;
            entity.Height = request.Height;
            entity.Skin = request.Skin;
            entity.Eyes = request.Eyes;
            entity.Hair = request.Hair;
            entity.Alignment = request.Alignment;
            entity.Traits = request.Traits;
            entity.Flaws = request.Flaws;
            entity.Bonds = request.Bonds;
            entity.Ideals = request.Ideals;
            entity.Allies = request.Allies;
            entity.Backstory = request.Backstory;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
