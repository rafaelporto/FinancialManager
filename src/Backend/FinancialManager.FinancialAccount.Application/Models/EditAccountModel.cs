using FinancialManager.FinancialAccounts.Domain;

namespace FinancialManager.FinancialAccounts.Application
{
    public record EditAccountModel
    {
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
    }
}
