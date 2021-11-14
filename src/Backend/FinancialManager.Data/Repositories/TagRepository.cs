using FinancialManager.Core.Data;
using FinancialManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Data
{
    public class TagRepository : ITagRepository
    {
        private readonly FinancialManagerContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public TagRepository(FinancialManagerContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Tag tag) => _context.Tags.Add(tag);

        public void Dispose() => _context?.Dispose();

        public async Task<Tag> Get(Guid id, CancellationToken cancellationToken = default) =>
            await _context.Tags.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<IEnumerable<Tag>> GetList(CancellationToken cancellationToken = default) =>
            await _context.Tags.ToListAsync(cancellationToken);

        public async ValueTask<bool> Remove(Guid id)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(p => p.Id == id);

            if (tag is null)
                return false;

            tag.Delete();
            _context.Update(tag);
            return true;
        }

        public void Update(Tag tag) => _context.Tags.Update(tag);
    }
}
