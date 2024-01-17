using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ability.Commands.UpdateManyAbilities
{
    public record UpdateManyAbilitiesCommand(List<AbilityVM> Abilities) : IRequest<Result<int>>, ICommand
    {
    }

    public class UpdateManyAbilitiesCommandHandler : IRequestHandler<UpdateManyAbilitiesCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.Ability> _repository;
        private readonly IMapper _mapper;

        public UpdateManyAbilitiesCommandHandler(IRepository<Domain.Entities.Ability> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateManyAbilitiesCommand request, CancellationToken cancellationToken)
        {
            var abilities = _mapper.Map<IQueryable<Domain.Entities.Ability>>(request.Abilities);

            _repository.UpdateMany(abilities);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == request.Abilities.Count() ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
