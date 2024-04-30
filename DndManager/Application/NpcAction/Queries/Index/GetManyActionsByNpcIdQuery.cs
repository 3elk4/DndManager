using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Application.NpcAction.Queries.Index
{
    [Authorize(Policy = Policies.OnlyOwnedNpc, ProperiesNames = ["Id"])]
    public record GetManyActionsByNpcIdQuery : IRequest<List<NpcActionVM>>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetManyCombatActionsByPcIdQueryHandler : IRequestHandler<GetManyActionsByNpcIdQuery, List<NpcActionVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyCombatActionsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<NpcActionVM>> Handle(GetManyActionsByNpcIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext
                               .NpcActions
                               .Where(item => item.NpcId.Equals(request.Id))
                               .ProjectToListAsync<NpcActionVM>(_mapper.ConfigurationProvider);
        }
    }
}
