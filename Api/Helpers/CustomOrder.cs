using System.Linq.Expressions;

namespace Api.Helpers
{
    public static class CustomOrder
    {
        public static IQueryable<T> CustomOrderBy<T>(this IQueryable queryable, string? propertyName, bool decend)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, propertyName!);
            var convert = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(convert, param);

            return decend ?
                queryable.OfType<T>().OrderByDescending(lambda) :
                queryable.OfType<T>().OrderBy(lambda); 
        }
    }
}
