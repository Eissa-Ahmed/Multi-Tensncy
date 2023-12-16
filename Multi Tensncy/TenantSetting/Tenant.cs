namespace Multi_Tensncy.TenantSetting;

public class Tenant // Tenant Information
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? ConnectionString { get; set; }
}
