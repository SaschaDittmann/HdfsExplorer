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
                return _driveInfo.Name + "|Standard";
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
            return Directory.GetFiles(path)
                .Select(file => new DriveEntry
                    {
                        Key = file,
                        Name = file.Substring(file.LastIndexOf('\\') + 1)
                    }).ToList();
        }

        public List<DriveEntry> GetDirectories(string path)
        {
            return Directory.GetDirectories(path)
                .Select(directory => new DriveEntry
                    {
                        Key = directory,
                        Name = directory.Substring(directory.LastIndexOf('\\') + 1)
                    }).ToList();
        }
    }
}
