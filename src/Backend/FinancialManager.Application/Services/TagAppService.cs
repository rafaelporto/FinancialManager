using FinancialManager.Core;
using FinancialManager.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Application
{
    public class TagAppService : ITagAppService
    {
        private readonly ITagRepository _repository;
        private readonly IScopeControl _scopeControl;

        public TagAppService(ITagRepository tagRepository, IScopeControl scopeControl)
        {
            _repository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _scopeControl = scopeControl ?? throw new ArgumentNullException(nameof(scopeControl));
        }

        public async Task<bool> Create(CreateTagModel model, CancellationToken token = default)
        {
            var tag = model.MapToTag(_scopeControl.GetUserId());

            if (tag.IsInValid())
            {
                _scopeControl.AddNotifications(tag.Notifications);
                return false;
            }

            if (token.IsCancellationRequested)
                return false;

            _repository.Add(tag);
            return await _repository.UnitOfWork.Commit(token);
        }

        public async Task<bool> Delete(Guid id, CancellationToken token = default)
        {
            if (await _repository.Remove(id))
            {
                return await _repository.UnitOfWork.Commit(token);
            }

            return false;
        }

        public async Task<IEnumerable<TagModel>> Get(CancellationToken token = default)
        {
            var tags = await _repository.GetList(token);

            return tags.MapToTagModels();
        }

        public async Task<TagModel> Get(Guid id, CancellationToken token = default)
        {
            var tag = await _repository.Get(id, token);

            return tag.MapToTagModel();
        }

        public async Task<bool> Update(Guid id, TagModel model, CancellationToken token = default)
        {
            var tag = model.MapToTag(id, _scopeControl.GetUserId());

            if (tag.IsInValid())
            {
                _scopeControl.AddNotifications(tag.Notifications);
                return false;
            }

            _repository.Update(tag);
            return await _repository.UnitOfWork.Commit(token);
        }
    }
}
