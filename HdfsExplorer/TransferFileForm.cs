using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HdfsExplorer.Drives;

namespace HdfsExplorer
{
    public partial class TransferFileForm : Form
    {
        private readonly IDrive _sourceDrive;
        private readonly StringCollection _sourceFilePaths;
        private readonly IDrive _targetDrive;
        private readonly string _targetPath;
        private BackgroundWorker _backgroundWorker;

        public TransferFileForm(IDrive sourceDrive, StringCollection sourceFilePaths, IDrive targetDrive, string targetPath)
        {
            InitializeComponent();
            _sourceDrive = sourceDrive;
            _sourceFilePaths = sourceFilePaths;
            _targetDrive = targetDrive;
            _targetPath = targetPath;
        }

        private void TransferFileFormLoad(object sender, EventArgs e)
        {
            CopyNextFile();
        }

        void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                Close();
                return;
            }

            _sourceFilePaths.RemoveAt(0);
            if (_sourceFilePaths.Count > 0)
                CopyNextFile();
            else
                Close();
        }

        private void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FileTransferProgressBar.Value = e.ProgressPercentage;
        }

        private void CopyNextFile()
        {
            if (_sourceFilePaths.Count == 0) return;

            SourceFilePath.Text = _sourceFilePaths[0];
            TargetFilePath.Text = _targetPath;

            if (_sourceFilePaths[0].StartsWith("hdfs://") && _sourceDrive != null)
            {
                _backgroundWorker = _sourceDrive.GetFileTransferBackgroundWorker(
                    _sourceFilePaths[0], 
                    _targetPath);
            }
            else
            {

            }

            if (_backgroundWorker == null) return;
            _backgroundWorker.ProgressChanged += BackgroundWorkerProgressChanged;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
            _backgroundWorker.RunWorkerAsync();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            if (_backgroundWorker != null)
                _backgroundWorker.CancelAsync();
        }
    }
}
