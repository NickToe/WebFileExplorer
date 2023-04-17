using System.Collections.Generic;
using System.Linq;
using WebFileExplorer.Enums;
using WebFileExplorer.Models;

namespace WebFileExplorer.Utility
{
    public static class GroupByExtension
    {
        public static IOrderedEnumerable<DirDto> DirOrderBy(this IEnumerable<DirDto> enumerable, SortOrder orderBy)
        {
            return orderBy == SortOrder.Descending ? enumerable.OrderByDescending(dir => dir.Size) : enumerable.OrderBy(dir => dir.Size);
        }

        public static IOrderedEnumerable<FileDto> FileOrderBy(this IEnumerable<FileDto> enumerable, SortOrder orderBy)
        {
            return orderBy == SortOrder.Descending ? enumerable.OrderByDescending(file => file.Size) : enumerable.OrderBy(file => file.Size);
        }
    }
}
