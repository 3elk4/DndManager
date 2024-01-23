﻿using Application.Ability;
using Application.Bio;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DndClass;
using Application.SpellInfo;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pc.Commands.Create
{
    public record AddNewPcCommand : IRequest<Result>, ICommand
    {
        public string Name { get; init; }
        public byte[] Image { get; init; } //todo: byte or ifile?
        public string RaceName { get; init; }
        public string BackgroundName { get; init; }

        public int AC { get; init; }
        public string Speed { get; init; }
        public int HP { get; init; }
        public string HitDice { get; init; }
        public IReadOnlyCollection<AbilityVM> Abilities { get; init; }
        public IReadOnlyCollection<DndClassVM> DndClasses { get; init; }
        public BioVM Bio { get; init; }
        public SpellInfoVM SpellInfo { get; init; }
    }

    public class AddNewPcCommandHandler : IRequestHandler<AddNewPcCommand, Result>
    {
        private readonly IRepository<Domain.Entities.Pc> _repository;
        private readonly IMapper _mapper;

        public AddNewPcCommandHandler(IRepository<Domain.Entities.Pc> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(AddNewPcCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.Pc()
            {
                Name = request.Name,
                Image = request.Image,
                RaceName = request.RaceName,
                BackgroundName = request.BackgroundName,
                AC = request.AC,
                Speed = request.Speed,
                HP = request.HP,
                HitDice = request.HitDice,
                Proficiency = 2,
                Bio = _mapper.Map<Domain.Entities.Bio>(request.Bio),
                SpellInfo = _mapper.Map<Domain.Entities.SpellInfo>(request.SpellInfo),
                Abilities = _mapper.Map<List<Domain.Entities.Ability>>(request.Abilities),
                DndClasses = _mapper.Map<List<Domain.Entities.DndClass>>(request.DndClasses),
            };

            _repository.Insert(item);
            var result = await _repository.SaveAsync(cancellationToken);

            return result == 1 ? Result.Success() : Result.Failure(new List<string>() { "Some problems occured during creating record." });
        }
    }
}