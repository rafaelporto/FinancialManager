using FinancialManager.FinancialAccounts.Domain;
using Newtonsoft.Json;

namespace FinancialManager.FinancialAccounts.Application
{
    public record RegisterAccountModel
    {
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
    }
}
