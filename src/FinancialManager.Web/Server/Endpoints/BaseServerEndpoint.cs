using Ardalis.ApiEndpoints;
using FinancialManager.Endpoints;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Server.Endpoints
{
    public abstract class BaseServerEndpoint<TRequest, TResponse> 
		: BaseAsyncEndpoint
	{
		public abstract Task<ApiResult<TResponse>> HandleAsync(TRequest request);
		protected void SetStatusCode(HttpStatusCode statusCode) => Response.StatusCode = (int)statusCode;
	}
}
