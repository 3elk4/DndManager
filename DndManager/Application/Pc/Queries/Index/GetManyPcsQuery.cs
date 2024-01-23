using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Queries.Index
{
    public record GetManyPcsQuery : IRequest<Result<List<PcBriefVM>>>, IQuery
    {
    }

    public class GetManyPcsQueryHandler : IRequestHandler<GetManyPcsQuery, Result<List<PcBriefVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyPcsQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<PcBriefVM>>> Handle(GetManyPcsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Pcs.ProjectToListAsync<PcBriefVM>(_mapper.ConfigurationProvider);
            return Result<List<PcBriefVM>>.Success(result);
        }

    }
}
