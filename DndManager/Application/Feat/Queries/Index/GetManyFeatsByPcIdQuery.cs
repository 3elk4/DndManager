using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Application.Feat.Queries.Index
{
    [Authorize(Policy = Policies.OnlyOwnedPc, ProperiesNames = ["Id"])]
    public record GetManyFeatsByPcIdQuery : IRequest<List<FeatVM>>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetManyFeatsByPcIdQueryHandler : IRequestHandler<GetManyFeatsByPcIdQuery, List<FeatVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyFeatsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<FeatVM>> Handle(GetManyFeatsByPcIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Feats
                    .Where(feat => feat.PcId.Equals(request.Id))
                    .ProjectToListAsync<FeatVM>(_mapper.ConfigurationProvider);
        }
    }
}
