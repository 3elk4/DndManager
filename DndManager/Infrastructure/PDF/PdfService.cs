using Application.Common.Interfaces;
using QuestPDF.Fluent;

namespace Infrastructure.PDF
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateNpcPdf(Npc Npc)
        {
            var document = new NpcDocument(Npc);
            return Document.Create(document.Compose).GeneratePdf();
        }

        public byte[] GeneratePcPdf(Pc Pc)
        {
            var document = new PcDocument(Pc);
            return Document.Create(document.Compose).GeneratePdf();
        }
    }
}
