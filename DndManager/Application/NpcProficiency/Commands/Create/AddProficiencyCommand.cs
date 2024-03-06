using Application.Common.Interfaces;

namespace Application.NpcProficiency.Commands.Create
{
    public record AddNewProficiencyCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Range { get; set; }
        public string NpcId { get; set; }
    }

    public class AddNewProficiencyCommandHandler : IRequestHandler<AddNewProficiencyCommand, string>
    {
        private readonly IDbContext _dbContext;


        public AddNewProficiencyCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(AddNewProficiencyCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.NpcProficiency()
            {
                Name = request.Name,
                Type = request.Type,
                Range = request.Range,
                NpcId = request.NpcId
            };

            _dbContext.NpcProficiencies.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
