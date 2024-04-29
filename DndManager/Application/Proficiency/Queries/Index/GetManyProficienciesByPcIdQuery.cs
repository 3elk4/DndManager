using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Application.Proficiency.Queries.Index
{
    [Authorize(Policy = Policies.OnlyOwnedPc, ProperiesNames = ["Id"])]
    public record GetManyProficienciesByPcIdQuery : IRequest<List<ProficiencyVM>>, IQuery
    {
        public string Id { get; init; }
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
                    .Where(item => item.PcId.Equals(request.Id))
                    .ProjectToListAsync<ProficiencyVM>(_mapper.ConfigurationProvider);
        }

    }
}
