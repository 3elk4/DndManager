﻿using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellInfo.Queries.Get
{
    public record GetSpellInfoWithAbilitiesByPcIdQuery : IRequest<Result<SpellInfoAndAbilitiesVM>>, IQuery
    {
        public string Id { get; init; }
    }
    public class GetSpellInfoWithAbilitiesByPcIdQueryHandler : IRequestHandler<GetSpellInfoWithAbilitiesByPcIdQuery, Result<SpellInfoAndAbilitiesVM>>, IQuery
    {
        private readonly IRepository<Domain.Entities.Pc> _repository;
        private readonly IMapper _mapper;

        public GetSpellInfoWithAbilitiesByPcIdQueryHandler(IRepository<Domain.Entities.Pc> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<SpellInfoAndAbilitiesVM>> Handle(GetSpellInfoWithAbilitiesByPcIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByID(request.Id, "SpellInfo, SpellInfo.SpellLvls, Abilities");
            if (result != null) return Result<SpellInfoAndAbilitiesVM>.Success(_mapper.Map<SpellInfoAndAbilitiesVM>(result));

            return Result<SpellInfoAndAbilitiesVM>.Failure(null, new List<string>() { $"Can't find record with id: {request.Id}" });

        }
    }

}
