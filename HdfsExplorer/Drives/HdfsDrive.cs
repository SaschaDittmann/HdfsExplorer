using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public List<DriveEntry> GetFiles(string path)
        {
            using (var fileSystem = GetHdfsFileSystemConnection())
            {
                if (!fileSystem.IsValid())
                    return null;

                var files = new List<DriveEntry>();
                using(var entries = fileSystem.ListDirectory(path))
                {
                    if (entries.Entries != null)
                        files.AddRange(
                            entries.Entries
                                .Where(e => e.Kind == HdfsFileInfoEntryKind.File)
                                .Select(GetDriveEntryFromHdfsFileInfoEntry));
                }
                return files;
            }
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

        public Stream GetFileStream(string file)
        {
            using (var fileSystem = GetHdfsFileSystemConnection())
            {
                if (!fileSystem.IsValid() || !fileSystem.FileExists(file)) return null;
                return fileSystem.OpenFileStream(file, HdfsFileAccess.Read);
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

        private static DriveEntry GetDriveEntryFromHdfsFileInfoEntry(HdfsFileInfoEntry entry)
        {
            return new DriveEntry
                {
                    Key = entry.Name,
                    Name = entry.Name.Substring(entry.Name.LastIndexOf('/') + 1),
                    Type = entry.Kind == HdfsFileInfoEntryKind.File
                               ? DriveEntryType.File
                               : DriveEntryType.Directory,
                    Size = entry.Size,
                    LastAccessed = entry.LastAccessed,
                    LastModified = entry.LastModified,
                };
        }

        private HdfsFileSystem GetHdfsFileSystemConnection()
        {
            return String.IsNullOrEmpty(_user)
                       ? HdfsFileSystem.Connect(_host, _port)
                       : HdfsFileSystem.Connect(_host, _port, _user);
        }
    }
}