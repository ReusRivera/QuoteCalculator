using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.Controllers
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

            var quotation = await _quotation.CreateQuotationAsync(model);

            if (quotation == null)
                return StatusCode(500, "An error occurred while processing the request.");

            return Ok(quotation);
        }

        [HttpPost]
        public async Task<IActionResult> CalculateQuote([FromBody] QuotationDto model)
        {
            if (model == null)
                return BadRequest("Invalid quotation data.");

            var quotation = await _quotation.CreateQuotationAsync(model);

            if (quotation == null)
                return StatusCode(500, "An error occurred while processing the request.");

            return Ok(quotation);
        }

        //public HttpResponseMessage Post()
        //{
        //    // ... do the job

        //    // now redirect
        //    var response = Request.CreateResponse(HttpStatusCode.Moved);
        //    response.Headers.Location = new Uri("http://www.abcmvc.com");
        //    return response;
        //}
    }
}
