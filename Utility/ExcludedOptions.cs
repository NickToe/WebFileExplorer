using System;
using System.Collections.Generic;
using System.Linq;

namespace WebFileExplorer.Utility
{
    public record ExcludedOptions
    {
        public string ExcludedDrives { get; set; } = String.Empty;
        public string ExcludedRootDirs { get; set; } = String.Empty;

        public IEnumerable<string> GetExcludedDrives()
        {
            return ExcludedDrives?.Split(',', StringSplitOptions.TrimEntries) ?? Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetExcludedRootDirs()
        {
            return ExcludedRootDirs?.Split(',', StringSplitOptions.TrimEntries) ?? Enumerable.Empty<string>();
        }
    }
}
