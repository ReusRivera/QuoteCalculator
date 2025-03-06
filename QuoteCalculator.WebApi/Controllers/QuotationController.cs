using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Domain.Models.ViewModels;
using QuoteCalculator.Services.FinanceService;
using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuotation _quotation;
        private readonly IFinance _finance;

        public QuotationController(IMapper mapper, IQuotation quotation, IFinance finance)
        {
            _mapper = mapper;
            _quotation = quotation;
            _finance = finance;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuote([FromQuery] Guid quotationId)
        {
            if (quotationId == Guid.Empty)
                return BadRequest("Invalid quotation ID.");

            var quotation = await _quotation.GetQuotationById(quotationId);

            if (quotation == null)
                return NotFound("Quotation record not found.");

            var result = _mapper.Map<QuotationViewModel>(quotation);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuote([FromBody] QuotationDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var quotationMap = _mapper.Map<QuotationModel>(model);

            if (!_quotation.IsQuotationValid(quotationMap))
                return BadRequest("Quotation details are invalid.");

            var quotation = await _quotation.ValidateQuotation(quotationMap);

            if (quotation == null)
                return NotFound("Quotation record not found.");

            var result = _mapper.Map<QuotationDto>(quotation);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CalculateQuote([FromBody] QuotationViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var quotationMap = _mapper.Map<QuotationModel>(viewModel);

            if (!_quotation.IsQuotationValid(quotationMap))
                return BadRequest("Quotation details are invalid.");

            var quotation = await _quotation.ValidateQuotation(quotationMap);

            if (quotation == null)
                return NotFound("Quotation record not found.");

            var financeMap = _mapper.Map<FinanceModel>(viewModel);

            if (!_finance.IsFinanceValid(financeMap))
                return BadRequest("Finance details are invalid.");

            var finance = await _finance.CreateFinance(financeMap);

            if (finance == null)
                return NotFound("Finance record not found.");

            var result = _mapper.Map<FinanceViewModel>(finance);

            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> EditQuote([FromBody] FinanceViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var quotationMap = _mapper.Map<QuotationModel>(viewModel);

        //    if (!_quotation.IsQuotationValid(quotationMap))
        //        return BadRequest("Quotation details are invalid.");

        //    var quotation = await _quotation.VerifyQuotation(quotationMap);

        //    if (quotation == null)
        //        return NotFound("Quotation record not found.");

        //    var result = _mapper.Map<QuotationViewModel>(quotation);

        //    return Ok(result);
        //}

        #region Mock Data
        //[HttpGet]
        //public async Task<IActionResult> GetMockQuote(/*string? financeId*/)
        //{
        //    //if (string.IsNullOrEmpty(financeId))
        //    //    return BadRequest("Invalid financeId.");

        //    var model = new QuotationViewModel();

        //    var quotation = await _quotation.CalculateQuotationMock(model);

        //    if (quotation == null)
        //        return NotFound("Quotation record not found.");

        //    return Ok(quotation);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CalculateMockQuote([FromBody] QuotationViewModel model)
        //{
        //    if (model == null)
        //        return BadRequest("Invalid quotation data.");

        //    var quotation = await _quotation.CalculateQuotationMock(model);

        //    if (quotation == null)
        //        return NotFound("Quotation record not found.");

        //    return Ok(quotation);
        //}
        #endregion
    }
}
