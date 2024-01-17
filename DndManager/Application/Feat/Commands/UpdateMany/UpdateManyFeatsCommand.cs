using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Feat.Command.UpdateMany
{
    public record UpdateManyFeatsCommand : IRequest<Result<int>>, ICommand
    {
        public IEnumerable<FeatVM> Feats { get; init; }
    }

    public class UpdateManyFeatsCommandHandler : IRequestHandler<UpdateManyFeatsCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.Feat> _repository;
        private readonly IMapper _mapper;

        public UpdateManyFeatsCommandHandler(IRepository<Domain.Entities.Feat> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateManyFeatsCommand request, CancellationToken cancellationToken)
        {
            var feats = _mapper.Map<IQueryable<Domain.Entities.Feat>>(request.Feats);

            _repository.UpdateMany(feats);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == request.Feats.Count() ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
