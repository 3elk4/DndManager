using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Proficiency.Commands.Create
{
    public record AddNewProficiencyCommand : IRequest<Result<ProficiencyVM>>, ICommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewProficiencyCommandHandler : IRequestHandler<AddNewProficiencyCommand, Result<ProficiencyVM>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;


        public AddNewProficiencyCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<ProficiencyVM>> Handle(AddNewProficiencyCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.Proficiency()
            {
                Name = request.Name,
                Type = request.Type,
                PcId = request.PcId
            };

            _dbContext.Proficiencies.Add(item);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ?
                Result<ProficiencyVM>.Success(_mapper.Map<ProficiencyVM>(item)) :
                Result<ProficiencyVM>.Failure(null, new List<string>() { "Some problems occured during creating record." });
        }
    }
}
