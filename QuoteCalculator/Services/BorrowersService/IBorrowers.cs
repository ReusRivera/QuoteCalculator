﻿using QuoteCalculator.Models;

namespace QuoteCalculator.Services.BorrowersService
{
    public interface IBorrowers
    {
        Task<List<BorrowerModel>> GetAllBorrowersList();
    }
}
