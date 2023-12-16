namespace Multi_Tensncy.Services;

public class TenantServices : ITenantServices
{
    private Tenant? tenant;
    private readonly TenantSettings tenantSettings;
    private HttpContext httpContext;
    public TenantServices(IHttpContextAccessor httpContextAccessor, TenantSettings tenantSettings)
    {
        httpContext = httpContextAccessor.HttpContext;
        this.tenantSettings = tenantSettings;

        if (httpContext is null)
            throw new Exception("httpContext Is Null");

        tenant = tenantSettings.Tenants.FirstOrDefault(i => i.Id.Equals(GetTenantIdFromHeader()));

        if (string.IsNullOrEmpty(tenant?.ConnectionString))
            tenant!.ConnectionString = tenantSettings.Defaults.ConnectionString;

    }
    public string? GetConnectionString()
    {
        return tenant.ConnectionString;

    }

    public Tenant? GetCurrentTenant()
    {
        return tenant;
    }

    public string? GetDatabaseProvider()
    {
        return tenantSettings.Defaults.DatabaseProvider;
    }

    private string? GetTenantIdFromHeader()
    {
        bool TenantIdIsExist = httpContext.Request.Headers.TryGetValue("tenantId", out var TenantId);
        if (!TenantIdIsExist)
            return null;

        return TenantId;
    }
}
