using WebFileExplorer.Enums;

namespace WebFileExplorer.Utility
{
    public static class SizeConverter
    {
        public static long GetSizeInUnit(long size, UnitOfInformation unit)
        {
            return size >> (10 * (int)unit);
        }
    }
}