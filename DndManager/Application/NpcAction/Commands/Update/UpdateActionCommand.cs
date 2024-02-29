using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcAction.Commands.Update
{
    public record UpdateActionCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public NpcAttackVM Attack { get; set; }
        public NpcDamageVM Damage { get; set; }
    }

    public class UpdateCombatActionCommandHandler : IRequestHandler<UpdateActionCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCombatActionCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.NpcActions.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find a class with id {request.Id}." });

            entity.Name = request.Name;
            entity.Type = request.Type;
            entity.Description = request.Description;

            _dbContext.NpcAttacks.Update(_mapper.Map<Domain.Entities.NpcAttack>(request.Attack));
            _dbContext.NpcDamages.Update(_mapper.Map<Domain.Entities.NpcDamage>(request.Damage));

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating record." });
        }
    }
}
