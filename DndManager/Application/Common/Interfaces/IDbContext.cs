using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDbContext
    {
        DbSet<Domain.Entities.Pc> Pcs { get; }
        DbSet<Domain.Entities.Test> Tests { get; }

        DbSet<Domain.Entities.Ability> Abilities { get; }
        DbSet<Domain.Entities.Skill> Skills { get; }
        DbSet<Domain.Entities.DndClass> DndClasses { get; }
        DbSet<Domain.Entities.Item> Items { get; }
        DbSet<Domain.Entities.Proficiency> Proficiencies { get; }
        DbSet<Domain.Entities.Feat> Feats { get; }

        DbSet<Domain.Entities.Bio> Bio { get; }
        DbSet<Domain.Entities.SpellInfo> SpellInfo { get; }
        DbSet<Domain.Entities.SpellLvlInfo> SpellLvlInfo { get; }
        DbSet<Domain.Entities.Spell> Spells { get; }

        DbSet<Domain.Entities.CombatAction> CombatActions { get; }
        DbSet<Domain.Entities.CombatAttack> CombatAttacks { get; }
        DbSet<Domain.Entities.CombatDamage> CombatDamages { get; }
        DbSet<Domain.Entities.CombatSavingThrow> CombatSavingThrows { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
