using Application.Common.Interfaces;
using Application.Common.Interfaces.Authorization;
using Ardalis.GuardClauses;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Identity;
using Infrastructure.PDF;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();


            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
                options.ConfigureWarnings(w => w.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            });

            services.AddScoped<IDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            services
                .AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthorization(options =>
            {
                options.AddOnlyOwnPolicies();
            });

            services.AddOnlyOwnPolicyHandlers();

            services.AddTransient<IPdfService, PdfService>();

            return services;
        }
    }
}