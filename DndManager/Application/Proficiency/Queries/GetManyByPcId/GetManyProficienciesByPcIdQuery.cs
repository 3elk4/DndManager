using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Proficiency.Queries.GetManyByPcId
{
    public record GetManyProficienciesByPcIdQuery : IRequest<Result<List<ProficiencyVM>>>, IQuery
    {
        public string PcId { get; init; }
    }

    public class GetManyFeatsByPcIdQueryHandler : IRequestHandler<GetManyProficienciesByPcIdQuery, Result<List<ProficiencyVM>>>, IQuery
    {
        private readonly IRepository<Domain.Entities.Proficiency> _repository;
        private readonly IMapper _mapper;

        public GetManyFeatsByPcIdQueryHandler(IRepository<Domain.Entities.Proficiency> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<ProficiencyVM>>> Handle(GetManyProficienciesByPcIdQuery request, CancellationToken cancellationToken)
        {
            return Result<List<ProficiencyVM>>.Success(
                await _repository
                    .Get(filter: item => item.PcId.Equals(request.PcId))
                    .AsNoTracking()
                    .ProjectTo<ProficiencyVM>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken));
        }

    }
}
