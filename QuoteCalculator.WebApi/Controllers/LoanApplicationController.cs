using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Services.LoanApplicationService;

namespace QuoteCalculator.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanApplication _loanApplication;

        public LoanApplicationController(ILoanApplication loanApplication)
        {
            _loanApplication = loanApplication;
        }

        //[HttpPost]
        //public async Task<IActionResult> ApplyLoan([FromBody] QuotationDto model)
        //{
        //    if (model == null)
        //        return BadRequest("Invalid quotation data.");

        //    var quotation = await _loanApplication.ValidateQuotation(model);

        //    if (quotation == null)
        //        return StatusCode(500, "An error occurred while processing the request.");

        //    return Ok(quotation);
        //}
    }
}
