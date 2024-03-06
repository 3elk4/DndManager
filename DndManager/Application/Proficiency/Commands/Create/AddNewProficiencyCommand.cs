using Application.Common.Interfaces;

namespace Application.Proficiency.Commands.Create
{
    public record AddNewProficiencyCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewProficiencyCommandHandler : IRequestHandler<AddNewProficiencyCommand, string>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;


        public AddNewProficiencyCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<string> Handle(AddNewProficiencyCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Proficiency()
            {
                Name = request.Name,
                Type = request.Type,
                PcId = request.PcId
            };

            _dbContext.Proficiencies.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
