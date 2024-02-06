using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feat.Queries.Index
{
    public record GetManyFeatsByPcIdQuery : IRequest<Result<List<FeatVM>>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyFeatsByPcIdQueryHandler : IRequestHandler<GetManyFeatsByPcIdQuery, Result<List<FeatVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyFeatsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<FeatVM>>> Handle(GetManyFeatsByPcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<FeatVM>>.Success(
                await _dbContext.Feats
                    .Where(feat => feat.PcId.Equals(request.PcId))
                    .ProjectToListAsync<FeatVM>(_mapper.ConfigurationProvider)
                    );
        }
    }
}
