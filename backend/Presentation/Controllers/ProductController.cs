using Application.Services;
using Core.DTOs;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IProductService _productService;

        public ProductController(IAuthService authService, IProductService productService)
        {
            _authService = authService;
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous] 
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }



        [HttpGet("{ProductId}")]
        [AllowAnonymous]
        public async Task<Product> GetProduct(string ProductId)
        {
            return await _productService.GetProduct(ProductId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest(new { message = "The ProductDTO field is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = await _productService.CreateProduct(productDTO.Name, productDTO.Description, productDTO.Price);
                return CreatedAtAction(nameof(GetProduct), new { productId = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{ProductId}/UploadImage")]
        public async Task<ActionResult<string>> UploadImage(string ProductId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No image found");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileData = new FileData
            {
                FileName = file.FileName,
                Content = memoryStream.ToArray(),
                ContentType = file.ContentType,
                Extension = Path.GetExtension(file.FileName),
            };

            string imageUrl = await _productService.UploadProductImage(ProductId, fileData);

            return CreatedAtAction(nameof(UploadImage), imageUrl);
        }
    }
}
