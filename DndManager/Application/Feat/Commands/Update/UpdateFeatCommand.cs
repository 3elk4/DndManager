using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Feat.Command.Update
{
    public record UpdateFeatCommand : IRequest<Result<int>>, ICommand
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Definition { get; set; }
        public string Id { get; set; }
    }


    public class UpdateFeatCommandHandler : IRequestHandler<UpdateFeatCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateFeatCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateFeatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Feats.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find an item with id {request.Id}." });

            entity.Title = request.Title;
            entity.Source = request.Source;
            entity.SourceType = request.SourceType;
            entity.Definition = request.Definition ?? "";

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
