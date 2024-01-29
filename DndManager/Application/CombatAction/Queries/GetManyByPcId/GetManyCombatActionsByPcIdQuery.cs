using Application.CombatAction;
using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CombatAction.Queries.GetManyByPcId
{
    public record GetManyCombatActionsByPcIdQuery : IRequest<Result<CombatActionsWithAbilitiesVM>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyCombatActionsByPcIdQueryHandler : IRequestHandler<GetManyCombatActionsByPcIdQuery, Result<CombatActionsWithAbilitiesVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyCombatActionsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<CombatActionsWithAbilitiesVM>> Handle(GetManyCombatActionsByPcIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Pcs.ProjectToSingle<Domain.Entities.Pc, CombatActionsWithAbilitiesVM>(x => x.Id.Equals(request.PcId), _mapper.ConfigurationProvider);
            if (result != null) return Result<CombatActionsWithAbilitiesVM>.Success(result);

            return Result<CombatActionsWithAbilitiesVM>.Failure(null, new List<string>() { $"Can't find records with pc id: {request.PcId}" });

        }

    }
}
