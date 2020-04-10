using System.Linq;

namespace KtpAcs.Infrastructure.Search.Sort
{
    public interface ISortCriteria<T>
    {
        SortDirection Direction { get; set; }
        IOrderedQueryable<T> Apply(IQueryable<T> query, bool useThenBy);
    }
}