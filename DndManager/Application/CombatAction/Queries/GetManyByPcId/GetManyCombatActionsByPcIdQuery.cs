using Application.CombatAction;
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
        private readonly IRepository<Domain.Entities.Pc> _repository;
        private readonly IMapper _mapper;

        public GetManyCombatActionsByPcIdQueryHandler(IRepository<Domain.Entities.Pc> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CombatActionsWithAbilitiesVM>> Handle(GetManyCombatActionsByPcIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByID(request.PcId, "CombatActions, CombatAction.CombatAttack, CombatAction.CombatDamage, CombatAction.CombatSavingThrow, Abilities");
            if (result != null) return Result<CombatActionsWithAbilitiesVM>.Success(_mapper.Map<CombatActionsWithAbilitiesVM>(result));

            return Result<CombatActionsWithAbilitiesVM>.Failure(null, new List<string>() { $"Can't find records with pc id: {request.PcId}" });

        }

    }
}
