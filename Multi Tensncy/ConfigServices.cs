namespace Multi_Tensncy;

public static class ConfigServices
{
    public static IServiceCollection ApplyConfigServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        //Connection DataBase
        //builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        TenantSettings tenantSettings = new TenantSettings();
        configuration.GetSection(nameof(TenantSettings)).Bind(tenantSettings);
        services.AddSingleton(tenantSettings);


        //Debendency Injection
        services.AddHttpContextAccessor();
        services.AddScoped<ITenantServices, TenantServices>();


        //service configurations
        if (tenantSettings.Defaults.DatabaseProvider.ToLower().Equals("Sql Server".ToLower()))
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer());
        }

        foreach (var tenant in tenantSettings.Tenants)
        {
            var connectionString = tenant.ConnectionString ?? tenantSettings.Defaults.ConnectionString;
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(connectionString);

                if (dbContext.Database.GetPendingMigrations().Any())
                    dbContext.Database.Migrate();
            }
        }

        return services;
    }
}
