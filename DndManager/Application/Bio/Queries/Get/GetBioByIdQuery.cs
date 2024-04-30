using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.Bio.Queries.Get
{
    [Authorize(Policy = Policies.OnlyOwnedBio, ProperiesNames = ["Id"])]
    public record GetBioByIdQuery : IRequest<BioVM>
    {
        public string Id { get; init; }
    }

    public class GetBioByIdQueryHandler : IRequestHandler<GetBioByIdQuery, BioVM>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBioByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BioVM> Handle(GetBioByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Bio.ProjectToSingle<Domain.Entities.Bio, BioVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);

            Guard.Against.NotFound(request.Id, result);

            return result;
        }
    }
}