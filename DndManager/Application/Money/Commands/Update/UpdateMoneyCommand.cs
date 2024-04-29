using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.Money.Commands.Update
{
    [Authorize(Policy = Policies.OnlyOwnedMoney, ProperiesNames = ["Id"])]
    public record UpdateMoneyCommand : IRequest, ICommand
    {
        public string Id { get; set; }
        public int Copper { get; set; }
        public int Silver { get; set; }
        public int Electrum { get; set; }
        public int Gold { get; set; }
        public int Platinum { get; set; }
    }

    public class UpdateMoneyCommandHandler : IRequestHandler<UpdateMoneyCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateMoneyCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateMoneyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Money.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Copper = request.Copper;
            entity.Silver = request.Silver;
            entity.Electrum = request.Electrum;
            entity.Gold = request.Gold;
            entity.Platinum = request.Platinum;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
