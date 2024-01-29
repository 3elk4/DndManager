using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DndClass.Queries.GetManyByPcId
{
    public record GetManyDndClasssByPcIdQuery : IRequest<Result<List<DndClassVM>>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyDndClasssByPcIdQueryHandler : IRequestHandler<GetManyDndClasssByPcIdQuery, Result<List<DndClassVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyDndClasssByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<DndClassVM>>> Handle(GetManyDndClasssByPcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<DndClassVM>>.Success(
                await _dbContext
                    .DndClasses
                    .Where(item => item.PcId.Equals(request.PcId))
                    .ProjectToListAsync<DndClassVM>(_mapper.ConfigurationProvider));
        }

    }
}
