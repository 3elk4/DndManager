using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Spell;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellLvlInfo.Commands.Update
{
    public record UpdateSpellLvlInfoCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public int Max { get; init; }
        public int Remaining { get; init; }
    }

    public class UpdateSpellLvlInfoCommandHandler : IRequestHandler<UpdateSpellLvlInfoCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;

        public UpdateSpellLvlInfoCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> Handle(UpdateSpellLvlInfoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.SpellLvlInfo.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find a spell lvl with id {request.Id}." });

            entity.Max = request.Max;
            entity.Remaining = request.Remaining;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ?
                    Result<int>.Success(result) :
                    Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
