using FinancialManager.Shared.Models;
using Refit;
using System.Threading.Tasks;

namespace FinancialManager.Endpoints.Authorization
{
    public interface IAuth
    {
        [Post("/auth/login")]
        Task<ApiResult<LoginResponse>> Login(LoginRequest loginRequest);

        [Get("/auth/user")]
        Task<UserInfo> User();
    }
}
