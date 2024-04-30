using Domain.Constants;
using Infrastructure.Identity.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjectionExtentions
    {
        public static void AddOnlyOwnPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(Policies.OnlyOwnedPc, policy => policy.Requirements.Add(new OwnRecordRequirement<Pc>()));
            options.AddPolicy(Policies.OnlyOwnedBio, policy => policy.Requirements.Add(new OwnRecordRequirement<Bio>()));
            options.AddPolicy(Policies.OnlyOwnedCombatAction, policy => policy.Requirements.Add(new OwnRecordRequirement<CombatAction>()));
            options.AddPolicy(Policies.OnlyOwnedDndClass, policy => policy.Requirements.Add(new OwnRecordRequirement<DndClass>()));
            options.AddPolicy(Policies.OnlyOwnedFeat, policy => policy.Requirements.Add(new OwnRecordRequirement<Feat>()));
            options.AddPolicy(Policies.OnlyOwnedItem, policy => policy.Requirements.Add(new OwnRecordRequirement<Item>()));
            options.AddPolicy(Policies.OnlyOwnedMoney, policy => policy.Requirements.Add(new OwnRecordRequirement<Money>()));
            options.AddPolicy(Policies.OnlyOwnedNpc, policy => policy.Requirements.Add(new OwnRecordRequirement<Npc>()));
            options.AddPolicy(Policies.OnlyOwnedNpcAction, policy => policy.Requirements.Add(new OwnRecordRequirement<NpcAction>()));
            options.AddPolicy(Policies.OnlyOwnedNpcFeat, policy => policy.Requirements.Add(new OwnRecordRequirement<NpcFeat>()));
            options.AddPolicy(Policies.OnlyOwnedNpcProficiency, policy => policy.Requirements.Add(new OwnRecordRequirement<NpcProficiency>()));
            options.AddPolicy(Policies.OnlyOwnedProficiency, policy => policy.Requirements.Add(new OwnRecordRequirement<Proficiency>()));
            options.AddPolicy(Policies.OnlyOwnedSpell, policy => policy.Requirements.Add(new OwnRecordRequirement<Spell>()));
            options.AddPolicy(Policies.OnlyOwnedSpellInfo, policy => policy.Requirements.Add(new OwnRecordRequirement<SpellInfo>()));
            options.AddPolicy(Policies.OnlyOwnedSpellLvlInfo, policy => policy.Requirements.Add(new OwnRecordRequirement<SpellLvlInfo>()));
        }

        public static void AddOnlyOwnPolicyHandlers(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<Pc>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<Bio>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<CombatAction>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<DndClass>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<Feat>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<Item>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<Money>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<Npc>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<NpcAction>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<NpcFeat>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<NpcProficiency>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<Proficiency>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<Spell>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<SpellInfo>>();
            services.AddTransient<IAuthorizationHandler, OwnRecordHandler<SpellLvlInfo>>();
        }
    }
}
