﻿using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Application.Item.Queries.Index
{
    [Authorize(Policy = Policies.OnlyOwnedPc, ProperiesNames = ["Id"])]
    public record GetManyItemsByPcIdQuery : IRequest<List<ItemVM>>, IQuery
    {
        public string Id { get; init; }
    }

    public class GetManyItemsByPcIdQueryHandler : IRequestHandler<GetManyItemsByPcIdQuery, List<ItemVM>>, IQuery
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetManyItemsByPcIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ItemVM>> Handle(GetManyItemsByPcIdQuery request, CancellationToken cancellationToken)
        {
            {
                return await _dbContext
                    .Items
                    .Where(item => item.PcId.Equals(request.Id))
                    .ProjectToListAsync<ItemVM>(_mapper.ConfigurationProvider);
            }

        }
    }
}
