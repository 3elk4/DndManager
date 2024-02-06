using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DndClass.Commands.Update
{
    public record UpdateDndClassCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string SubclassName { get; set; }
    }

    public class UpdateDndClassCommandHandler : IRequestHandler<UpdateDndClassCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateDndClassCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateDndClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.DndClasses.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find a class with id {request.Id}." });

            entity.Name = request.Name;
            entity.SubclassName = request.SubclassName ?? "";
            entity.Lvl = request.Lvl;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
