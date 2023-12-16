namespace Multi_Tensncy.Contrct
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
