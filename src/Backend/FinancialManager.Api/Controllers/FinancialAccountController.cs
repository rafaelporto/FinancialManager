using FinancialManager.Core;
using FinancialManager.Core.Extensions;
using FinancialManager.FinancialAccounts.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Api.Controllers
{
    [Authorize]
    public class FinancialAccountController : ControllerBase
    {
        private readonly IAccountAppService _service;

        public FinancialAccountController(IAccountAppService service,
            IScopeControl scopeControl) : base(scopeControl) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(RegisterAccountModel model, CancellationToken token = default)
        {
            await _service.CreateAccount(model, token);
            return ApiCreatedResponse();
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<AccountModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var accounts = await _service.GetAccounts(token);
            return ApiResponse(accounts);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<AccountModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(Guid id, CancellationToken token = default)
        {
            var account = await _service.GetAccount(id, token);
            return ApiResponse(account);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EditAccountModel model, CancellationToken token = default)
        {
            await _service.UpdateAccount(id, model, token);
            return ApiNoContentResponse();
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token = default)
        {
            var result = await _service.DeleteAccount(id, token);

            if (result)
                return ApiNoContentResponse();

            if (ScopeControl.HasNotification)
            {
                var notifications = ScopeControl.Notifications.MapToDictionary();
                return BadRequest(ApiResult.Failure(notifications));
            }

            return NotFound();
        }
    }
}
