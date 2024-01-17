using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CombatAction.Queries.Get
{
    public record GetCombatActionByIdQuery : IRequest<Result<CombatActionVM>>
    {
        public string Id { get; init; }
    }

    public class GetCombatActionByIdQueryHandler : IRequestHandler<GetCombatActionByIdQuery, Result<CombatActionVM>>, IQuery
    {
        private readonly IRepository<Domain.Entities.CombatAction> _repository;
        private readonly IMapper _mapper;

        public GetCombatActionByIdQueryHandler(IRepository<Domain.Entities.CombatAction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CombatActionVM>> Handle(GetCombatActionByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByID(request.Id, includeProperties: "CombatAttack, CombatSavingThrow, CombatDamage");

            if (result != null) return Result<CombatActionVM>.Success(_mapper.Map<CombatActionVM>(result));

            return Result<CombatActionVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
        }
    }
}
