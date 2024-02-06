using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SpellInfo.Commands.Update
{

    public record UpdateSpellInfoCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public string AbilityId { get; set; }
    }

    public class UpdateSpellInfoCommandHandler : IRequestHandler<UpdateSpellInfoCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;

        public UpdateSpellInfoCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> Handle(UpdateSpellInfoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.SpellInfo.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find a spell info with id {request.Id}." });

            entity.AbilityId = request.AbilityId;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ?
                    Result<int>.Success(result) :
                    Result<int>.Failure(0, new List<string>() { "Some errors occured during updating record." });
        }
    }
}
