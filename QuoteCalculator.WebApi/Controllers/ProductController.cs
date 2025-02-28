using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Services.ProductService;

namespace QuoteCalculator.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var productList = await _product.GetAllProductList();

                return Ok(productList);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMockProductList() // Mock Product list for research and scientific purposes.
        {
            var productList = await _product.GetAllMockProductList();

            return Ok(productList);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var quotation = await _quotation.ValidateQuotation(model);

            //if (quotation == null)
            //    return StatusCode(500, "An error occurred while processing the request.");

            //return Ok(quotation);

            return Ok();
        }
    }
}
