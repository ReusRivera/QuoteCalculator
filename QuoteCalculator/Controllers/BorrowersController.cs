using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Context;
using QuoteCalculator.Models.Dto;

namespace QuoteCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        //private readonly IUser _user;

        public BorrowersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrowers()
        {
            try
            {
                var allBorrowers = await _context.Borrowers
                    .ToListAsync();

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
                var allBorrowers = await _context.Borrowers
                    .ToListAsync();

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
                var allBorrowers = await _context.Borrowers
                    .ToListAsync();

                return Ok(allBorrowers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
