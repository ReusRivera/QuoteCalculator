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
