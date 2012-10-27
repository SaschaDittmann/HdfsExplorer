using System.Collections.Generic;

namespace HdfsExplorer.Drives
{
    public interface IDrive
    {
        string Key { get; }
        string Name { get; }
        string Label { get; }
        long AvailableFreeSpace { get; }
        long TotalSize { get; }

        List<DriveEntry> GetFiles(string path);
        List<DriveEntry> GetDirectories(string path);
    }
}
