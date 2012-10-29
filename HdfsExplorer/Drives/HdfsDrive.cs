using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Hdfs;

namespace HdfsExplorer.Drives
{
    public class HdfsDrive : IDrive
    {
        private readonly string _name;
        private readonly string _host;
        private readonly ushort _port;
        private readonly string _user;

        public HdfsDrive(string name, string host, ushort port)
        {
            _name = name;
            _host = host;
            _port = port;
            _user = null;
        }

        public HdfsDrive(string name, string host, ushort port, string user)
        {
            _name = name;
            _host = host;
            _port = port;
            _user = user;
        }

        public string Key
        {
            get { return String.Format("hdfs://{0}:{1}/|{2}", _host, _port, _user); }
        }

        public string Name
        {
            get
            {
                return String.Format("{0}:{1}", _host, _port);
            }
        }

        public string Label
        {
            get
            {
                return String.Format("{0} [{1}:{2}]", _name, _host, _port);
            }
        }
        
        public long AvailableFreeSpace
        {
            get
            {
                using (var fileSystem = GetHdfsFileSystemConnection())
                {
                    return fileSystem.IsValid() ? fileSystem.GetCapacity() - fileSystem.GetUsed() : -1;
                }
            }
        }

        public long TotalSize
        {
            get
            {
                using (var fileSystem = GetHdfsFileSystemConnection())
                {
                    return fileSystem.IsValid() ? fileSystem.GetCapacity() : -1;
                }
            }
        }

        public List<DriveEntry> GetFiles(string path, bool includeSubdirectories)
        {
            using (var fileSystem = GetHdfsFileSystemConnection())
            {
                if (!fileSystem.IsValid())
                    return null;

                return GetFiles(fileSystem, path, includeSubdirectories);
            }
        }

        private List<DriveEntry> GetFiles(HdfsFileSystem fileSystem, string path, bool includeSubdirectories)
        {
            var files = new List<DriveEntry>();
            using (var entries = fileSystem.ListDirectory(path))
            {
                if (entries.Entries != null)
                {
                    files.AddRange(
                        entries.Entries
                            .Where(e => e.Kind == HdfsFileInfoEntryKind.File)
                            .Select(GetDriveEntryFromHdfsFileInfoEntry));

                    if (includeSubdirectories)
                    {
                        foreach (var directory in entries.Entries
                            .Where(e => e.Kind == HdfsFileInfoEntryKind.Directory))
                        {
                            files.AddRange(GetFiles(fileSystem, directory.Name, true));
                        }
                    }
                }
            }
            return files;
        }

        public List<DriveEntry> GetDirectories(string path)
        {
            using (var fileSystem = GetHdfsFileSystemConnection())
            {
                if (!fileSystem.IsValid())
                    return null;

                var directories = new List<DriveEntry>();
                using (var entries = fileSystem.ListDirectory(path))
                {
                    if (entries.Entries != null)
                        directories.AddRange(
                            entries.Entries
                                .Where(e => e.Kind == HdfsFileInfoEntryKind.Directory)
                                .Select(GetDriveEntryFromHdfsFileInfoEntry));
                }
                return directories;
            }
        }

        public List<DriveEntry> GetDriveEntries(string path)
        {
            using (var fileSystem = GetHdfsFileSystemConnection())
            {
                if (!fileSystem.IsValid())
                    return null;

                var driveEntries = new List<DriveEntry>();
                using (var entries = fileSystem.ListDirectory(path))
                {
                    if (entries.Entries!=null)
                        driveEntries.AddRange(
                            entries.Entries
                                .Select(GetDriveEntryFromHdfsFileInfoEntry));
                }
                return driveEntries;
            }
        }

        public char PathDelimiter { get { return '/'; } }

        public string GetFileName(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf(PathDelimiter) + 1);
        }

        public string CombinePath(params string[] paths)
        {
            if (paths.Length == 0) return null;

            var sb = new StringBuilder();
            foreach (var path in paths)
            {
                if (sb.Length > 0)
                    sb.Append("/");
                sb.Append(path.Trim(PathDelimiter));
            }

            return sb.ToString();
        }

        public DriveEntryType GetDriveEntryType(string path)
        {
            using (var fileSystem = GetHdfsFileSystemConnection())
            {
                if (!fileSystem.IsValid()) return DriveEntryType.Unknown;
                using (var info = fileSystem.GetPathInfo(path))
                {
                    return info.Kind == HdfsFileInfoEntryKind.File
                               ? DriveEntryType.File
                               : DriveEntryType.Directory;
                }
            }
        }

        public void DeleteFile(string file)
        {
            using (var fileSystem = GetHdfsFileSystemConnection())
            {
                if (!fileSystem.IsValid() || !fileSystem.FileExists(file)) return;
                fileSystem.DeleteFile(file);
            }
        }

        public void DeleteDirectory(string path)
        {
            using (var fileSystem = GetHdfsFileSystemConnection())
            {
                if (!fileSystem.IsValid()) return;
                fileSystem.DeleteFile(path);
            }
        }

        private DriveEntry GetDriveEntryFromHdfsFileInfoEntry(HdfsFileInfoEntry entry)
        {
            return new DriveEntry
                {
                    Key = entry.Name,
                    Name = entry.Name.Substring(entry.Name.LastIndexOf(PathDelimiter) + 1),
                    Type = entry.Kind == HdfsFileInfoEntryKind.File
                               ? DriveEntryType.File
                               : DriveEntryType.Directory,
                    Size = entry.Size,
                    LastAccessed = entry.LastAccessed,
                    LastModified = entry.LastModified,
                };
        }

        private HdfsFileSystem _fileSystem;
        private HdfsFileStream _fileStream;

        public Stream OpenFileStreamForRead(string file)
        {
            _fileSystem = GetHdfsFileSystemConnection();
            if (!_fileSystem.IsValid()) return null;

            _fileStream = _fileSystem.OpenFileStream(file, HdfsFileAccess.Read);
            return _fileStream.IsValid() ? _fileStream : null;
        }

        public Stream OpenFileStreamForWrite(string file)
        {
            _fileSystem = GetHdfsFileSystemConnection();
            if (!_fileSystem.IsValid()) return null;

            _fileStream = _fileSystem.OpenFileStream(file, HdfsFileAccess.Write);
            return _fileStream.IsValid() ? _fileStream : null;
        }

        public Stream OpenFileStreamForAppend(string file)
        {
            _fileSystem = GetHdfsFileSystemConnection();
            if (!_fileSystem.IsValid()) return null;

            _fileStream = _fileSystem.OpenFileStream(file, HdfsFileAccess.Append);
            return _fileStream.IsValid() ? _fileStream : null;
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
            //if (_fileStream != null)
            //{
            //    _fileStream.Dispose();
            //    _fileStream = null;
            //}

            if (_fileSystem != null)
            {
                _fileSystem.Dispose();
                _fileSystem = null;
            }
        }

        private HdfsFileSystem GetHdfsFileSystemConnection()
        {
            return String.IsNullOrEmpty(_user)
                       ? HdfsFileSystem.Connect(_host, _port)
                       : HdfsFileSystem.Connect(_host, _port, _user);
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

        ~HdfsDrive()
        {
            Dispose(false);
        }
    }
}