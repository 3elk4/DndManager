using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Domain.Entities.Test> _repository;
        //private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetManyTestsQueryHandler(IRepository<Domain.Entities.Test> repository, IMapper mapper)
        {
            _repository = repository;
            //_appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<TestVM>>> Handle(GetManyTestsQuery request, CancellationToken cancellationToken)
        {
            var result1 = _repository.Get();
            var result = await result1.ProjectTo<TestVM>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return Result<List<TestVM>>.Success(result);
        }

        //public async Task<Result<List<TestVM>>> Handle(GetManyTestsQuery request, CancellationToken cancellationToken)
        //{
        //    var result1 = _appDbContext.Tests;
        //    var result = await result1.ProjectTo<TestVM>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        //    return Result<List<TestVM>>.Success(result);
        //    //var result = _repository.Get().AsNoTracking(); //todo: moze faktycznie zrobić to z appdbcontext? a irepository dac do utils
        //    //return Result<List<PcVM>>.Success(
        //    //    await 
        //    //          result
        //    //              .ProjectTo<PcVM>(_mapper.ConfigurationProvider)
        //    //              .ToListAsync<PcVM>(cancellationToken));
        //}

    }
}
