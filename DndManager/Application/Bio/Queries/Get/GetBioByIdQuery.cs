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
        private readonly IRepository<Domain.Entities.Bio> _repository;
        private readonly IMapper _mapper;

        public GetBioByIdQueryHandler(IRepository<Domain.Entities.Bio> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<BioVM>> Handle(GetBioByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByID(request.Id);

            if (result != null)  return Result<BioVM>.Success(_mapper.Map<BioVM>(result));

            return Result<BioVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
        }
    }
}