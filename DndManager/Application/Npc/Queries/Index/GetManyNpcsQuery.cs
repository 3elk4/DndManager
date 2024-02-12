using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Npc.Queries.Index
{
    public record GetManyNpcsQuery : IRequest<Result<PaginatedListVM<NpcBriefVM>>>, IQuery
    {
        public string SearchString { get; init; }
        public int PageNumber { get; set; }
        public int PageSize { get; init; }
    }

    public class GetManyNpcsQueryHandler : IRequestHandler<GetManyNpcsQuery, Result<PaginatedListVM<NpcBriefVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyNpcsQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedListVM<NpcBriefVM>>> Handle(GetManyNpcsQuery request, CancellationToken cancellationToken)
        {
            var pcs = _dbContext.Npcs.ProjectTo<NpcBriefVM>(_mapper.ConfigurationProvider);

            if (request.SearchString != null)
            {
                request.PageNumber = 1;
            }

            if (!String.IsNullOrEmpty(request.SearchString))
            {
                var ss = request.SearchString.ToLower();
                pcs = pcs.Where(pc => pc.Name.ToLower().Contains(ss) ||
                                      pc.Alignment.ToLower().Contains(ss) ||
                                      pc.Type.ToLower().Contains(ss));
            }

            var result = await pcs.PaginatedListAsync<NpcBriefVM>(request.PageNumber, request.PageSize);
            return Result<PaginatedListVM<NpcBriefVM>>.Success(result);
        }

    }
}
