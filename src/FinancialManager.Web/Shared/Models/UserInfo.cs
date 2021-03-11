using System.Collections.Generic;

namespace FinancialManager.Shared.Models
{
    public class UserInfo
    {
        public static readonly UserInfo Anonymous = new UserInfo();
        public bool IsAuthenticated { get; set; }
        public string AccessToken { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
