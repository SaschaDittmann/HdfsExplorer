using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using HdfsExplorer.Drives;
using HdfsExplorer.Properties;

namespace HdfsExplorer
{
    public partial class FileTransferForm : Form
    {
        private readonly IDrive _sourceDrive;
        private readonly StringCollection _sourcePaths;
        private readonly Queue<FileTransferParameters> _fileTransferQueue;
        private readonly IDrive _targetDrive;
        private readonly string _targetPath;
        private int _totalFileCount;

        public FileTransferForm(IDrive sourceDrive, StringCollection sourcePaths, IDrive targetDrive, string targetPath)
        {
            InitializeComponent();

            _sourceDrive = sourceDrive;
            _sourcePaths = sourcePaths;
            _targetDrive = targetDrive;
            _targetPath = targetPath;

            _fileTransferQueue = new Queue<FileTransferParameters>();
        }

        private void TransferFileFormLoad(object sender, EventArgs e)
        {
            try
            {
                statusStrip1.Text = Resources.InitFileTransferStatusMessage;
                InitFileTransferBackgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            if (FileTransferBackgroundWorker != null)
                FileTransferBackgroundWorker.CancelAsync();
        }

        private void InitFileTransferBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var path in _sourcePaths)
            {
                switch (_sourceDrive.GetDriveEntryType(path))
                {
                    case DriveEntryType.File:
                        _fileTransferQueue.Enqueue(new FileTransferParameters
                        {
                            SourceDrive = _sourceDrive,
                            SourceFilePath = path,
                            TargetDrive = _targetDrive,
                            TargetFilePath = _targetDrive.CombinePath(_targetPath, _sourceDrive.GetFileName(path))
                        });
                        break;
                    case DriveEntryType.Directory:
                        foreach (var file in _sourceDrive.GetFiles(path, true))
                            _fileTransferQueue.Enqueue(new FileTransferParameters
                            {
                                SourceDrive = _sourceDrive,
                                SourceFilePath = file.Key,
                                TargetDrive = _targetDrive,
                                TargetFilePath = _targetDrive.CombinePath(
                                    _targetPath,
                                    file.Key
                                        .Remove(0, path.LastIndexOf(_sourceDrive.PathDelimiter) + 1)
                                        .Replace(_sourceDrive.PathDelimiter,
                                                 _targetDrive.PathDelimiter))
                            });
                        break;
                }
            }
        }

        private void InitFileTransferBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message, Resources.ErrorCaption,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                    Close();
                }

                _totalFileCount = _fileTransferQueue.Count;

                CopyNextFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                Close();
            }
        }

        private void FileTransferBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var fileTransferParameters = e.Argument as FileTransferParameters;
            if (fileTransferParameters == null) return;

            try
            {
                if (fileTransferParameters.SourceFilePath == fileTransferParameters.TargetFilePath)
                    return;

                using (var source = fileTransferParameters.SourceDrive.OpenFileStreamForRead(
                    fileTransferParameters.SourceFilePath))
                {
                    if (source == null)
                        throw new FileLoadException();

                    var sourceLength = source.Length;

                    using (var target = fileTransferParameters.TargetDrive.OpenFileStreamForWrite(
                        fileTransferParameters.TargetFilePath))
                    {
                        while (source.Position < sourceLength)
                        {
                            var bufferSize =
                                8192 < sourceLength - source.Position
                                    ? 8192
                                    : Convert.ToInt32(sourceLength - source.Position);
                            var buffer = new byte[bufferSize];

                            source.Read(buffer, 0, bufferSize);
                            if (FileTransferBackgroundWorker.CancellationPending)
                            {
                                source.Close();
                                target.Close();
                                _targetDrive.DeleteFile(fileTransferParameters.TargetFilePath);
                                return;
                            }

                            target.Write(buffer, 0, buffer.Length);
                            if (FileTransferBackgroundWorker.CancellationPending)
                            {
                                source.Close();
                                target.Close();
                                _targetDrive.DeleteFile(fileTransferParameters.TargetFilePath);
                                return;
                            }

                            var progress = Convert.ToDouble(source.Position)/Convert.ToDouble(sourceLength)*100.0;
                            FileTransferBackgroundWorker.ReportProgress(Convert.ToInt32(progress));
                        }
                    }
                }
            }
            finally
            {
                fileTransferParameters.SourceDrive.DisposeFileStream();
                fileTransferParameters.TargetDrive.DisposeFileStream();
            }
        }

        private void FileTransferBackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FileTransferProgressBar.Value = e.ProgressPercentage;
        }

        private void FileTransferBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                Close();
                return;
            }

            if (_fileTransferQueue.Count > 0)
                CopyNextFile();
            else
                Close();
        }

        private void CopyNextFile()
        {
            if (_fileTransferQueue.Count == 0) return;

            var progress = Convert.ToDouble(_totalFileCount - _fileTransferQueue.Count) / Convert.ToDouble(_totalFileCount) * 100.0;
            TotalTransferProgressBar.Value = Convert.ToInt32(progress);
            var nextFileTransfers = _fileTransferQueue.Dequeue();
            SourceFilePath.Text = nextFileTransfers.SourceFilePath;
            TargetFilePath.Text = nextFileTransfers.TargetFilePath;
            FileTransferBackgroundWorker.RunWorkerAsync(nextFileTransfers);
        }
    }
}
