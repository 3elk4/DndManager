using Application.Common.Extentions;
using Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Proficiency.Queries.Index
{
    public record GetManyProficienciesByPcIdQuery : IRequest<List<ProficiencyVM>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyFeatsByPcIdQueryHandler : IRequestHandler<GetManyProficienciesByPcIdQuery, List<ProficiencyVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyFeatsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ProficiencyVM>> Handle(GetManyProficienciesByPcIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext
                    .Proficiencies
                    .Where(item => item.PcId.Equals(request.PcId))
                    .ProjectToListAsync<ProficiencyVM>(_mapper.ConfigurationProvider);
        }

    }
}
