﻿namespace QuoteCalculator.Domain.Models
{
    public class FinanceModel
    {
        public Guid Id { get; set; } = new Guid();
        public decimal FinanceAmount { get; set; }
        public required string RepaymentSchedule { get; set; }
        public required QuotationModel Quotation { get; set; }
        public required ProductModel Product { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
