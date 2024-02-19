using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcFeat.Commands.Create
{
    public record AddNewFeatCommand : IRequest<Result<NpcFeatVM>>, ICommand
    {
        public string Name { get; set; }
        public string TimeRegeneration { get; set; }
        public string Description { get; set; }
        public string NpcId { get; set; }
    }

    public class AddNewFeatCommandHandler : IRequestHandler<AddNewFeatCommand, Result<NpcFeatVM>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewFeatCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<NpcFeatVM>> Handle(AddNewFeatCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.NpcFeat()
            {
                Name = request.Name,
                TimeRegeneration = request.TimeRegeneration,
                Description = request.Description,
                NpcId = request.NpcId
            };

            _dbContext.NpcFeats.Add(item);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ?
                Result<NpcFeatVM>.Success(_mapper.Map<NpcFeatVM>(item)) :
                Result<NpcFeatVM>.Failure(null, new List<string>() { "Some problems occured during creating record." });
        }
    }
}
