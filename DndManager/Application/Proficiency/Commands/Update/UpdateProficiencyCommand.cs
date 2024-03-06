using Application.Common.Interfaces;

namespace Application.Proficiency.Commands.Update
{
    public record UpdateProficiencyCommand : IRequest, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
    }


    public class UpdateProficiencyCommandHandler : IRequestHandler<UpdateProficiencyCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateProficiencyCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProficiencyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Proficiencies.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.Type = request.Type;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
