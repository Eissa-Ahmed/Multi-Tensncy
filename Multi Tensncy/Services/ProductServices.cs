
namespace Multi_Tensncy.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext _context;
        public ProductServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<Product> GetById(string Id)
        {
            return await _context.products.FirstOrDefaultAsync(i => i.Id.Equals(Id));
        }
    }
}
