using Application.Common.Interfaces;
using Application.Common.Security;

namespace Application.Item.Commands.Create
{
    [Authorize]
    public record AddNewItemCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string Notes { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewItemCommandHandler : IRequestHandler<AddNewItemCommand, string>
    {
        private readonly IDbContext _dbContext;

        public AddNewItemCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(AddNewItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Item()
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Weight = request.Weight,
                Notes = request.Notes ?? "",
                PcId = request.PcId
            };

            _dbContext.Items.Add(entity);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
