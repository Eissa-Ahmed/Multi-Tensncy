namespace Multi_Tensncy.Services;

public interface ITenantServices
{
    string? GetDatabaseProvider();
    string? GetConnectionString();
    Tenant? GetCurrentTenant();
}
