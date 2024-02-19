using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcProficiency.Queries.Index
{
    public record GetManyProficienciesByNpcIdQuery : IRequest<Result<List<NpcProficiencyVM>>>, IQuery
    {
        public string NpcId { get; init; }
    }

    public class GetManyFeatsByNpcIdQueryHandler : IRequestHandler<GetManyProficienciesByNpcIdQuery, Result<List<NpcProficiencyVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyFeatsByNpcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<NpcProficiencyVM>>> Handle(GetManyProficienciesByNpcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<NpcProficiencyVM>>.Success(
                await _dbContext
                    .NpcProficiencies
                    .Where(item => item.NpcId.Equals(request.NpcId))
                    .ProjectToListAsync<NpcProficiencyVM>(_mapper.ConfigurationProvider)
                );
        }

    }
}
