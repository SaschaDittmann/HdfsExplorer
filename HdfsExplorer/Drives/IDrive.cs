using System.Collections.Generic;
using System.ComponentModel;

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
        List<DriveEntry> GetDriveEntries(string path);

        BackgroundWorker GetFileTransferBackgroundWorker(string sourceFilePath, string targetPath);
        void DeleteFile(string file);
    }
}
