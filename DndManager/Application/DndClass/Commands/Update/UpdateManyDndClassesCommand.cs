using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DndClass.Commands.Update
{
    public record UpdateManyDndClassesCommand : IRequest<Result<int>>, ICommand
    {
        public IEnumerable<DndClassVM> DndClasses { get; init; }
    }

    public class UpdateManyDndClassesCommandHandler : IRequestHandler<UpdateManyDndClassesCommand, Result>
    {
        private readonly IRepository<Domain.Entities.DndClass> _repository;
        private readonly IMapper _mapper;

        public UpdateManyDndClassesCommandHandler(IRepository<Domain.Entities.DndClass> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateManyDndClassesCommand request, CancellationToken cancellationToken)
        {
            var dndClasses = _mapper.Map<IQueryable<Domain.Entities.DndClass>>(request.DndClasses);

            _repository.UpdateMany(dndClasses);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == request.DndClasses.Count() ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
