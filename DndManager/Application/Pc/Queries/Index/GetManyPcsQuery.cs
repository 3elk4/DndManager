using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;

namespace Application.Pc.Queries.Index
{
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

        public GetManyPcsQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedListVM<PcBriefVM>> Handle(GetManyPcsQuery request, CancellationToken cancellationToken)
        {
            var pcs = _dbContext.Pcs.ProjectTo<PcBriefVM>(_mapper.ConfigurationProvider);

            if (request.SearchString != null)
            {
                request.PageNumber = 1;
            }

            if (!String.IsNullOrEmpty(request.SearchString))
            {
                var ss = request.SearchString.ToLower();
                pcs = pcs.Where(pc => pc.Name.ToLower().Contains(ss) ||
                                      pc.RaceName.ToLower().Contains(ss) ||
                                      pc.BackgroundName.ToLower().Contains(ss) ||
                                      pc.DndClasses.Any(dclass => dclass.Name.ToLower().Contains(ss) || dclass.SubclassName.ToLower().Contains(ss)));
            }

            return await pcs.PaginatedListAsync<PcBriefVM>(request.PageNumber, request.PageSize);
        }

    }
}
