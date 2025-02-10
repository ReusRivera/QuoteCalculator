﻿namespace QuoteCalculator.Domain.Models
{
    public class EmailDomainModel
    {
        public Guid Id { get; set; } = new Guid();
        public required string DomainName { get; set; }
        public bool IsAllowed { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
    }
}
