using System.Linq;
using KtpAcs.Infrastructure.Search.Extensions;

namespace KtpAcs.Infrastructure.Search.Sort
{
    public class FieldSortCriteria<T> : ISortCriteria<T> where T : class
    {
        public FieldSortCriteria()
        {
            Direction = SortDirection.Ascending;
        }

        public FieldSortCriteria(string name, SortDirection direction)
        {
            Name = name;
            Direction = direction;
        }

        public string Name { get; set; }

        public SortDirection Direction { get; set; }

        public IOrderedQueryable<T> Apply(IQueryable<T> qry, bool useThenBy)
        {
            var isDescending = Direction == SortDirection.Descending;
            var result = !useThenBy ? qry.OrderBy(Name, isDescending) : qry.ThenBy(Name, isDescending);
            return result;
        }
    }
}