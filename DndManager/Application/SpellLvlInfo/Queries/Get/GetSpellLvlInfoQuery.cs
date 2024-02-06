using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellLvlInfo.Queries.Get
{
    public record GetSpellLvlInfoQuery : IRequest<Result<SpellLvlInfoVM>>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetSpellLvlInfoQueryHandler : IRequestHandler<GetSpellLvlInfoQuery, Result<SpellLvlInfoVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSpellLvlInfoQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<SpellLvlInfoVM>> Handle(GetSpellLvlInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.SpellLvlInfo.ProjectToSingle<Domain.Entities.SpellLvlInfo, SpellLvlInfoVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);
            if (result != null) return Result<SpellLvlInfoVM>.Success(result);

            return Result<SpellLvlInfoVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
        }
    }
}
