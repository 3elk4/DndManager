using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();


            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
            });

            
            services.AddTransient<Repository<Ability>>();
            services.AddTransient<Repository<Bio>>();
            services.AddTransient<Repository<CombatAction>>();
            services.AddTransient<Repository<CombatAttack>>();
            services.AddTransient<Repository<CombatDamage>>();
            services.AddTransient<Repository<CombatSavingThrow>>();
            services.AddTransient<Repository<DndClass>>();
            services.AddTransient<Repository<Feat>>();
            services.AddTransient<Repository<Item>>();
            services.AddTransient<Repository<Pc>>();
            services.AddTransient<Repository<Proficiency>>();
            services.AddTransient<Repository<Skill>>();
            services.AddTransient<Repository<Spell>>();
            services.AddTransient<Repository<SpellInfo>>();
            services.AddTransient<Repository<SpellLvlInfo>>();
            services.AddTransient<Repository<Test>>();

            services.AddScoped<IRepository<Ability>>(provider => provider.GetRequiredService<Repository<Ability>>());
            services.AddScoped<IRepository<Bio>>(provider => provider.GetRequiredService<Repository<Bio>>());
            services.AddScoped<IRepository<CombatAction>>(provider => provider.GetRequiredService<Repository<CombatAction>>());
            services.AddScoped<IRepository<CombatAttack>>(provider => provider.GetRequiredService<Repository<CombatAttack>>());
            services.AddScoped<IRepository<CombatDamage>>(provider => provider.GetRequiredService<Repository<CombatDamage>>());
            services.AddScoped<IRepository<CombatSavingThrow>>(provider => provider.GetRequiredService<Repository<CombatSavingThrow>>());
            services.AddScoped<IRepository<DndClass>>(provider => provider.GetRequiredService<Repository<DndClass>>());
            services.AddScoped<IRepository<Feat>>(provider => provider.GetRequiredService<Repository<Feat>>());
            services.AddScoped<IRepository<Item>>(provider => provider.GetRequiredService<Repository<Item>>());
            services.AddScoped<IRepository<Pc>>(provider => provider.GetRequiredService<Repository<Pc>>());
            services.AddScoped<IRepository<Proficiency>>(provider => provider.GetRequiredService<Repository<Proficiency>>());
            services.AddScoped<IRepository<Skill>>(provider => provider.GetRequiredService<Repository<Skill>>());
            services.AddScoped<IRepository<Spell>>(provider => provider.GetRequiredService<Repository<Spell>>());
            services.AddScoped<IRepository<SpellInfo>>(provider => provider.GetRequiredService<Repository<SpellInfo>>());
            services.AddScoped<IRepository<SpellLvlInfo>>(provider => provider.GetRequiredService<Repository<SpellLvlInfo>>());
            services.AddScoped<IRepository<Test>>(provider => provider.GetRequiredService<Repository<Test>>());

            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());


            //services.AddScoped<AppDbContextInitializer>();


            //services
            //    .AddDefaultIdentity<User>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>();

            //services.AddTransient<IIdentityService, IdentityService>();

            //services.AddAuthorization(options =>
            //    options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

            return services;
        }
    }
}