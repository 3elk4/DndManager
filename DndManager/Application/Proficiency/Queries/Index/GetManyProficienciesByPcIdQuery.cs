using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Proficiency.Queries.Index
{
    public record GetManyProficienciesByPcIdQuery : IRequest<Result<List<ProficiencyVM>>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyFeatsByPcIdQueryHandler : IRequestHandler<GetManyProficienciesByPcIdQuery, Result<List<ProficiencyVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyFeatsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<ProficiencyVM>>> Handle(GetManyProficienciesByPcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<ProficiencyVM>>.Success(
                await _dbContext
                    .Proficiencies
                    .Where( item => item.PcId.Equals(request.PcId))
                    .ProjectToListAsync<ProficiencyVM>(_mapper.ConfigurationProvider)
                );
        }

    }
}
