using Application.Common.Interfaces;

namespace Application.NpcProficiency.Commands.Update
{
    public record UpdateProficiencyCommand : IRequest, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Range { get; set; }
        public string Id { get; set; }
    }


    public class UpdateProficiencyCommandHandler : IRequestHandler<UpdateProficiencyCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateProficiencyCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateProficiencyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcProficiencies.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.Type = request.Type;
            entity.Range = request.Range;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
