using Microsoft.AspNetCore.Mvc;
using petBack.DTOS;
using petBack.DTOS.Mappings;
using petBack.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;


namespace petBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMemoryCache _cache;

        public ProductsController(IUnitOfWork uof, ILogger<ProductsController> logger, IMemoryCache cache)
        {
            _uof = uof;
            _logger = logger;
            _cache = cache;
        }


        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            const string cacheKey = "all_products";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProductDTO>? productsDto))
            {
                var products = await _uof.ProductRepository.GetAllAsync();

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("Nenhum produto encontrado.");
                    return NotFound("Nenhum produto encontrado.");
                }

                productsDto = products.ToProductDTOList();

                // Define tempo de expiração do cache (2 horas)
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2),
                    Priority = CacheItemPriority.High
                };

                _cache.Set(cacheKey, productsDto, cacheOptions);
                _logger.LogInformation("Cache criado para todos os produtos (2 horas).");
            }
            else
            {
                _logger.LogInformation("Retornando produtos do cache.");
            }

            return Ok(productsDto);
        }



        /// <summary>
        /// Obtém um produto pelo ID.
        /// </summary>
        [HttpGet("{id:guid}", Name = "ObterProduct")]
        public async Task<ActionResult<ProductDTO>> Get(Guid id)
        {
            var product = await _uof.ProductRepository.GetAsync(p => p.ProductId == id);

            if (product == null)
            {
                _logger.LogWarning($"Produto com ID = {id} não encontrado.");
                return NotFound($"Produto com ID = {id} não encontrado.");
            }

            return Ok(product.ToProductDTO());
        }



        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Dados inválidos para criação de produto.");
                return BadRequest(ModelState);
            }

            var product = productDto.ToProduct();
            var createdProduct = _uof.ProductRepository.Create(product);
            await _uof.Commit();
            _cache.Remove("all_products"); // limpa cache da lista geral


            var resultDto = createdProduct.ToProductDTO();

            return CreatedAtRoute("ObterProduct", new { id = resultDto.ProductId }, resultDto);
        }

        /// <summary>
        /// Atualiza um produto existente. --> Authorize
        /// </summary>
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> Put(Guid id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                _logger.LogWarning("ID do produto não confere.");
                return BadRequest("ID do produto não confere.");
            }

            var existingProduct = await _uof.ProductRepository.GetAsync(p => p.ProductId == id);
            if (existingProduct == null)
            {
                _logger.LogWarning($"Produto com ID = {id} não encontrado para atualização.");
                return NotFound($"Produto com ID = {id} não encontrado.");
            }

            // Atualiza manualmente as propriedades
            existingProduct.Title = productDto.Title;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.ImagemUrl = productDto.ImagemUrl;

            _uof.ProductRepository.Update(existingProduct);

            await _uof.Commit();
            _cache.Remove("all_products"); // limpa cache da lista geral


            return Ok(existingProduct.ToProductDTO());
        }





        /// <summary>
        /// Remove um produto. --> Authorize
        /// </summary>
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> Delete(Guid id)
        {
            var existingProduct = await _uof.ProductRepository.GetAsync(p => p.ProductId == id);

            if (existingProduct == null)
            {
                _logger.LogWarning($"Produto com ID = {id} não encontrado para exclusão.");
                return NotFound($"Produto com ID = {id} não encontrado.");
            }

            _uof.ProductRepository.Delete(existingProduct);

            await _uof.Commit();
            _cache.Remove("all_products"); // limpa cache da lista geral


            return Ok(existingProduct.ToProductDTO());
        }
    }
}
