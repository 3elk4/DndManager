using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Item.Commands.UpdateMany
{
    public record UpdateManyItemsCommand : IRequest<Result<int>>, ICommand
    {
        public IEnumerable<ItemVM> Items { get; init; }
    }

    public class UpdateManyItemsCommandHandler : IRequestHandler<UpdateManyItemsCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.Item> _repository;
        private readonly IMapper _mapper;

        public UpdateManyItemsCommandHandler(IRepository<Domain.Entities.Item> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateManyItemsCommand request, CancellationToken cancellationToken)
        {
            var items = _mapper.Map<IQueryable<Domain.Entities.Item>>(request.Items);

            _repository.UpdateMany(items);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == request.Items.Count() ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
