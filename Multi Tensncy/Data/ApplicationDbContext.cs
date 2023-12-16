namespace Multi_Tensncy.Data;

public class ApplicationDbContext : DbContext
{
    private string TenantId { get; set; }
    private readonly ITenantServices _tenantServices;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option, ITenantServices tenantServices) : base(option)
    {
        _tenantServices = tenantServices;
        TenantId = _tenantServices.GetCurrentTenant()!.Id;
    }

    public DbSet<Product> products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(e => e.TenantId.Equals(TenantId));
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_tenantServices.GetConnectionString() is not null)
            if (_tenantServices.GetDatabaseProvider()!.ToLower().Equals("Sql Server".ToLower()))
                optionsBuilder.UseSqlServer(_tenantServices.GetConnectionString());

        base.OnConfiguring(optionsBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // هيتم تفعيلها ع الجدول اللى بيرث من IMustHaveTenant وبشرط انه يكون بيضيف داتا
        foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().Where(e => e.State == EntityState.Added))
        {
            entry.Entity.TenantId = TenantId;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
