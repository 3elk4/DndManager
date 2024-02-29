using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NpcProficiency.Commands.Create
{
    public record AddNewProficiencyCommand : IRequest<Result<NpcProficiencyVM>>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Range { get; set; }
        public string NpcId { get; set; }
    }

    public class AddNewProficiencyCommandHandler : IRequestHandler<AddNewProficiencyCommand, Result<NpcProficiencyVM>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;


        public AddNewProficiencyCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<NpcProficiencyVM>> Handle(AddNewProficiencyCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.NpcProficiency()
            {
                Name = request.Name,
                Type = request.Type,
                Range = request.Range,
                NpcId = request.NpcId
            };

            _dbContext.NpcProficiencies.Add(item);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ?
                Result<NpcProficiencyVM>.Success(_mapper.Map<NpcProficiencyVM>(item)) :
                Result<NpcProficiencyVM>.Failure(null, new List<string>() { "Some problems occured during creating record." });
        }
    }
}
