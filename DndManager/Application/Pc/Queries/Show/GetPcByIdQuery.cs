using Application.Common.Extentions;
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

namespace Application.Pc.Queries.Show
{
    public record GetPcByIdQuery : IRequest<Result<PcDetailsVM>>, IQuery
    {
        public string Id { get; set; }
    }

    public class GetPcByIdQueryHandler : IRequestHandler<GetPcByIdQuery, Result<PcDetailsVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPcByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<PcDetailsVM>> Handle(GetPcByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Pcs.ProjectToSingle<Domain.Entities.Pc, PcDetailsVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);

            if (result != null) return Result<PcDetailsVM>.Success(result);

            return Result<PcDetailsVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
        }

    }
}
