using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bio.Queries.Get
{
    public record GetBioByIdQuery : IRequest<Result<BioVM>>
    {
        public string Id { get; init; }
    }

    public class GetBioByIdQueryHandler : IRequestHandler<GetBioByIdQuery, Result<BioVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBioByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<BioVM>> Handle(GetBioByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Bio.ProjectToSingle<Domain.Entities.Bio, BioVM>(x => x.Id.Equals(request.Id), _mapper.ConfigurationProvider);

            if (result != null) return Result<BioVM>.Success(result);

            return Result<BioVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
        }
    }
}