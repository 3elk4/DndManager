using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.CombatAction.Queries.GetManyByPcId
{
    [Authorize(Policy = Policies.OnlyOwnedPc, ProperiesNames = ["Id"])]
    public record GetManyCombatActionsByPcIdQuery : IRequest<CombatActionsWithAbilitiesVM>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetManyCombatActionsByPcIdQueryHandler : IRequestHandler<GetManyCombatActionsByPcIdQuery, CombatActionsWithAbilitiesVM>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyCombatActionsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CombatActionsWithAbilitiesVM> Handle(GetManyCombatActionsByPcIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Pcs.ProjectToSingle<Domain.Entities.Pc, CombatActionsWithAbilitiesVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);

            return result;

        }

    }
}
