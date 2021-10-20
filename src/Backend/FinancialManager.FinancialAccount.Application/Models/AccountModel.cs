using FinancialManager.FinancialAccounts.Domain;
using System;

namespace FinancialManager.FinancialAccounts.Application
{
    public record AccountModel
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
        public Guid OwnerId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? LastUpdated { get; set; }
    }
    public record RegisterAccountModel
    {
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
    }
    public record EditAccountModel
    {
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
    }
}
