using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Item.Queries.Index
{
    public record GetManyItemsByPcIdQuery : IRequest<Result<List<ItemVM>>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyItemsByPcIdQueryHandler : IRequestHandler<GetManyItemsByPcIdQuery, Result<List<ItemVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyItemsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<ItemVM>>> Handle(GetManyItemsByPcIdQuery request, CancellationToken cancellationToken)
        {
            {
                return Result<List<ItemVM>>.Success(
                    await _dbContext
                    .Items
                    .Where(item => item.PcId.Equals(request.PcId))
                    .ProjectToListAsync<ItemVM>(_mapper.ConfigurationProvider)
                    );
            }

        }
    }
}
