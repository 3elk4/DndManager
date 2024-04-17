using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace Application.Pc.Queries.GeneratePdf
{
    public record GeneratePcPdfQuery : IRequest<PdfResult>, ICommand
    {
        public string Id { get; init; }
    }

    public class UpdatePcCommandHandler : IRequestHandler<GeneratePcPdfQuery, PdfResult>
    {
        private readonly IDbContext _dbContext;
        private readonly IPdfService _pdfService;

        public UpdatePcCommandHandler(IDbContext dbContext, IPdfService pdfService)
        {
            _dbContext = dbContext;
            _pdfService = pdfService;
        }

        public async Task<PdfResult> Handle(GeneratePcPdfQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Pcs.Where(i => i.Id.Equals(request.Id))
            .Include(pc => pc.Bio)
            .Include(pc => pc.SpellInfo)
                .ThenInclude(spellInfo => spellInfo.SpellLvls)
                .ThenInclude(spellLvlInfo => spellLvlInfo.Spells)
            .FirstOrDefaultAsync(cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            /// atomatically includes it to entity and it's much faster than direct include
            await _dbContext.Abilities.Where(i => i.PcId.Equals(request.Id)).Include(pc => pc.Skills).ToListAsync(cancellationToken);
            await _dbContext.DndClasses.Where(i => i.PcId.Equals(request.Id)).ToListAsync(cancellationToken);
            await _dbContext.Items.Where(i => i.PcId.Equals(request.Id)).ToListAsync(cancellationToken);
            await _dbContext.Feats.Where(i => i.PcId.Equals(request.Id)).ToListAsync(cancellationToken);
            await _dbContext.Proficiencies.Where(i => i.PcId.Equals(request.Id)).ToListAsync(cancellationToken);
            await _dbContext.CombatActions.Where(i => i.PcId.Equals(request.Id))
                .Include(ca => ca.CombatAttack)
                .Include(ca => ca.CombatDamage)
                .Include(ca => ca.CombatSavingThrow)
                .ToListAsync(cancellationToken);

            return new PdfResult()
            {
                Filename = entity.Name.Trim().Replace(' ', '_').ToLower() + ".pdf",
                MemoryStream = new MemoryStream(_pdfService.GeneratePcPdf(entity))
            };
        }
    }
}
