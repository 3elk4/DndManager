using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Proficiency.Commands.Update
{
    public record UpdateProficiencyCommand : IRequest<Result<int>>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
    }


    public class UpdateProficiencyCommandHandler : IRequestHandler<UpdateProficiencyCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateProficiencyCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateProficiencyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Proficiencies.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find an item with id {request.Id}." });

            entity.Name = request.Name;
            entity.Type = request.Type;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
