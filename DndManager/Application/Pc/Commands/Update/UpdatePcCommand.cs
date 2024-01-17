using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Commands.Update
{
    public record UpdatePcCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public byte[] Image { get; init; } //todo: byte or ifile?
        public string RaceName { get; init; }
        public string BackgroundName { get; init; }
    }

    public class UpdatePcCommandHandler : IRequestHandler<UpdatePcCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.Pc> _repository;

        public UpdatePcCommandHandler(IRepository<Domain.Entities.Pc> repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(UpdatePcCommand request, CancellationToken cancellationToken)
        {
            var pc = new Domain.Entities.Pc()
            {
                Id = request.Id,
                Name = request.Name,
                Image = request.Image,
                RaceName = request.RaceName,
                BackgroundName = request.BackgroundName
            };

            _repository.Update(pc);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
