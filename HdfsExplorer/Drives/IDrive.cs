using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace HdfsExplorer.Drives
{
    public interface IDrive : IDisposable
    {
        string Key { get; }
        string Name { get; }
        string Label { get; }
        long AvailableFreeSpace { get; }
        long TotalSize { get; }

        List<DriveEntry> GetFiles(string path, bool includeSubdirectories);
        List<DriveEntry> GetDirectories(string path);
        List<DriveEntry> GetDriveEntries(string path);

        char PathDelimiter { get; }
        string GetFileName(string filePath);
        string CombinePath(params string[] paths);
        DriveEntryType GetDriveEntryType(string path);

        Stream OpenFileStreamForRead(string file);
        Stream OpenFileStreamForWrite(string file);
        Stream OpenFileStreamForAppend(string file);
        void CloseFileStream();
        void DisposeFileStream();

        void DeleteFile(string file);
    }
}
