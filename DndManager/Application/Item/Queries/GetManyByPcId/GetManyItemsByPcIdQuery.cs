using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Item.Queries.GetManyByPcId
{
    public record GetManyItemsByPcIdQuery : IRequest<Result<List<ItemVM>>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyItemsByPcIdQueryHandler : IRequestHandler<GetManyItemsByPcIdQuery, Result<List<ItemVM>>>, IQuery
    {
        private readonly IRepository<Domain.Entities.Item> _repository;
        private readonly IMapper _mapper;

        public GetManyItemsByPcIdQueryHandler(IRepository<Domain.Entities.Item> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<ItemVM>>> Handle(GetManyItemsByPcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<ItemVM>>.Success(
                await _repository
                    .Get(filter: item => item.PcId.Equals(request.PcId))
                    .AsNoTracking()
                    .ProjectTo<ItemVM>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken));
        }

    }
}
