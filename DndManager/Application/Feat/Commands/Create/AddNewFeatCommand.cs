using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feat.Command.Create
{
    public record AddNewFeatCommand : IRequest<Result<FeatVM>>, ICommand
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Definition { get; set; }
        public string PcId { get; set; }
    }

    public class AddNewFeatCommandHandler : IRequestHandler<AddNewFeatCommand, Result<FeatVM>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewFeatCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<FeatVM>> Handle(AddNewFeatCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.Feat()
            {
                Title = request.Title,
                Source = request.Source,
                SourceType = request.SourceType,
                Definition = request.Definition ?? "",
                PcId = request.PcId
            };

            _dbContext.Feats.Add(item);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ?
                Result<FeatVM>.Success(_mapper.Map<FeatVM>(item)) :
                Result<FeatVM>.Failure(null, new List<string>() { "Some problems occured during creating record." });
        }
    }
}
