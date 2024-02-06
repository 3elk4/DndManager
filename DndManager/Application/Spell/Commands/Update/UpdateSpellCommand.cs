using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Spell.Commands.Update
{
    public record UpdateSpellCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CastingTime { get; init; }
        public string CastingRange { get; init; }
        public string Components { get; init; }
        public string Duration { get; init; }
    }

    public class UpdateSpellCommandHandler : IRequestHandler<UpdateSpellCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateSpellCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateSpellCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Spells.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find a class with id {request.Id}." });

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.CastingRange = request.CastingRange;
            entity.CastingTime = request.CastingTime;
            entity.Components = request.Components;
            entity.Duration = request.Duration;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
