using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellInfo.Queries.Get
{
    public record GetSpellInfoByIdQuery : IRequest<Result<SpellInfoVM>>, IQuery
    {
        public string Id { get; init; }
    }

    //public class GetSpellInfoByIdQueryHandler : IRequestHandler<GetSpellInfoByIdQuery, Result<SpellInfoVM>>, IQuery
    //{
    //    private readonly IRepository<Domain.Entities.SpellInfo> _repository;
    //    private readonly IMapper _mapper;

    //    public GetSpellInfoByIdQueryHandler(IRepository<Domain.Entities.SpellInfo> repository, IMapper mapper)
    //    {
    //        _repository = repository;
    //        _mapper = mapper;
    //    }

    //    public async Task<Result<SpellInfoVM>> Handle(GetSpellInfoByIdQuery request, CancellationToken cancellationToken)
    //    {
    //        var result = await _repository.GetByID(request.Id, "SpellLvls");
    //        if (result != null) return Result<SpellInfoVM>.Success(_mapper.Map<SpellInfoVM>(result));

    //        return Result<SpellInfoVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });
    //    }
    //}
}
