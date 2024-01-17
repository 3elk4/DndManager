using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellLvlInfo.Queries.Get
{
    public record GetSpellLvlInfoQuery : IRequest<Result<SpellLvlInfoVM>>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetSpellLvlInfoQueryHandler : IRequestHandler<GetSpellLvlInfoQuery, Result<SpellLvlInfoVM>>, IQuery
    {
        private readonly IRepository<Domain.Entities.SpellLvlInfo> _repository;
        private readonly IMapper _mapper;

        public GetSpellLvlInfoQueryHandler(IRepository<Domain.Entities.SpellLvlInfo> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<SpellLvlInfoVM>> Handle(GetSpellLvlInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByID(request.Id, "Spells");
            if (result != null) return Result<SpellLvlInfoVM>.Success(_mapper.Map<SpellLvlInfoVM>(result));

            return Result<SpellLvlInfoVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
        }
    }
}
