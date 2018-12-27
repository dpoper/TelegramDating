using System.Linq;
using TelegramDating.Model;

namespace TelegramDating.Database
{
    public static class DatabaseExt
    {
        public static bool Contains(this IQueryable<Entity> entitiesQueryable, Entity entity)
        {
            return entitiesQueryable.Any(eq => eq.Id == entity.Id);
        }

    }
}
