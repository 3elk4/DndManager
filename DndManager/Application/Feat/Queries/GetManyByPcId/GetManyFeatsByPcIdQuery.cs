using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feat.Queries.GetManyByPcId
{
    public record GetManyFeatsByPcIdQuery : IRequest<Result<List<FeatVM>>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyFeatsByPcIdQueryHandler : IRequestHandler<GetManyFeatsByPcIdQuery, Result<List<FeatVM>>>, IQuery
    {
        private readonly IRepository<Domain.Entities.Feat> _repository;
        private readonly IMapper _mapper;

        public GetManyFeatsByPcIdQueryHandler(IRepository<Domain.Entities.Feat> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<FeatVM>>> Handle(GetManyFeatsByPcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<FeatVM>>.Success(
                await _repository
                    .Get(filter: feat => feat.PcId.Equals(request.PcId))
                    .AsNoTracking()
                    .ProjectTo<FeatVM>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken));
        }
    }
}
