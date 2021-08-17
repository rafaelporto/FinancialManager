using BlazorState;
using FinancialManager.Client.Features.Authentication;

namespace FinancialManager.Web.Client.Pages
{
    public partial class Index
    {
        AuthState AuthState => GetState<AuthState>();
    }
}
