using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Item.Commands.Create
{
    public record AddNewItemCommand : IRequest<Result<ItemVM>>, ICommand
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string Notes { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewItemCommandHandler : IRequestHandler<AddNewItemCommand, Result<ItemVM>>
    {
        private readonly IRepository<Domain.Entities.Item> _repository;
        private readonly IMapper _mapper;

        public AddNewItemCommandHandler(IRepository<Domain.Entities.Item> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ItemVM>> Handle(AddNewItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.Item()
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Weight = request.Weight,
                Notes = request.Notes,
                PcId = request.PcId
            };

            _repository.Insert(item);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ?
                Result<ItemVM>.Success(_mapper.Map<ItemVM>(item)) :
                Result<ItemVM>.Failure(null, new List<string>() { "Some problems occured during creating record." });
        }
    }
}
