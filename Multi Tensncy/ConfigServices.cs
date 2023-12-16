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
        services.AddScoped<ITenantServices, TenantServices>();


        //service configurations
        services.AddHttpContextAccessor();

        return services;
    }
}
