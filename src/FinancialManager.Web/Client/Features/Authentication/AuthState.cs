using BlazorState;
using System.Collections.Generic;

namespace FinancialManager.Client.Features.Authentication
{
    public partial class AuthState : State<AuthState>
    {
        public string Email { get; private set; }
        public string Name { get; private set; }
        public List<string> Roles { get; private set; }

        public override void Initialize() => Roles = new List<string>();
    }
}
