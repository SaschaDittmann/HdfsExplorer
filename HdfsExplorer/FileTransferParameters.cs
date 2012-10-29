using HdfsExplorer.Drives;

namespace HdfsExplorer
{
    public class FileTransferParameters
    {
        public IDrive SourceDrive { get; set; }
        public string SourceFilePath { get; set; }
        public IDrive TargetDrive { get; set; }
        public string TargetFilePath { get; set; }
    }
}
