namespace Multi_Tensncy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _services;
        public ProductController(IProductServices services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product product)
        {
            var result = await _services.CreateAsync(product);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _services.GetAllAsync();
            return Ok(result);
        }
    }
}
