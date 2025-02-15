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

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto model)
        {
            //if (model == null)
            //    return BadRequest("Invalid quotation data.");

            //var quotation = await _quotation.CreateQuotation(model);

            //if (quotation == null)
            //    return StatusCode(500, "An error occurred while processing the request.");

            //return Ok(quotation);

            return Ok();
        }
    }
}
