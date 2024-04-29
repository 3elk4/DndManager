using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;

namespace Application.Item.Commands.Update
{
    [Authorize(Policy = Policies.OnlyOwnedItem, ProperiesNames = ["Id"])]
    public record UpdateItemCommand : IRequest, ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateItemCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Items.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.Quantity = request.Quantity;
            entity.Weight = request.Weight;
            entity.Notes = request.Notes ?? "";

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
