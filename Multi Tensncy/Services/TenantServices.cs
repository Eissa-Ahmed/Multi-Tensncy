namespace Multi_Tensncy.Services;

public class TenantServices : ITenantServices
{
    private Tenant? tenant;
    private readonly TenantSettings tenantSettings;
    private IHttpContextAccessor _httpContextAccessor;
    public TenantServices(IHttpContextAccessor httpContextAccessor, TenantSettings tenantSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        this.tenantSettings = tenantSettings;

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
        /*if (_httpContextAccessor.HttpContext is null)
            return null;*/

        /*bool TenantIdIsExist = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("tenantId", out var TenantId);
        if (!TenantIdIsExist)
            return null;*/

        return "Id2";
    }
}
