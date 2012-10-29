using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using HdfsExplorer.Drives;
using HdfsExplorer.Properties;

namespace HdfsExplorer
{
    public partial class MainForm : Form
    {
        private Dictionary<string, IDrive> _drives;
        private Dictionary<string, List<DriveEntry>> _driveEntryCache;
        private string _leftFileGridDirectoryKey;
        private string _leftFileGridDriveKey;
        private string _rightFileGridDirectoryKey;
        private string _rightFileGridDriveKey;
        private bool _moveDriveEntriesInClipboard;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            _drives = new Dictionary<string, IDrive>();
            _driveEntryCache = new Dictionary<string, List<DriveEntry>>();
            AddStandardDrives();
            AddHdfsDrives();
            RefreshDirectoryTrees();
            if (leftDirectoryTree.Nodes.Count > 0)
                leftDirectoryTree.SelectedNode = leftDirectoryTree.Nodes[0];
            if (rightDirectoryTree.Nodes.Count > 0)
                rightDirectoryTree.SelectedNode = rightDirectoryTree.Nodes[0];
        }

        private void MainFormLeave(object sender, EventArgs e)
        {
            foreach (var drive in _drives)
                drive.Value.Dispose();
        }

        private void RefreshDirectoryTrees()
        {
            leftDirectoryTree.Nodes.Clear();
            rightDirectoryTree.Nodes.Clear();
            foreach (var drive in _drives)
            {
                var node = leftDirectoryTree.Nodes.Add(drive.Key, drive.Value.Label);
                node.Nodes.Add(Resources.LoadingText);
                node = rightDirectoryTree.Nodes.Add(drive.Key, drive.Value.Label);
                node.Nodes.Add(Resources.LoadingText);
            }
        }

        private void AddStandardDrives()
        {
            foreach (var drive in DriveInfo.GetDrives().Select(drive => new StandardDrive(drive)))
            {
                _drives.Add(drive.Key, drive);
            }
        }

        private void AddHdfsDrives()
        {
            // Dummy Drives
            var hdfsDrive = new HdfsDrive("Local HDInsight Server", "localhost", 8020);
            _drives.Add(hdfsDrive.Key, hdfsDrive);
            hdfsDrive = new HdfsDrive("Dead Drive", "localhost", 9000);
            _drives.Add(hdfsDrive.Key, hdfsDrive);
        }

        private void DirectoryTreeBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.FirstNode == null || !String.IsNullOrEmpty(e.Node.FirstNode.Name))
                    return;

                RefreshDirectories(e.Node);
            }
            catch (Exception ex)
            {
                e.Node.Nodes.Clear();
                e.Node.Nodes.Add(
                    new TreeNode(Resources.ErrorPrefix + ex.Message)
                        {
                            ForeColor = Color.Red
                        });
            }
        }

        private void LeftDirectoryTreeBeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                var drive = GetDriveFromTreeNode(e.Node);
                if (drive == null) return;

                _leftFileGridDirectoryKey = e.Node.Name.Split('|')[0];
                _leftFileGridDriveKey = drive.Key;

                RefreshDriveEntries(drive, e.Node, leftFileGrid, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void LeftFileGridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(leftFileGrid.Rows[e.RowIndex].Cells[2].Value is DriveEntryType)) return;
            var type = (DriveEntryType)leftFileGrid.Rows[e.RowIndex].Cells[2].Value;
            if (type != DriveEntryType.Directory) return;

            var key = leftFileGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            var node = leftDirectoryTree.Nodes.Find(key, true);
            if (node.Length == 1)
                leftDirectoryTree.SelectedNode = node[0];
        }

        private void LeftFileGridKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (Convert.ToByte(e.KeyChar))
            {
                case 3:
                    // Copy
                    _moveDriveEntriesInClipboard = false;
                    AddDriveEntriesToClipboard(_leftFileGridDriveKey, leftFileGrid);
                    e.Handled = true;
                    break;
                case 24:
                    _moveDriveEntriesInClipboard = true;
                    AddDriveEntriesToClipboard(_leftFileGridDriveKey, leftFileGrid);
                    e.Handled = true;
                    break;
                case 22:
                    StartFileTransfer(_leftFileGridDriveKey, _leftFileGridDirectoryKey);
                    e.Handled = true;
                    break;
            }
        }

        private void RightDirectoryTreeAfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                var drive = GetDriveFromTreeNode(e.Node);
                if (drive == null) return;

                _rightFileGridDirectoryKey = e.Node.Name.Split('|')[0];
                _rightFileGridDriveKey = drive.Key;

                RefreshDriveEntries(drive, e.Node, rightFileGrid, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void RightFileGridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(rightFileGrid.Rows[e.RowIndex].Cells[2].Value is DriveEntryType)) return;
            var type = (DriveEntryType)rightFileGrid.Rows[e.RowIndex].Cells[2].Value;
            if (type != DriveEntryType.Directory) return;

            var key = rightFileGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            var node = rightDirectoryTree.Nodes.Find(key, true);
            if (node.Length == 1)
                rightDirectoryTree.SelectedNode = node[0];
        }

        private void RightFileGridKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (Convert.ToByte(e.KeyChar))
            {
                case 3:
                    // Copy
                    _moveDriveEntriesInClipboard = false;
                    AddDriveEntriesToClipboard(_rightFileGridDriveKey, rightFileGrid);
                    e.Handled = true;
                    break;
                case 24:
                    _moveDriveEntriesInClipboard = true;
                    AddDriveEntriesToClipboard(_rightFileGridDriveKey, rightFileGrid);
                    e.Handled = true;
                    break;
                case 22:
                    StartFileTransfer(_rightFileGridDriveKey, _rightFileGridDirectoryKey);
                    e.Handled = true;
                    break;
            }
        }

        private void DirectoryTreeKeyPress(object sender, KeyPressEventArgs e)
        {
            var treeView = sender as TreeView;
            if (treeView == null || treeView.SelectedNode == null) return;

            var drive = GetDriveFromTreeNode(treeView.SelectedNode);
            if (drive == null) return;

            switch (Convert.ToByte(e.KeyChar))
            {
                case 3:
                    // Copy
                    _moveDriveEntriesInClipboard = false;
                    AddDirectoryToClipboard(drive.Key, treeView);
                    e.Handled = true;
                    break;
                case 24:
                    _moveDriveEntriesInClipboard = true;
                    AddDirectoryToClipboard(drive.Key, treeView);
                    e.Handled = true;
                    break;
                case 22:
                    StartFileTransfer(drive.Key, treeView.SelectedNode.Name);
                    e.Handled = true;
                    break;
            }
            
        }

        private IDrive GetDriveFromTreeNode(TreeNode node)
        {
            var driveNode = node;
            while (driveNode.Level > 0)
                driveNode = driveNode.Parent;
            return _drives.ContainsKey(driveNode.Name) 
                ? _drives[driveNode.Name]
                : null;
        }

        private void RefreshDirectories(TreeNode node)
        {
            var drive = GetDriveFromTreeNode(node);
            if (drive == null) return;

            // Update Directories
            var directories = drive.GetDirectories(node.Name.Split('|')[0]);
            node.Nodes.Clear();
            foreach (var newNode in directories
                .Select(directory => node.Nodes.Add(directory.Key, directory.Name)))
            {
                newNode.Nodes.Add(Resources.LoadingText);
            }
        }

        private void RefreshDriveEntries(IDrive drive, TreeNode node, DataGridView grid, bool forceRefresh)
        {
            var driveEntryKey = node.Name.Split('|')[0];
            if (!_driveEntryCache.ContainsKey(driveEntryKey) || forceRefresh)
            {
                var driveEntries = drive.GetDriveEntries(driveEntryKey);
                if (driveEntries == null)
                {
                    grid.DataSource = null;
                    return;
                }
                _driveEntryCache.Add(driveEntryKey, driveEntries);
            }
            grid.DataSource = _driveEntryCache[driveEntryKey].ToList();    

            if (node.FirstNode == null || !String.IsNullOrEmpty(node.FirstNode.Name))
                return;

            node.Nodes.Clear();
            foreach (var newNode in _driveEntryCache[driveEntryKey]
                .Where(entry => entry.Type == DriveEntryType.Directory)
                .Select(directory => node.Nodes.Add(directory.Key, directory.Name)))
            {
                newNode.Nodes.Add(Resources.LoadingText);
            }
        }

        private static void AddDriveEntriesToClipboard(string driveKey, DataGridView grid)
        {
            if (grid.SelectedRows.Count == 0) return;

            var fileDropList = new StringCollection();
            fileDropList.AddRange((from DataGridViewRow row in grid.SelectedRows
                                   select row.Cells[0].Value.ToString()).ToArray());
            AddFileDropListToClipboard(driveKey, fileDropList);
        }

        private static void AddDirectoryToClipboard(string driveKey, TreeView treeView)
        {
            if (treeView.SelectedNode == null) return;

            AddFileDropListToClipboard(
                driveKey,
                new StringCollection {treeView.SelectedNode.Name.Split('|')[0]});
        }

        private static void AddFileDropListToClipboard(string driveKey, StringCollection fileDropList)
        {
            if (fileDropList[0].StartsWith("hdfs://"))
            {
                fileDropList.Insert(0, driveKey);
                Clipboard.SetData("HdfsFileTransfer", fileDropList);
            }
            else
                Clipboard.SetFileDropList(fileDropList);
        }

        private void StartFileTransfer(string targetDriveKey, string targetDirectoryKey)
        {
            string driveKey;
            StringCollection fileDropList;

            if (Clipboard.ContainsFileDropList())
            {
                fileDropList = Clipboard.GetFileDropList();
                if (fileDropList.Count==0) return;
                driveKey = Path.GetPathRoot(fileDropList[0]);
                if (String.IsNullOrEmpty(driveKey)) return;
            }
            else if (Clipboard.ContainsData("HdfsFileTransfer"))
            {
                var hdfsFileTransfer = Clipboard.GetData("HdfsFileTransfer") as StringCollection;
                if (hdfsFileTransfer == null || hdfsFileTransfer.Count<2) return;
                driveKey = hdfsFileTransfer[0];
                hdfsFileTransfer.RemoveAt(0);
                fileDropList = hdfsFileTransfer;
            }
            else 
                return;

            var form = new FileTransferForm(_drives[driveKey], fileDropList, 
                _drives[targetDriveKey], targetDirectoryKey);
            form.Closed += (s, e) =>
                {
                    _driveEntryCache.Remove(targetDirectoryKey);
                    if (_leftFileGridDirectoryKey == targetDirectoryKey
                        && leftDirectoryTree.SelectedNode != null)
                        RefreshDriveEntries(_drives[targetDriveKey],
                                            leftDirectoryTree.SelectedNode,
                                            leftFileGrid, true);
                    if (_rightFileGridDirectoryKey == targetDirectoryKey
                        && rightDirectoryTree.SelectedNode != null)
                        RefreshDriveEntries(_drives[targetDriveKey],
                                            rightDirectoryTree.SelectedNode,
                                            rightFileGrid, true);
                };
            form.Show();
        }
    }
}
