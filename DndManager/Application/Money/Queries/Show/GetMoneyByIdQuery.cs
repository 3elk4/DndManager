using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.Money.Queries.Show
{
    [Authorize(Policy = Policies.OnlyOwnedMoney, ProperiesNames = ["Id"])]
    public record GetMoneyByIdQuery : IRequest<MoneyVM>
    {
        public string Id { get; init; }
    }

    public class GetMoneyByIdQueryHandler : IRequestHandler<GetMoneyByIdQuery, MoneyVM>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMoneyByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MoneyVM> Handle(GetMoneyByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Money.ProjectToSingle<Domain.Entities.Money, MoneyVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);

            Guard.Against.NotFound(request.Id, result);

            return result;
        }
    }
}
