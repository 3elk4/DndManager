using Application.Common.Extentions;
using Application.Common.Interfaces;

namespace Application.Npc.Queries.Show
{
    public record GetNpcByIdQuery : IRequest<NpcDetailsVM>, IQuery
    {
        public string Id { get; set; }
    }

    public class GetNpcByIdQueryHandler : IRequestHandler<GetNpcByIdQuery, NpcDetailsVM>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNpcByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NpcDetailsVM> Handle(GetNpcByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Npcs.ProjectToSingle<Domain.Entities.Npc, NpcDetailsVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);

            Guard.Against.NotFound(request.Id, result);

            return result;
        }

    }
}
