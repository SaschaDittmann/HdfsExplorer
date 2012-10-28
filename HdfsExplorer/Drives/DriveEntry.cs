using System;

namespace HdfsExplorer.Drives
{
    public class DriveEntry
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public DriveEntryType Type { get; set; }
        public long Size { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime LastModified { get; set; }

        public string SizeText
        {
            get
            {
                if (Type == DriveEntryType.Directory) 
                    return "";
                if (Size >= 1099511627776)
                    return String.Format("{0} TB", Size / 1099511627776);
                if (Size >= 1073741824)
                    return String.Format("{0} GB", Size / 1073741824);
                if (Size >= 1048576)
                    return String.Format("{0} MB", Size / 1048576);
                if (Size >= 1024)
                    return String.Format("{0} KB", Size / 1024);
                return String.Format("{0} Bytes", Size);
            }
        }
    }
}
