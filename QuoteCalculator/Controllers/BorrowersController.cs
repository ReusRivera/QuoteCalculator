using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Services.BorrowerService;
using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBorrower _borrower;
        private readonly IQuotation _quotation;

        public BorrowersController(IMapper mapper, IBorrower borrower, IQuotation quotation)
        {
            _mapper = mapper;
            _borrower = borrower;
            _quotation = quotation;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrowers()
        {
            var allBorrowers = await _borrower.GetBorrowersList();

            return Ok(allBorrowers);

            //return BadRequest($"Error: {ex.Message}");
        }

        //[HttpPost]
        //public async Task<IActionResult> GetQuotation(QuotationDto model)
        //{
        //    var allBorrowers = await _borrower.GetBorrowersList();

        //    return Ok(allBorrowers);
        //}

        //[HttpPost]
        //public async Task<IActionResult> GetQuotation([FromBody] QuotationDto model)
        //{
        //    var config = new MapperConfiguration(cfg =>
        //       cfg.CreateMap<QuotationDto, QuotationModel>()
        //    );

        //    var mapper = new Mapper(config);
        //    var quotation = mapper.Map<QuotationModel>(model);

        //    return Ok("allBorrowers");
        //}

        [HttpPost]
        public async Task<IActionResult> GetQuotation([FromBody] QuotationDto model)
        {
            var quotationMap = _mapper.Map<QuotationModel>(model);
            var borrowerMap = _mapper.Map<BorrowerModel>(model);

            var borrower = await _borrower.AddBorrower(borrowerMap);

            quotationMap.Borrower = borrower;

            var quotation = await _quotation.AddQuotation(quotationMap);

            return Ok(quotation);
        }

        //[HttpPost]
        //public async Task<IActionResult> ApplyBorrowerLoanRequest()
        //{
        //    try
        //    {
        //        var allBorrowers = await _borrower.GetBorrowersList();

        //        return Ok(allBorrowers);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}

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
