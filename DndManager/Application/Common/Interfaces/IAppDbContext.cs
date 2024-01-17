using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Domain.Entities.Pc> Pcs { get; }
        DbSet<Domain.Entities.Test> Tests { get; }
    }
}
