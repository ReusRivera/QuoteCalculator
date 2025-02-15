using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotation _quotation;

        public QuotationController(IQuotation quotation)
        {
            _quotation = quotation;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuote([FromBody] QuotationDto model)
        {
            if (model == null)
                return BadRequest("Invalid quotation data.");

            var quotation = await _quotation.CreateQuotation(model);

            if (quotation == null)
                return StatusCode(500, "An error occurred while processing the request.");

            return Ok(quotation);
        }
    }
}
