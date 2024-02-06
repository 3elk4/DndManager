using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Queries.Index
{
    public record GetManyPcsQuery : IRequest<Result<PaginatedListVM<PcBriefVM>>>, IQuery
    {
        public string SearchString { get; init; }
        public int PageNumber { get; set; }
        public int PageSize { get; init; }
    }

    public class GetManyPcsQueryHandler : IRequestHandler<GetManyPcsQuery, Result<PaginatedListVM<PcBriefVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyPcsQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedListVM<PcBriefVM>>> Handle(GetManyPcsQuery request, CancellationToken cancellationToken)
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

            var result = await pcs.PaginatedListAsync<PcBriefVM>(request.PageNumber, request.PageSize);
            return Result<PaginatedListVM<PcBriefVM>>.Success(result);
        }

    }
}
