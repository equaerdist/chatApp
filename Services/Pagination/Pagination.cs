using System.Linq.Expressions;

namespace WebApplication5.Services.Pagination 
{
    public class Pagination<TInput>
    {
        public static Expression<Func<TInput, object>> CreateKeySelector(string? sortTerm)
        {
            var type = typeof(TInput);
            var x = Expression.Parameter(type);
            var property = Expression.Property(x, sortTerm ?? "Id");
            var conversion = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TInput, object>>(conversion, x);
        }
    }
}