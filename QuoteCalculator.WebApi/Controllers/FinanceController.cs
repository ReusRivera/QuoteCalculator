using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models.Dto;

namespace QuoteCalculator.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        public FinanceController()
        {
            
        }

        //[HttpPost]
        //public async Task<IActionResult> CalculateQuote([FromBody] QuotationDto model)
        //{
        //    if (model == null)
        //        return BadRequest("Invalid quotation data.");

        //    var quotation = await _quotation.CreateQuotation(model);

        //    if (quotation == null)
        //        return StatusCode(500, "An error occurred while processing the request.");

        //    return Ok(quotation);
        //}
    }
}
