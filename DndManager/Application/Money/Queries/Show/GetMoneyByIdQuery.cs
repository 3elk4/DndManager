using Application.Common.Extentions;
using Application.Common.Interfaces;

namespace Application.Money.Queries.Show
{
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
