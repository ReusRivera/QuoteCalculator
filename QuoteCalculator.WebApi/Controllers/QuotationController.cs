using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Domain.Models.ViewModels;
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

        [HttpPost]
        public async Task<IActionResult> CalculateQuote([FromBody] QuotationViewModel model)
        {
            if (model == null)
                return BadRequest("Invalid quotation data.");

            var quotation = await _quotation.CalculateQuotation(model);

            if (quotation == null)
                return StatusCode(500, "An error occurred while processing the request.");

            return Ok(quotation);
        }

        #region Mock Data
        [HttpGet]
        public async Task<IActionResult> GetMockQuote(/*string? financeId*/)
        {
            //if (string.IsNullOrEmpty(financeId))
            //    return BadRequest("Invalid financeId.");

            var model = new QuotationViewModel();

            var quotation = await _quotation.CalculateMockQuotation(model);

            if (quotation == null)
                return StatusCode(500, "An error occurred while processing the request.");

            return Ok(quotation);
        }

        [HttpPost]
        public async Task<IActionResult> CalculateMockQuote([FromBody] QuotationViewModel model)
        {
            if (model == null)
                return BadRequest("Invalid quotation data.");

            var quotation = await _quotation.CalculateMockQuotation(model);

            if (quotation == null)
                return StatusCode(500, "An error occurred while processing the request.");

            return Ok(quotation);
        }
        #endregion
    }
}
