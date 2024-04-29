using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;

namespace Application.Npc.Queries.Index
{
    [Authorize]
    public record GetManyNpcsQuery : IRequest<PaginatedListVM<NpcBriefVM>>, IQuery
    {
        public string SearchString { get; init; }
        public int PageNumber { get; set; }
        public int PageSize { get; init; }
    }

    public class GetManyNpcsQueryHandler : IRequestHandler<GetManyNpcsQuery, PaginatedListVM<NpcBriefVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public GetManyNpcsQueryHandler(IDbContext dbContext, IMapper mapper, IUser user)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _user = user;
        }

        public async Task<PaginatedListVM<NpcBriefVM>> Handle(GetManyNpcsQuery request, CancellationToken cancellationToken)
        {
            var userNpcs = _dbContext.Npcs.ByUser(_user);
            var npcsVMs = userNpcs.ProjectTo<NpcBriefVM>(_mapper.ConfigurationProvider);

            if (request.SearchString != null)
            {
                request.PageNumber = 1;
            }

            if (!String.IsNullOrEmpty(request.SearchString))
            {
                var ss = request.SearchString.ToLower();
                npcsVMs = npcsVMs.Where(pc => pc.Name.ToLower().Contains(ss) ||
                                      pc.Alignment.ToLower().Contains(ss) ||
                                      pc.Type.ToLower().Contains(ss));
            }

            return await npcsVMs.PaginatedListAsync<NpcBriefVM>(request.PageNumber, request.PageSize);
        }

    }
}
