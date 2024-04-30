using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Application.DndClass.Queries.Index
{
    [Authorize(Policy = Policies.OnlyOwnedPc, ProperiesNames = ["Id"])]
    public record GetManyDndClasssByPcIdQuery : IRequest<List<DndClassVM>>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetManyDndClasssByPcIdQueryHandler : IRequestHandler<GetManyDndClasssByPcIdQuery, List<DndClassVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyDndClasssByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<DndClassVM>> Handle(GetManyDndClasssByPcIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext
                    .DndClasses
                    .Where(item => item.PcId.Equals(request.Id))
                    .ProjectToListAsync<DndClassVM>(_mapper.ConfigurationProvider);
        }

    }
}
