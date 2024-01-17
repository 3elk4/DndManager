using Application.Ability;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ability.Queries.GetManyAbilities
{
    public record GetManyAbilitiesByPcIdQuery : IRequest<Result<List<AbilityVM>>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyAbilitiesByPcIdQueryHandler : IRequestHandler<GetManyAbilitiesByPcIdQuery, Result<List<AbilityVM>>>, IQuery
    {
        private readonly IRepository<Domain.Entities.Ability> _repository;
        private readonly IMapper _mapper;

        public GetManyAbilitiesByPcIdQueryHandler(IRepository<Domain.Entities.Ability> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<AbilityVM>>> Handle(GetManyAbilitiesByPcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<AbilityVM>>.Success(
                await _repository
                    .Get(filter: feat => feat.PcId.Equals(request.PcId), includeProperties: "Skills")
                    .AsNoTracking()
                    .ProjectTo<AbilityVM>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken));
        }
    }
}
