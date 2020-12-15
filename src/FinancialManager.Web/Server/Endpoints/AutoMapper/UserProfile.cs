using AutoMapper;
using FinancialManager.Identity;
using FinancialManager.Identity.Jwt;
using FinancialManager.Web.Shared.Endpoints;
using FinancialManager.Web.Shared.Models;

namespace FinancialManager.Web.Server.Endpoints.AutoMapper
{
	internal class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<UserClaim, Claim>()
				.ConvertUsing(source => new Claim(source.Type, source.Value));
		}
	}
}
