using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Proficiency.Commands.UpdateMany
{
    public record UpdateManyProficienciesCommand : IRequest<Result<int>>, ICommand
    {
        public IEnumerable<ProficiencyVM> Proficiencies { get; init; }
    }

    public class UpdateManyProficienciesCommandHandler : IRequestHandler<UpdateManyProficienciesCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.Proficiency> _repository;
        private readonly IMapper _mapper;

        public UpdateManyProficienciesCommandHandler(IRepository<Domain.Entities.Proficiency> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateManyProficienciesCommand request, CancellationToken cancellationToken)
        {
            var proficiencies = _mapper.Map<IQueryable<Domain.Entities.Proficiency>>(request.Proficiencies);

            _repository.UpdateMany(proficiencies);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == request.Proficiencies.Count() ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
