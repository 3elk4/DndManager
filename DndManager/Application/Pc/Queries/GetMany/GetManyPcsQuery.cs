using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Queries.GetMany
{
    public record GetManyPcsQuery : IRequest<Result<List<PcVM>>>, IQuery
    {
    }

    public class GetManyPcsQueryHandler : IRequestHandler<GetManyPcsQuery, Result<List<PcVM>>>, IQuery
    {
        //private readonly IRepository<Domain.Entities.Pc> _repository;
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetManyPcsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            //_repository = repository;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<PcVM>>> Handle(GetManyPcsQuery request, CancellationToken cancellationToken)
        {
            var result1 = _appDbContext.Pcs;
            var result = await result1.ProjectTo<PcVM>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return Result<List<PcVM>>.Success(result);
            //var result = _repository.Get().AsNoTracking(); //todo: moze faktycznie zrobić to z appdbcontext? a irepository dac do utils
            //return Result<List<PcVM>>.Success(
            //    await 
            //          result
            //              .ProjectTo<PcVM>(_mapper.ConfigurationProvider)
            //              .ToListAsync<PcVM>(cancellationToken));
        }

    }
}
