#nullable enable
using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model
{
    public class PagedResult<TData> : Result<IEnumerable<TData>>
    {
        public PagedResult() : this(null, 0, 0)
        {
        }

        public PagedResult(IEnumerable<TData>? data, int totalItems, int pageSize)
        {
            Data = data;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }

        public int TotalItems { get; }
        public int TotalPages { get; }

        public static PagedResult<TData> CreateSuccess(IEnumerable<TData>? items, int totalItems, int pageSize) =>
            new(items, totalItems, pageSize);

        public new static PagedResult<TData> CreateError(string error) => 
            new(null, 0, 0) { Error = error };
    }
}
