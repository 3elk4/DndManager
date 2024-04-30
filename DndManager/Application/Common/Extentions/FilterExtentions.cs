using Application.Common.Interfaces;
using Domain.Common;
using System.Linq;

namespace Application.Common.Extentions
{
    public static class FilterExtentions
    {
        public static IQueryable<TDestination> ByUser<TDestination>(this IQueryable<TDestination> queryable, IUser user) where TDestination : BaseAuditableEntity
        {
            return queryable.Where(item => item.CreatedBy.Equals(user.Id));
        }
    }
}
