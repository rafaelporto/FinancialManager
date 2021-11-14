using FinancialManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialManager.Application
{
    public static class TagMapper
    {
        public static Tag MapToTag(this TagModel model, Guid aspNetUserId) =>
            new(model.Id, model.Description, aspNetUserId);

        public static Tag MapToTag(this TagModel model, Guid id, Guid aspNetUserId) =>
            new(id, model.Description, aspNetUserId);

        public static Tag MapToTag(this CreateTagModel model, Guid aspNetUserId) =>
            new(model.Description, aspNetUserId);

        public static TagModel MapToTagModel(this Tag entity) =>
            new()
            {
                Id = entity.Id,
                Description = entity.Description
            };

        public static IEnumerable<TagModel> MapToTagModels(this IEnumerable<Tag> model) =>
            model.Select(s => s.MapToTagModel());

        public static IEnumerable<Tag> MapToTags(this IEnumerable<TagModel> model, Guid aspNetUserId) =>
            model.Select(s => s.MapToTag(aspNetUserId));
    }
}
