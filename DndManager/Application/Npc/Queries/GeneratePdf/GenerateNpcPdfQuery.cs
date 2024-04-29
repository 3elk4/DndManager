using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace Application.Npc.Queries.GeneratePdf
{
    [Authorize(Policy = Policies.OnlyOwnedNpc, ProperiesNames = ["Id"])]
    public record GenerateNpcPdfQuery : IRequest<PdfResult>, ICommand
    {
        public string Id { get; init; }
    }

    public class UpdateNpcCommandHandler : IRequestHandler<GenerateNpcPdfQuery, PdfResult>
    {
        private readonly IDbContext _dbContext;
        private readonly IPdfService _pdfService;

        public UpdateNpcCommandHandler(IDbContext dbContext, IPdfService pdfService)
        {
            _dbContext = dbContext;
            _pdfService = pdfService;
        }

        public async Task<PdfResult> Handle(GenerateNpcPdfQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Npcs.Where(i => i.Id.Equals(request.Id))
                                .Include(npc => npc.SpellInfo)
                                .FirstOrDefaultAsync(cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            /// atomatically includes it to entity and it's much faster than direct include
            await _dbContext.NpcAbilities.Where(i => i.NpcId.Equals(request.Id)).Include(npc => npc.Skills).ToListAsync(cancellationToken);
            await _dbContext.NpcFeats.Where(i => i.NpcId.Equals(request.Id)).ToListAsync(cancellationToken);
            await _dbContext.NpcProficiencies.Where(i => i.NpcId.Equals(request.Id)).ToListAsync(cancellationToken);
            await _dbContext.NpcActions.Where(i => i.NpcId.Equals(request.Id)).Include(i => i.Attack).Include(i => i.Damage).ToListAsync(cancellationToken);

            return new PdfResult()
            {
                Filename = entity.Name.Trim().Replace(' ', '_').ToLower() + ".pdf",
                MemoryStream = new MemoryStream(_pdfService.GenerateNpcPdf(entity))
            };
        }
    }
}
