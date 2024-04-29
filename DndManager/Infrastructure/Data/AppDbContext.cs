using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>, IDbContext
    {
        public DbSet<Pc> Pcs { get; set; }
        public DbSet<Npc> Npcs { get; set; }

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<NpcAbility> NpcAbilities { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<NpcSkill> NpcSkills { get; set; }
        public DbSet<DndClass> DndClasses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Proficiency> Proficiencies { get; set; }
        public DbSet<NpcProficiency> NpcProficiencies { get; set; }
        public DbSet<Feat> Feats { get; set; }
        public DbSet<NpcFeat> NpcFeats { get; set; }

        public DbSet<Bio> Bio { get; set; }
        public DbSet<SpellInfo> SpellInfo { get; set; }
        public DbSet<NpcSpellInfo> NpcSpellInfo { get; set; }
        public DbSet<SpellLvlInfo> SpellLvlInfo { get; set; }
        public DbSet<Spell> Spells { get; set; }

        public DbSet<CombatAction> CombatActions { get; set; }
        public DbSet<NpcAction> NpcActions { get; set; }
        public DbSet<CombatAttack> CombatAttacks { get; set; }
        public DbSet<NpcAttack> NpcAttacks { get; set; }
        public DbSet<CombatDamage> CombatDamages { get; set; }
        public DbSet<NpcDamage> NpcDamages { get; set; }
        public DbSet<CombatSavingThrow> CombatSavingThrows { get; set; }
        public DbSet<Money> Money { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : BaseAuditableEntity
        {
            return this.Set<TEntity>();
        }
    }
}
