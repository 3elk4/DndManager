using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDbContext
    {
        DbSet<Domain.Entities.Pc> Pcs { get; }
        DbSet<Domain.Entities.Npc> Npcs { get; }

        DbSet<Domain.Entities.Ability> Abilities { get; }
        DbSet<Domain.Entities.NpcAbility> NpcAbilities { get; }
        DbSet<Domain.Entities.Skill> Skills { get; }
        DbSet<Domain.Entities.NpcSkill> NpcSkills { get; }
        DbSet<Domain.Entities.DndClass> DndClasses { get; }
        DbSet<Domain.Entities.Item> Items { get; }
        DbSet<Domain.Entities.Proficiency> Proficiencies { get; }
        DbSet<Domain.Entities.NpcProficiency> NpcProficiencies { get; }
        DbSet<Domain.Entities.Feat> Feats { get; }
        DbSet<Domain.Entities.NpcFeat> NpcFeats { get; }

        DbSet<Domain.Entities.Bio> Bio { get; }
        DbSet<Domain.Entities.SpellInfo> SpellInfo { get; }
        DbSet<Domain.Entities.NpcSpellInfo> NpcSpellInfo { get; }
        DbSet<Domain.Entities.SpellLvlInfo> SpellLvlInfo { get; }
        DbSet<Domain.Entities.Spell> Spells { get; }

        DbSet<Domain.Entities.CombatAction> CombatActions { get; }
        DbSet<Domain.Entities.NpcAction> NpcActions { get; }
        DbSet<Domain.Entities.CombatAttack> CombatAttacks { get; }
        DbSet<Domain.Entities.NpcAttack> NpcAttacks { get; }
        DbSet<Domain.Entities.CombatDamage> CombatDamages { get; }
        DbSet<Domain.Entities.NpcDamage> NpcDamages { get; }
        DbSet<Domain.Entities.CombatSavingThrow> CombatSavingThrows { get; }

        DbSet<Domain.Entities.Money> Money { get; }

        DbSet<TEntity> GetSet<TEntity>() where TEntity : BaseAuditableEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
