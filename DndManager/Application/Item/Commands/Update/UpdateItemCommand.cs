using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Item.Commands.Update
{
    public record UpdateItemCommand : IRequest<Result<int>>, ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result<int>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateItemCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Items.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) Result<int>.Failure(0, new List<string>() { $"Can't find an item with id {request.Id}." });

            entity.Name = request.Name;
            entity.Quantity = request.Quantity;
            entity.Weight = request.Weight;
            entity.Notes = request.Notes ?? "";

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                   Result<int>.Success(result) :
                   Result<int>.Failure(0, new List<string>() { "Some errors occured during updating records." });
        }
    }
}
