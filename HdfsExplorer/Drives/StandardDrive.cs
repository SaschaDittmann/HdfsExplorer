using System;
using System.Collections.Generic;
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

        public List<DriveEntry> GetFiles(string path, bool includeSubdirectories)
        {
            if (!_driveInfo.IsReady) return null;

            var files = includeSubdirectories
                            ? Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                            : Directory.GetFiles(path);

            return (from file in files
                    let info = new FileInfo(file)
                    select new DriveEntry
                        {
                            Key = file,
                            Name = file.Substring(file.LastIndexOf(PathDelimiter) + 1), 
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
                            Name = directory.Substring(directory.LastIndexOf(PathDelimiter) + 1),
                            Type = DriveEntryType.Directory,
                            Size = 0,
                            LastAccessed = info.LastAccessTime,
                            LastModified = info.LastWriteTime,
                        }).ToList();
        }

        public List<DriveEntry> GetDriveEntries(string path)
        {
            var driveEntries = new List<DriveEntry>(GetDirectories(path));
            driveEntries.AddRange(GetFiles(path, false));
            return driveEntries;
        }

        public char PathDelimiter { get { return '\\'; } }

        public string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public DriveEntryType GetDriveEntryType(string path)
        {
            if (File.Exists(path))
                return DriveEntryType.File;
            else if (Directory.Exists(path))
                return DriveEntryType.Directory;
            return DriveEntryType.Unknown;
        }

        private FileStream _fileStream;

        public Stream OpenFileStreamForRead(string file)
        {
            _fileStream = File.Open(file, FileMode.Open, FileAccess.Read);
            return _fileStream;
        }

        public Stream OpenFileStreamForWrite(string file)
        {
            if (String.IsNullOrEmpty(file)) return null;
            var directory = Path.GetDirectoryName(file);
            if (String.IsNullOrEmpty(directory)) return null;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            _fileStream = File.Open(file, FileMode.Create, FileAccess.Write);
            return _fileStream;
        }

        public Stream OpenFileStreamForAppend(string file)
        {
            if (String.IsNullOrEmpty(file)) return null;
            var directory = Path.GetDirectoryName(file);
            if (String.IsNullOrEmpty(directory)) return null;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            _fileStream = File.Open(file, FileMode.Append, FileAccess.Write);
            return _fileStream;
        }

        public void CloseFileStream()
        {
            if (_fileStream != null)
            {
                if (_fileStream.CanWrite)
                    _fileStream.Flush();
                _fileStream.Close();
            }
        }

        public void DisposeFileStream()
        {
            if (_fileStream != null)
            {
                CloseFileStream();
                _fileStream.Dispose();
                _fileStream = null;
            }
        }

        public void DeleteFile(string file)
        {
            File.Delete(file);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DisposeFileStream();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~StandardDrive()
        {
            Dispose(false);
        }
    }
}
