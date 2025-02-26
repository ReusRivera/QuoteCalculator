using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models.ViewModels;
using QuoteCalculator.Services.FinanceService;

namespace QuoteCalculator.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFinance _finance;

        public FinanceController(IMapper mapper, IFinance finance)
        {
            _mapper = mapper;
            _finance = finance;
        }

        [HttpGet]
        public async Task<IActionResult> GetFinance([FromQuery] Guid financeId)
        {
            if (financeId == Guid.Empty)
                return BadRequest("Invalid finance ID.");

            var finance = await _finance.GetFinanceById(financeId);

            if (finance == null)
                return NotFound("Finance record not found.");

            var result = _mapper.Map<FinanceViewModel>(finance);

            return Ok(result);
        }
    }
}
