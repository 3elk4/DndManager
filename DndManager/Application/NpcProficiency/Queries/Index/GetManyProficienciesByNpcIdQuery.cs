using Application.Common.Extentions;
using Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.NpcProficiency.Queries.Index
{
    public record GetManyProficienciesByNpcIdQuery : IRequest<List<NpcProficiencyVM>>, IQuery
    {
        public string NpcId { get; init; }
    }

    public class GetManyFeatsByNpcIdQueryHandler : IRequestHandler<GetManyProficienciesByNpcIdQuery, List<NpcProficiencyVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyFeatsByNpcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<NpcProficiencyVM>> Handle(GetManyProficienciesByNpcIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext
                    .NpcProficiencies
                    .Where(item => item.NpcId.Equals(request.NpcId))
                    .ProjectToListAsync<NpcProficiencyVM>(_mapper.ConfigurationProvider);
        }

    }
}
