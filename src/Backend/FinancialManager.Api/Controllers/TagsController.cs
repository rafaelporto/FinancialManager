using FinancialManager.Application;
using FinancialManager.Core;
using FinancialManager.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Api.Controllers
{
    [Route("api/tags")]
    public class TagsController : ControllerBase
    {
        private readonly ITagAppService _service;

        public TagsController(ITagAppService service, IScopeControl scopeControl) : base(scopeControl)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateTagModel model, CancellationToken token = default)
        {
            await _service.Create(model, token);
            return ApiCreatedResponse();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<TagModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var tags = await _service.Get(token);
            return ApiResponse(tags);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<TagModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(Guid id, CancellationToken token = default)
        {
            var tag = await _service.Get(id, token);
            return ApiResponse(tag);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TagModel model, CancellationToken token = default)
        {
            await _service.Update(id, model, token);
            return ApiNoContentResponse();
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token = default)
        {
            var result = await _service.Delete(id, token);

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
