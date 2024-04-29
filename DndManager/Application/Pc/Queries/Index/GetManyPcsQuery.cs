using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;

namespace Application.Pc.Queries.Index
{
    [Authorize]
    public record GetManyPcsQuery : IRequest<PaginatedListVM<PcBriefVM>>, IQuery
    {
        public string SearchString { get; init; }
        public int PageNumber { get; set; }
        public int PageSize { get; init; }
    }

    public class GetManyPcsQueryHandler : IRequestHandler<GetManyPcsQuery, PaginatedListVM<PcBriefVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public GetManyPcsQueryHandler(IDbContext dbContext, IMapper mapper, IUser user)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _user = user;
        }

        public async Task<PaginatedListVM<PcBriefVM>> Handle(GetManyPcsQuery request, CancellationToken cancellationToken)
        {
            var userPcs = _dbContext.Pcs.ByUser(_user);
            var pcsVMs = userPcs.ProjectTo<PcBriefVM>(_mapper.ConfigurationProvider);

            if (request.SearchString != null)
            {
                request.PageNumber = 1;
            }

            if (!String.IsNullOrEmpty(request.SearchString))
            {
                var ss = request.SearchString.ToLower();
                pcsVMs = pcsVMs.Where(pc => pc.Name.ToLower().Contains(ss) ||
                                      pc.RaceName.ToLower().Contains(ss) ||
                                      pc.BackgroundName.ToLower().Contains(ss) ||
                                      pc.DndClasses.Any(dclass => dclass.Name.ToLower().Contains(ss) || dclass.SubclassName.ToLower().Contains(ss)));
            }

            return await pcsVMs.PaginatedListAsync(request.PageNumber, request.PageSize);
        }

    }
}
