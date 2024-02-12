using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Npc.Queries.Show
{
    public record GetNpcByIdQuery : IRequest<Result<NpcDetailsVM>>, IQuery
    {
        public string Id { get; set; }
    }

    public class GetNpcByIdQueryHandler : IRequestHandler<GetNpcByIdQuery, Result<NpcDetailsVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNpcByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<NpcDetailsVM>> Handle(GetNpcByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Npcs.ProjectToSingle<Domain.Entities.Npc, NpcDetailsVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);

            if (result != null) return Result<NpcDetailsVM>.Success(result);

            return Result<NpcDetailsVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
        }

    }
}
