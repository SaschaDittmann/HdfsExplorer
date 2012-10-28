using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace HdfsExplorer.Drives
{
    public class StandardDrive : IDrive
    {
        private readonly DriveInfo _driveInfo;
        public StandardDrive(DriveInfo driveInfo)
        {
            _driveInfo = driveInfo;
        }

        public string Key
        {
            get
            {
                return _driveInfo.Name;
            }
        }

        public string Name
        {
            get
            {
                return _driveInfo.Name;
            }
        }

        public string Label
        {
            get 
            {
                if (_driveInfo.IsReady && !String.IsNullOrEmpty(_driveInfo.VolumeLabel))
                    return String.Format("{0} [{1}]", _driveInfo.Name, _driveInfo.VolumeLabel);
                    
                return _driveInfo.Name;
            }
        }

        public long AvailableFreeSpace
        {
            get { return _driveInfo.IsReady ? _driveInfo.AvailableFreeSpace : -1; }
        }

        public long TotalSize
        {
            get { return _driveInfo.IsReady ? _driveInfo.TotalSize : -1; }
        }

        public List<DriveEntry> GetFiles(string path)
        {
            if (!_driveInfo.IsReady) return null;

            return (from file in Directory.GetFiles(path)
                    let info = new FileInfo(file)
                    select new DriveEntry
                        {
                            Key = file,
                            Name = file.Substring(file.LastIndexOf('\\') + 1), 
                            Type = DriveEntryType.File, 
                            Size = info.Length,
                            LastAccessed = info.LastAccessTime,
                            LastModified = info.LastWriteTime,
                        }).ToList();
        }

        public List<DriveEntry> GetDirectories(string path)
        {
            if (!_driveInfo.IsReady) return null;

            return (from directory in Directory.GetDirectories(path)
                    let info = new DirectoryInfo(directory)
                    select new DriveEntry
                        {
                            Key = directory,
                            Name = directory.Substring(directory.LastIndexOf('\\') + 1),
                            Type = DriveEntryType.Directory,
                            Size = 0,
                            LastAccessed = info.LastAccessTime,
                            LastModified = info.LastWriteTime,
                        }).ToList();
        }

        public List<DriveEntry> GetDriveEntries(string path)
        {
            var driveEntries = new List<DriveEntry>(GetDirectories(path));
            driveEntries.AddRange(GetFiles(path));
            return driveEntries;
        }

        public BackgroundWorker GetFileTransferBackgroundWorker(string sourceFilePath, string targetFilePath)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string file)
        {
            File.Delete(file);
        }
    }
}
