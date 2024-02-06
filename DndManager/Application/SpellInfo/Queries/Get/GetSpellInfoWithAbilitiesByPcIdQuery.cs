using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellInfo.Queries.Get
{
    public record GetSpellInfoWithAbilitiesByPcIdQuery : IRequest<Result<SpellInfoAndAbilitiesVM>>, IQuery
    {
        public string Id { get; init; }
    }
    public class GetSpellInfoWithAbilitiesByPcIdQueryHandler : IRequestHandler<GetSpellInfoWithAbilitiesByPcIdQuery, Result<SpellInfoAndAbilitiesVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSpellInfoWithAbilitiesByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<SpellInfoAndAbilitiesVM>> Handle(GetSpellInfoWithAbilitiesByPcIdQuery request, CancellationToken cancellationToken)
        {

            var spellinfo = await _dbContext.SpellInfo.FindAsync(new object[] { request.Id }, cancellationToken);

            if(spellinfo == null) return Result<SpellInfoAndAbilitiesVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });

            var result = await _dbContext.Pcs.ProjectToSingle<Domain.Entities.Pc, SpellInfoAndAbilitiesVM>(x => x.Id.Equals(spellinfo.PcId), _mapper.ConfigurationProvider);
            if (result != null) return Result<SpellInfoAndAbilitiesVM>.Success(result);

            return Result<SpellInfoAndAbilitiesVM>.Failure(null, new List<string>() { $"Can't find record with id: {spellinfo.PcId}" });

        }
    }

}
