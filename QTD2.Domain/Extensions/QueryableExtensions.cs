using System;
using System.Linq;
using System.Linq.Expressions;

namespace QTD2.Domain.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector,
            bool isDescendingOrder)
        {
            return isDescendingOrder ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }
        
        public static IQueryable<TSource> ApplyPaging<TSource>(
            this IQueryable<TSource> query,
            int page,
            int pageSize)
        {
            return query.Skip((page) * pageSize)
                .Take(pageSize);
        }
    }
}