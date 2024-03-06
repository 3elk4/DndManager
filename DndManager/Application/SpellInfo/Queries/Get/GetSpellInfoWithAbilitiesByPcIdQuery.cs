using Application.Common.Extentions;
using Application.Common.Interfaces;

namespace Application.SpellInfo.Queries.Get
{
    public record GetSpellInfoWithAbilitiesByPcIdQuery : IRequest<SpellInfoAndAbilitiesVM>, IQuery
    {
        public string Id { get; init; }
    }
    public class GetSpellInfoWithAbilitiesByPcIdQueryHandler : IRequestHandler<GetSpellInfoWithAbilitiesByPcIdQuery, SpellInfoAndAbilitiesVM>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSpellInfoWithAbilitiesByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SpellInfoAndAbilitiesVM> Handle(GetSpellInfoWithAbilitiesByPcIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.SpellInfo.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            return await _dbContext.Pcs.ProjectToSingle<Domain.Entities.Pc, SpellInfoAndAbilitiesVM>(x => x.Id.Equals(entity.PcId), _mapper.ConfigurationProvider);
        }
    }

}
