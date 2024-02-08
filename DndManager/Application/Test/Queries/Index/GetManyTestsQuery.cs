using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;

namespace Application.Test.Queries.GetMany
{
    public record GetManyTestsQuery : IRequest<Result<List<TestVM>>>, IQuery
    {
    }

    public class GetManyTestsQueryHandler : IRequestHandler<GetManyTestsQuery, Result<List<TestVM>>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyTestsQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<TestVM>>> Handle(GetManyTestsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Tests.ProjectToListAsync<TestVM>(_mapper.ConfigurationProvider);
            return Result<List<TestVM>>.Success(result);
        }
    }
}
