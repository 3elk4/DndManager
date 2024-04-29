using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Application.NpcFeat.Queries.Index
{
    [Authorize(Policy = Policies.OnlyOwnedNpc, ProperiesNames = ["Id"])]
    public record GetManyFeatsByNpcIdQuery : IRequest<List<NpcFeatVM>>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetManyFeatsByNpcIdQueryHandler : IRequestHandler<GetManyFeatsByNpcIdQuery, List<NpcFeatVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyFeatsByNpcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<NpcFeatVM>> Handle(GetManyFeatsByNpcIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.NpcFeats
                    .Where(feat => feat.NpcId.Equals(request.Id))
                    .ProjectToListAsync<NpcFeatVM>(_mapper.ConfigurationProvider);
        }
    }
}
