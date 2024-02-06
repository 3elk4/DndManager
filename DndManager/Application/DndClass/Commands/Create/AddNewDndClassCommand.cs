
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DndClass.Commands.Create
{
    public record AddNewDndClassCommand : IRequest<Result<DndClassVM>>, ICommand
    {
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string SubclassName { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewDndClassCommandHandler : IRequestHandler<AddNewDndClassCommand, Result<DndClassVM>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewDndClassCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<DndClassVM>> Handle(AddNewDndClassCommand request, CancellationToken cancellationToken)
        {
            var dndClass = new Domain.Entities.DndClass()
            {
                Name = request.Name,
                Lvl = request.Lvl,
                SubclassName = request.SubclassName ?? "",
                PcId = request.PcId
            };

            _dbContext.DndClasses.Add(dndClass);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ?
                Result<DndClassVM>.Success(_mapper.Map<DndClassVM>(dndClass)) :
                Result<DndClassVM>.Failure(null, new List<string>() { "Some problems occured during creating record." });
        }
    }
}
