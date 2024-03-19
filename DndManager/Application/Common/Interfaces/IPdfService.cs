namespace Application.Common.Interfaces
{
    public interface IPdfService
    {
        byte[] GeneratePcPdf(Domain.Entities.Pc Pc);
        byte[] GenerateNpcPdf(Domain.Entities.Npc Npc);
    }
}
