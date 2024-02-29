using Application.CombatAction;
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

namespace Application.NpcAction.Queries.Index
{
    public record GetManyActionsByNpcIdQuery : IRequest<Result<List<NpcActionVM>>>, IQuery
    {
        public string NpcId { get; init; }
    }

    public class GetManyCombatActionsByPcIdQueryHandler : IRequestHandler<GetManyActionsByNpcIdQuery, Result<List<NpcActionVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyCombatActionsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<NpcActionVM>>> Handle(GetManyActionsByNpcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<NpcActionVM>>.Success(
                           await _dbContext
                               .NpcActions
                               .Where(item => item.NpcId.Equals(request.NpcId))
                               .ProjectToListAsync<NpcActionVM>(_mapper.ConfigurationProvider)
                           );
        }

    }
}
