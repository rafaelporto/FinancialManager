using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Identity.Jwt;
using FinancialManager.Infra.ValueObjects;

namespace FinancialManager.Identity
{
	public interface IAuthService
	{
		Task<Result<UserResponse>> Login(Email email, string password);
	}
}
