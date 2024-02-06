
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CombatAction.Commands.Update
{
    public record UpdateCombatActionCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public string Type { get; set; }
        public CombatAttackVM CombatAttack { get; set; }
        public CombatDamageVM CombatDamage { get; set; }
        public CombatSavingThrowVM CombatSavingThrow { get; set; }
    }

    public class UpdateCombatActionCommandHandler : IRequestHandler<UpdateCombatActionCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCombatActionCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateCombatActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.CombatActions.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find a class with id {request.Id}." });

            entity.Name = request.Name;
            entity.Type = request.Type;

            _dbContext.CombatAttacks.Update(_mapper.Map<Domain.Entities.CombatAttack>(request.CombatAttack));
            _dbContext.CombatDamages.Update(_mapper.Map<Domain.Entities.CombatDamage>(request.CombatDamage));
            _dbContext.CombatSavingThrows.Update(_mapper.Map<Domain.Entities.CombatSavingThrow>(request.CombatSavingThrow));

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating record." });
        }
    }
}
