using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext, IDbContext //IdentityDbContext<User>
    {
        public DbSet<Pc> Pcs { get; set; }
        public DbSet<Test> Tests { get; set; }

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<DndClass> DndClasses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Proficiency> Proficiencies { get; set; }
        public DbSet<Feat> Feats { get; set; }

        public DbSet<Bio> Bio { get; set; }
        public DbSet<SpellInfo> SpellInfo { get; set; }
        public DbSet<SpellLvlInfo> SpellLvlInfo { get; set; }
        public DbSet<Spell> Spells { get; set; }

        public DbSet<CombatAction> CombatActions { get; set; }
        public DbSet<CombatAttack> CombatAttacks { get; set; }
        public DbSet<CombatDamage> CombatDamages { get; set; }
        public DbSet<CombatSavingThrow> CombatSavingThrows { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
