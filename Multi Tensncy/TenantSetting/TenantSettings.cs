namespace Multi_Tensncy.TenantSetting
{
    public class TenantSettings // Included Database Information With Tenants
    {
        public Configuration Defaults { get; set; } = default!;
        public List<Tenant> Tenants { get; set; } = new();
    }
}
