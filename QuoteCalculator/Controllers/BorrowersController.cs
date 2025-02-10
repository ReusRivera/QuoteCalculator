using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Services.BorrowersService;

namespace QuoteCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        private readonly IBorrowers _borrowers;

        public BorrowersController(IBorrowers borrowers)
        {
            _borrowers = borrowers;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrowers()
        {
            try
            {
                var allBorrowers = await _borrowers.GetBorrowersList();

                return Ok(allBorrowers);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuotation(QuotationDto model)
        {
            try
            {
                var allBorrowers = await _borrowers.GetBorrowersList();

                return Ok(allBorrowers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBorrowRequest()
        {
            try
            {
                var allBorrowers = await _borrowers.GetBorrowersList();

                return Ok(allBorrowers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
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
