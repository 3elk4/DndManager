using Application.Common.Extentions;
using Application.Common.Interfaces;

namespace Application.SpellLvlInfo.Queries.Get
{
    public record GetSpellLvlInfoQuery : IRequest<SpellLvlInfoVM>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetSpellLvlInfoQueryHandler : IRequestHandler<GetSpellLvlInfoQuery, SpellLvlInfoVM>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSpellLvlInfoQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SpellLvlInfoVM> Handle(GetSpellLvlInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.SpellLvlInfo.ProjectToSingle<Domain.Entities.SpellLvlInfo, SpellLvlInfoVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);

            Guard.Against.NotFound(request.Id, result);

            return result;
        }
    }
}
