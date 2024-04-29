using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.Proficiency.Commands.Update
{
    [Authorize(Policy = Policies.OnlyOwnedProficiency, ProperiesNames = ["Id"])]
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
