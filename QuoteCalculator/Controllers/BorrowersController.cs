using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpGet]
        //public async Task<IActionResult> GetAllBorrowers()
        //{
        //    var allBorrowers = await _borrower.GetBorrowersList();

        //    return Ok(allBorrowers);

        //    //return BadRequest($"Error: {ex.Message}");
        //}

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
    }
}
