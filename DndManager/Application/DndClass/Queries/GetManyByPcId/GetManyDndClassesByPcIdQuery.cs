using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        private readonly IRepository<Domain.Entities.DndClass> _repository;
        private readonly IMapper _mapper;

        public GetManyDndClasssByPcIdQueryHandler(IRepository<Domain.Entities.DndClass> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<DndClassVM>>> Handle(GetManyDndClasssByPcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<DndClassVM>>.Success(
                await _repository
                    .Get(filter: item => item.PcId.Equals(request.PcId))
                    .AsNoTracking()
                    .ProjectTo<DndClassVM>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken));
        }

    }
}
