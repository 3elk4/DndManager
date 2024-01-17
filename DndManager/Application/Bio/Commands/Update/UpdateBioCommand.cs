using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bio.Commands.Update
{
    public record UpdateBioCommand() : IRequest<Result<int>>, ICommand
    {
        public string Id { get; init; }
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
    }

    public class UpdateBioCommandHandler : IRequestHandler<UpdateBioCommand, Result<int>>
    {
        private readonly IRepository<Domain.Entities.Bio> _repository;

        public UpdateBioCommandHandler(IRepository<Domain.Entities.Bio> repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(UpdateBioCommand request, CancellationToken cancellationToken)
        {
            var bio = new Domain.Entities.Bio()
            {
                Id = request.Id,
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
                Backstory = request.Backstory
            };

            _repository.Update(bio);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ?
                    Result<int>.Success(result) :
                    Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
            }
    }
}
