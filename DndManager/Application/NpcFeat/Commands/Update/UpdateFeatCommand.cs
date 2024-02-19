using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcFeat.Commands.Update
{
    public record UpdateFeatCommand : IRequest<Result<int>>, ICommand
    {
        public string Name { get; set; }
        public string TimeRegeneration { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }


    public class UpdateFeatCommandHandler : IRequestHandler<UpdateFeatCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateFeatCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateFeatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcFeats.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find an item with id {request.Id}." });

            entity.Name = request.Name;
            entity.TimeRegeneration = request.TimeRegeneration;
            entity.Description = request.Description;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
