namespace Multi_Tensncy.Services
{
    public interface IProductServices
    {
        Task<Product> CreateAsync(Product product);
        Task<Product> GetById(string Id);
        Task<IReadOnlyList<Product>> GetAllAsync();
    }
}
