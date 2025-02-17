﻿namespace QuoteCalculator.Domain.Models.ViewModels
{
    public class FinanceViewModel
    {
        public Guid Id { get; set; }
        public decimal FinanceAmount { get; set; }
        public string? RepaymentSchedule { get; set; }
        public QuotationModel? Quotation { get; set; }
        public ProductModel? Product { get; set; }

        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
    }
}
