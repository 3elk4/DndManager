using Application.Common.Interfaces;
using Application.Common.Security;

namespace Application.DndClass.Commands.Create
{
    [Authorize]
    public record AddNewDndClassCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string SubclassName { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewDndClassCommandHandler : IRequestHandler<AddNewDndClassCommand, string>
    {
        private readonly IDbContext _dbContext;

        public AddNewDndClassCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(AddNewDndClassCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.DndClass()
            {
                Name = request.Name,
                Lvl = request.Lvl,
                SubclassName = request.SubclassName ?? "",
                PcId = request.PcId
            };

            _dbContext.DndClasses.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
