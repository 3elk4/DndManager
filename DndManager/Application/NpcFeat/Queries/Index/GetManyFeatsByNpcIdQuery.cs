using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcFeat.Queries.Index
{
    public record GetManyFeatsByNpcIdQuery : IRequest<Result<List<NpcFeatVM>>>, IQuery
    {
        public string NpcId { get; init; }
    }

    public class GetManyFeatsByNpcIdQueryHandler : IRequestHandler<GetManyFeatsByNpcIdQuery, Result<List<NpcFeatVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyFeatsByNpcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<NpcFeatVM>>> Handle(GetManyFeatsByNpcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<NpcFeatVM>>.Success(
                await _dbContext.NpcFeats
                    .Where(feat => feat.NpcId.Equals(request.NpcId))
                    .ProjectToListAsync<NpcFeatVM>(_mapper.ConfigurationProvider)
                    );
        }
    }
}
