using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Identity.Jwt;

namespace FinancialManager.Identity
{
	public interface IAuthService
	{
		Task<Result<UserResponse>> Login(string email, string password);
	}
}
