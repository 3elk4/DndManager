using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.Pc.Queries.Show
{
    [Authorize(Policy = Policies.OnlyOwnedPc, ProperiesNames = ["Id"])]
    public record GetPcByIdQuery : IRequest<PcDetailsVM>, IQuery
    {
        public string Id { get; set; }
    }

    public class GetPcByIdQueryHandler : IRequestHandler<GetPcByIdQuery, PcDetailsVM>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPcByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PcDetailsVM> Handle(GetPcByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Pcs.ProjectToSingle<Domain.Entities.Pc, PcDetailsVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider); ;

            Guard.Against.NotFound(request.Id, result);

            return result;
        }
    }
}
