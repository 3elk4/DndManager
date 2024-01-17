using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bio.Commands.Create
{
    public record AddNewBioCommand : IRequest<Result<string>>, ICommand
    {
        public int Age { get; init; }
        public string Size { get; init; }
        public string Weight { get; init; }
        public string Height { get; init; }
        public string Skin { get; init; }
        public string Eyes { get; init; }
        public string Hair { get; init; }
        public string Alignment { get; init; }
        public string Traits { get; init; }
        public string Flaws { get; init; }
        public string Bonds { get; init; }
        public string Ideals { get; init; }
        public string Allies { get; init; }
        public string Backstory { get; init; }
        public string PcId { get; init; }
    }

    public class AddNewBioCommandHandler : IRequestHandler<AddNewBioCommand, Result<string>>
    {
        private readonly IRepository<Domain.Entities.Bio> _repository;

        public AddNewBioCommandHandler(IRepository<Domain.Entities.Bio> repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(AddNewBioCommand request, CancellationToken cancellationToken)
        {
            var bio = new Domain.Entities.Bio()
            {
                Age = request.Age,
                Size = request.Size,
                Weight = request.Weight,
                Height = request.Height,
                Skin = request.Skin,
                Eyes = request.Eyes,
                Hair = request.Hair,
                Alignment = request.Alignment,
                Traits = request.Traits,
                Flaws = request.Flaws,
                Bonds = request.Bonds,
                Ideals = request.Ideals,
                Allies = request.Allies,
                Backstory = request.Backstory,
                PcId = request.PcId
            };

            _repository.Insert(bio);
            await _repository.SaveAsync(cancellationToken);

            return Result<string>.Success(bio.Id);
        }
    }
}