using Microsoft.AspNetCore.Mvc;
using petBack.DTOS;
using petBack.DTOS.Mappings;
using petBack.Repositories.Interfaces;

namespace petBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IUnitOfWork uof, ILogger<ProductsController> logger)
        {
            _uof = uof;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _uof.ProductRepository.GetAllAsync();

            if (products == null || !products.Any())
            {
                _logger.LogWarning("Nenhum produto encontrado.");
                return NotFound("Nenhum produto encontrado.");
            }

            return Ok(products.ToProductDTOList());
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

            var resultDto = createdProduct.ToProductDTO();

            return CreatedAtRoute("ObterProduct", new { id = resultDto.ProductId }, resultDto);
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
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

            return Ok(existingProduct.ToProductDTO());
        }





        /// <summary>
        /// Remove um produto.
        /// </summary>
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

            return Ok(existingProduct.ToProductDTO());
        }
    }
}
