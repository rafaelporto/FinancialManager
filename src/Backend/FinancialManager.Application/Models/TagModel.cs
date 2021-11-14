using System;

namespace FinancialManager.Application
{
    public record TagModel : CreateTagModel
    {
        public Guid Id { get; set; }
    }

    public record CreateTagModel  
    {
        public string Description { get; set; }
    }
}
