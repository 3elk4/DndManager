using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Queries.Get
{
    public record GetPcByIdQuery : IRequest<Result<PcVM>>, IQuery
    {
        public string Id { get; set; }
    }

    public class GetPcByIdQueryHandler : IRequestHandler<GetPcByIdQuery, Result<PcVM>>, IQuery
    {
        private readonly IRepository<Domain.Entities.Pc> _repository;
        private readonly IMapper _mapper;

        public GetPcByIdQueryHandler(IRepository<Domain.Entities.Pc> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PcVM>> Handle(GetPcByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByID(request.Id);

            if (result != null) return Result<PcVM>.Success(_mapper.Map<PcVM>(result));

            return Result<PcVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
        }

    }
}
