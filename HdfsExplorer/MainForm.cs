using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HdfsExplorer.Drives;
using HdfsExplorer.Properties;

namespace HdfsExplorer
{
    public partial class MainForm : Form
    {
        private const string HdfsServerFile = "HdfsServers.xml";
        private const string ClipboardDataType = "HdfsExplorerFileDropList";
        private const string MoveModeToken = "*MOVE_MODE*";
        private Dictionary<string, IDrive> _drives;
        private Dictionary<string, List<DriveEntry>> _driveEntryCache;
        private string _leftFileGridDirectoryKey;
        private string _leftFileGridDriveKey;
        private string _rightFileGridDirectoryKey;
        private string _rightFileGridDriveKey;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Form Events

        private void MainFormLoad(object sender, EventArgs e)
        {
            try
            {
                _drives = new Dictionary<string, IDrive>();
                _driveEntryCache = new Dictionary<string, List<DriveEntry>>();
                AddStandardDrives();
                LoadHdfsDrives();
                InitDirectoryTrees();
                if (leftDirectoryTree.Nodes.Count > 0)
                    leftDirectoryTree.SelectedNode = leftDirectoryTree.Nodes[0];
                if (rightDirectoryTree.Nodes.Count > 0)
                    rightDirectoryTree.SelectedNode = rightDirectoryTree.Nodes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void MainFormLeave(object sender, EventArgs e)
        {
            try
            {
                foreach (var drive in _drives)
                    drive.Value.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        #region Directory Tree

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

        private void DirectoryTreeKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                var treeView = sender as TreeView;
                if (treeView == null || treeView.SelectedNode == null) return;

                var drive = GetDriveFromTreeNode(treeView.SelectedNode);
                if (drive == null) return;

                switch (Convert.ToByte(e.KeyChar))
                {
                    case 3:
                        AddDirectoryToClipboard(drive.Key, treeView, false);
                        e.Handled = true;
                        break;
                    case 24:
                        AddDirectoryToClipboard(drive.Key, treeView, true);
                        e.Handled = true;
                        break;
                    case 22:
                        StartFileTransfer(drive.Key, treeView.SelectedNode.Name);
                        e.Handled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void DirectoryTreeKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var treeView = sender as TreeView;
                if (treeView == null || treeView.SelectedNode == null) return;

                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (treeView.SelectedNode.Level == 0) return;
                        DeleteDirectoryFromTree(treeView.SelectedNode);
                        break;
                    case Keys.F2:
                        if (treeView.SelectedNode.Level == 0) return;
                        RenameDirectoryFromTree(treeView.SelectedNode);
                        break;
                    case Keys.F5:
                        var drive = GetDriveFromTreeNode(treeView.SelectedNode);
                        RefreshDriveEntries(drive.Key, treeView.SelectedNode.Name);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        #region File Grid

        private void LeftFileGridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(leftFileGrid.Rows[e.RowIndex].Cells[2].Value is DriveEntryType)) return;
                var type = (DriveEntryType)leftFileGrid.Rows[e.RowIndex].Cells[2].Value;
                if (type != DriveEntryType.Directory) return;

                var key = leftFileGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                var node = leftDirectoryTree.Nodes.Find(key, true);
                if (node.Length == 1)
                    leftDirectoryTree.SelectedNode = node[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void LeftFileGridKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (Convert.ToByte(e.KeyChar))
            {
                case 3:
                    AddDriveEntriesToClipboard(_leftFileGridDriveKey, leftFileGrid, false);
                    e.Handled = true;
                    break;
                case 24:
                    AddDriveEntriesToClipboard(_leftFileGridDriveKey, leftFileGrid, true);
                    e.Handled = true;
                    break;
                case 22:
                    StartFileTransfer(_leftFileGridDriveKey, _leftFileGridDirectoryKey);
                    e.Handled = true;
                    break;
            }
        }

        private void LeftFileGridKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        DeleteFilesAndFoldersFromFileGrid(leftFileGrid,
                            _leftFileGridDriveKey, _leftFileGridDirectoryKey);
                        break;
                    case Keys.F2:
                        RenameFileOrFolderFromFileGrid(leftFileGrid,
                            _leftFileGridDriveKey, _leftFileGridDirectoryKey);
                        break;
                    case Keys.F5:
                        RefreshDriveEntries(_leftFileGridDriveKey,
                            _leftFileGridDirectoryKey);
                        break;
                }
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
            try
            {
                switch (Convert.ToByte(e.KeyChar))
                {
                    case 3:
                        // Copy
                        AddDriveEntriesToClipboard(_rightFileGridDriveKey, rightFileGrid, false);
                        e.Handled = true;
                        break;
                    case 24:
                        AddDriveEntriesToClipboard(_rightFileGridDriveKey, rightFileGrid, true);
                        e.Handled = true;
                        break;
                    case 22:
                        StartFileTransfer(_rightFileGridDriveKey, _rightFileGridDirectoryKey);
                        e.Handled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void RightFileGridKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        DeleteFilesAndFoldersFromFileGrid(rightFileGrid,
                            _rightFileGridDriveKey, _rightFileGridDirectoryKey);
                        break;
                    case Keys.F2:
                        RenameFileOrFolderFromFileGrid(rightFileGrid,
                            _rightFileGridDriveKey, _rightFileGridDirectoryKey);
                        break;
                    case Keys.F5:
                        RefreshDriveEntries(_rightFileGridDriveKey,
                            _rightFileGridDirectoryKey);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        #region Main ToolStrip

        private void AddHdfsServerButtonClick(object sender, EventArgs e)
        {
            try
            {
                AddHdfsServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void EditHdfsServerButtonClick(object sender, EventArgs e)
        {
            try
            {
                var selectionForm = new HdfsDriveSelectionForm(_drives
                    .Select(d => d.Value)
                    .OfType<HdfsDrive>()
                    .ToList());
                if (selectionForm.ShowDialog() != DialogResult.OK) return;

                EditHdfsServer(selectionForm.SelectedDrive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void RemoveHdfsServerButtonClick(object sender, EventArgs e)
        {
            try
            {
                var selectionForm = new HdfsDriveSelectionForm(_drives
                    .Select(d => d.Value)
                    .OfType<HdfsDrive>()
                    .ToList())
                {
                    FormCaption = Resources.RemoveHdfsServerCaption,
                    SelectButtonText = Resources.RemoveButtonText
                };
                if (selectionForm.ShowDialog() != DialogResult.OK) return;

                RemoveHdfsServer(selectionForm.SelectedDrive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        #region DirectoryTree ContextMenu

        private void DirectoryTreeContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var contextMenu = sender as ContextMenuStrip;
                if (contextMenu == null)
                {
                    e.Cancel = true;
                    return;
                }
                var treeView = contextMenu.SourceControl as TreeView;
                if (treeView == null || treeView.SelectedNode == null)
                {
                    e.Cancel = true;
                    return;
                }

                editHdfsServerTreeMenuItem.Enabled =
                    treeView.SelectedNode.Level == 0
                    && _drives[treeView.SelectedNode.Name] is HdfsDrive;
                removeHdfsServerTreeMenuItem.Enabled =
                    treeView.SelectedNode.Level == 0
                    && _drives[treeView.SelectedNode.Name] is HdfsDrive;
                renameFolderTreeMenuItem.Enabled = treeView.SelectedNode.Level > 0;
                deleteFolderTreeMenuItem.Enabled = treeView.SelectedNode.Level > 0;
                copyTreeMenuItem.Enabled = treeView.SelectedNode.Level > 0;
                cutTreeMenuItem.Enabled = treeView.SelectedNode.Level > 0;
                pasteTreeMenuItem.Enabled = Clipboard.ContainsFileDropList()
                                            || Clipboard.ContainsData(ClipboardDataType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void AddHdfsServerTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                AddHdfsServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void EditHdfsServerTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;
                var drive = _drives[treeView.SelectedNode.Name] as HdfsDrive;
                if (drive == null) return;
                EditHdfsServer(drive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void RemoveHdfsServerTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;
                var drive = _drives[treeView.SelectedNode.Name] as HdfsDrive;
                if (drive == null) return;
                RemoveHdfsServer(drive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void NewFolderTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;

                var form = new DriveEntryNameForm
                    {
                        FormCaption = Resources.NewFolderFormCaption,
                        DriveEntryName = "",
                        SaveButtonText = Resources.NewFolderFormSaveButtonText
                    };
                if (form.ShowDialog() != DialogResult.OK) return;

                var drive = GetDriveFromTreeNode(treeView.SelectedNode);
                var newFolder = drive.CombinePath(
                    treeView.SelectedNode.Name.Split('|')[0], 
                    form.DriveEntryName);
                drive.CreateDirectory(newFolder);
                RefreshDriveEntries(drive.Key, treeView.SelectedNode.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void RenameFolderTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;

                RenameDirectoryFromTree(treeView.SelectedNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void DeleteFolderTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;

                DeleteDirectoryFromTree(treeView.SelectedNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void CopyTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;

                var drive = GetDriveFromTreeNode(treeView.SelectedNode);
                AddDirectoryToClipboard(drive.Key, treeView, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void CutTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;

                var drive = GetDriveFromTreeNode(treeView.SelectedNode);
                AddDirectoryToClipboard(drive.Key, treeView, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void PasteTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;

                var drive = GetDriveFromTreeNode(treeView.SelectedNode);
                StartFileTransfer(drive.Key, treeView.SelectedNode.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void RefreshTreeMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var treeView = GetTreeViewFromTreeMenuItem(sender);
                if (treeView == null || treeView.SelectedNode == null) return;

                var drive = GetDriveFromTreeNode(treeView.SelectedNode);
                RefreshDriveEntries(drive.Key, treeView.SelectedNode.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        #region Private Methods

        private void AddStandardDrives()
        {
            foreach (var drive in DriveInfo.GetDrives().Select(drive => new StandardDrive(drive)))
            {
                _drives.Add(drive.Key, drive);
            }
        }

        private void LoadHdfsDrives()
        {
            try
            {
                if (!File.Exists(HdfsServerFile)) return;
                var servers = XDocument.Load(HdfsServerFile);
                if (servers.Root == null) return;

                foreach (var server in servers.Root.Elements("HdfsServer"))
                {
                    var nameAttribute = server.Attribute("name");
                    var hostAttribute = server.Attribute("host");
                    var portAttribute = server.Attribute("port");
                    var userAttribute = server.Attribute("user");
                    ushort port;

                    if (nameAttribute == null || hostAttribute == null || portAttribute == null
                        || !ushort.TryParse(portAttribute.Value, out port))
                        continue;

                    var drive = userAttribute == null
                                    ? new HdfsDrive(nameAttribute.Value, hostAttribute.Value, port)
                                    : new HdfsDrive(nameAttribute.Value, hostAttribute.Value, port, userAttribute.Value);
                    _drives.Add(drive.Key, drive);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void StoreHdfsDrives()
        {
            using (var fileStream = File.Create(HdfsServerFile))
            {
                using (var writer = XmlWriter.Create(fileStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("HdfsServers");

                    foreach (var drive in _drives
                        .Select(drive => drive.Value)
                        .OfType<HdfsDrive>()
                        .OrderBy(d => d.Label))
                    {
                        writer.WriteStartElement("HdfsServer");
                        writer.WriteAttributeString("name", drive.Name);
                        writer.WriteAttributeString("host", drive.Host);
                        writer.WriteAttributeString("port", drive.Port.ToString(CultureInfo.InvariantCulture));
                        if (!String.IsNullOrEmpty(drive.User))
                            writer.WriteAttributeString("user", drive.User);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                }
                fileStream.Close();
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

        private void InitDirectoryTrees()
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
                _driveEntryCache[driveEntryKey] = driveEntries;
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

        private void RefreshDriveEntries(string driveKey, string directoryKey)
        {
            if (_driveEntryCache.ContainsKey(directoryKey))
                _driveEntryCache.Remove(directoryKey);
            if (_leftFileGridDirectoryKey == directoryKey
                && leftDirectoryTree.SelectedNode != null)
                RefreshDriveEntries(_drives[driveKey],
                                    leftDirectoryTree.SelectedNode,
                                    leftFileGrid, true);
            if (_rightFileGridDirectoryKey == directoryKey
                && rightDirectoryTree.SelectedNode != null)
                RefreshDriveEntries(_drives[driveKey],
                                    rightDirectoryTree.SelectedNode,
                                    rightFileGrid, true);
            var leftNodes = leftDirectoryTree.Nodes.Find(directoryKey, true);
            if (leftNodes.Length == 1)
            {
                leftDirectoryTree.SelectedNode = leftNodes[0];
                RefreshDirectories(leftNodes[0]);
            }
            var rightNodes = rightDirectoryTree.Nodes.Find(directoryKey, true);
            if (rightNodes.Length == 1)
            {
                rightDirectoryTree.SelectedNode = rightNodes[0];
                RefreshDirectories(rightNodes[0]);
            }
        }

        private static TreeView GetTreeViewFromTreeMenuItem(object sender)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return null;
            var contextMenu = menuItem.GetCurrentParent() as ContextMenuStrip;
            if (contextMenu == null) return null;
            return contextMenu.SourceControl as TreeView;
        }

        private void AddDriveEntriesToClipboard(string driveKey, DataGridView grid, bool cut)
        {
            if (grid.SelectedRows.Count == 0) return;

            var fileDropList = new StringCollection();
            fileDropList.AddRange((from DataGridViewRow row in grid.SelectedRows
                                   select row.Cells[0].Value.ToString()).ToArray());
            mainStatusLabel.Text =
                String.Format(Resources.ItemsAddedToClipboardMessage, fileDropList.Count);
            AddFileDropListToClipboard(driveKey, fileDropList, cut);
        }

        private void AddDirectoryToClipboard(string driveKey, TreeView treeView, bool cut)
        {
            if (treeView.SelectedNode == null) return;

            var folder = treeView.SelectedNode.Name.Split('|')[0];
            AddFileDropListToClipboard(driveKey, new StringCollection {folder}, cut);
            mainStatusLabel.Text =
                String.Format(Resources.FolderAddedToClipboardMessage, folder);
        }

        private static void AddFileDropListToClipboard(string driveKey, StringCollection fileDropList, bool cut)
        {
            if (cut || fileDropList[0].StartsWith("hdfs://"))
            {
                if (cut)
                    fileDropList.Insert(0, MoveModeToken);
                fileDropList.Insert(0, driveKey);
                Clipboard.SetData(ClipboardDataType, fileDropList);
            }
            else
                Clipboard.SetFileDropList(fileDropList);
        }

        private void StartFileTransfer(string targetDriveKey, string targetDirectoryKey)
        {
            string driveKey;
            StringCollection fileDropList;
            var moveMode = false;

            if (Clipboard.ContainsFileDropList())
            {
                fileDropList = Clipboard.GetFileDropList();
                if (fileDropList.Count==0) return;
                driveKey = Path.GetPathRoot(fileDropList[0]);
                if (String.IsNullOrEmpty(driveKey)) return;
            }
            else if (Clipboard.ContainsData(ClipboardDataType))
            {
                var hdfsFileTransfer = Clipboard.GetData(ClipboardDataType) as StringCollection;
                if (hdfsFileTransfer == null || hdfsFileTransfer.Count < 2) return;
                driveKey = hdfsFileTransfer[0];
                hdfsFileTransfer.RemoveAt(0);
                if (hdfsFileTransfer[0] == MoveModeToken)
                {
                    moveMode = true;
                    hdfsFileTransfer.RemoveAt(0);
                }
                fileDropList = hdfsFileTransfer;
            }
            else 
                return;

            var sourceDrive = _drives[driveKey];
            var form = new FileTransferForm(sourceDrive, fileDropList,
                _drives[targetDriveKey], targetDirectoryKey, moveMode);
            form.Closed += (s, e) =>
                {
                    if (moveMode && fileDropList.Count>0)
                    {
                        var sourceDirectoryKey = fileDropList[0]
                            .Substring(0, fileDropList[0].LastIndexOf(sourceDrive.PathDelimiter));
                        RefreshDriveEntries(driveKey, sourceDirectoryKey);
                    }
                    RefreshDriveEntries(targetDriveKey, targetDirectoryKey);
                };
            form.Show();
        }

        private void RenameDirectoryFromTree(TreeNode selectedNode)
        {
            var drive = GetDriveFromTreeNode(selectedNode);
            var currentPath = selectedNode.Name;
            var form = new DriveEntryNameForm
            {
                FormCaption = Resources.RenameFolderFormCaption,
                DriveEntryName = currentPath.Substring(currentPath.LastIndexOf(drive.PathDelimiter) + 1),
                SaveButtonText = Resources.RenameFolderFormSaveButtonText
            };
            if (form.ShowDialog() != DialogResult.OK) return;

            var newPath = drive.CombinePath(
                currentPath.Substring(0, currentPath.LastIndexOf(drive.PathDelimiter) + 1),
                form.DriveEntryName);
            drive.RenameDirectory(currentPath, newPath);
            RefreshDriveEntries(drive.Key, selectedNode.Parent.Name);
        }

        private void DeleteDirectoryFromTree(TreeNode selectedNode)
        {
            if (MessageBox.Show(
                String.Format(Resources.DeleteDirectoryQuestion, selectedNode.Name),
                Resources.DeleteDirectoryCaption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;

            var drive = GetDriveFromTreeNode(selectedNode);
            if (drive == null) return;

            drive.DeleteDirectory(selectedNode.Name);
            RefreshDriveEntries(drive.Key, selectedNode.Parent.Name);
        }

        private void RenameFileOrFolderFromFileGrid(DataGridView grid, string driveKey, string directoryKey)
        {
            if (grid == null || grid.SelectedRows.Count == 0) return;
            var drive = _drives[driveKey];
            if (drive == null) return;
            var form = new DriveEntryNameForm();
            foreach (DataGridViewRow row in grid.SelectedRows)
            {
                var currentPath = row.Cells[0].Value.ToString();
                switch (drive.GetDriveEntryType(currentPath))
                {
                    case DriveEntryType.Directory:
                        form.FormCaption = Resources.RenameFolderFormCaption;
                        form.DriveEntryName = currentPath.Substring(currentPath.LastIndexOf(drive.PathDelimiter) + 1);
                        form.SaveButtonText = Resources.RenameFolderFormSaveButtonText;
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            var newPath = drive.CombinePath(
                                currentPath.Substring(0, currentPath.LastIndexOf(drive.PathDelimiter) + 1),
                                form.DriveEntryName);
                            drive.RenameDirectory(currentPath, newPath);
                        }
                        break;
                    case DriveEntryType.File:
                        form.FormCaption = Resources.RenameFileFormCaption;
                        form.DriveEntryName = currentPath.Substring(currentPath.LastIndexOf(drive.PathDelimiter) + 1);
                        form.SaveButtonText = Resources.RenameFileFormSaveButtonText;
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            var newPath = drive.CombinePath(
                                currentPath.Substring(0, currentPath.LastIndexOf(drive.PathDelimiter) + 1),
                                form.DriveEntryName);
                            drive.RenameFile(currentPath, newPath);
                        }
                        break;
                }
            }
            RefreshDriveEntries(driveKey, directoryKey);
        }

        private void DeleteFilesAndFoldersFromFileGrid(DataGridView grid, string driveKey, string directoryKey)
        {
            if (grid == null || grid.SelectedRows.Count == 0) return;
            var drive = _drives[driveKey];
            if (drive == null) return;
            foreach (DataGridViewRow row in grid.SelectedRows)
            {
                var key = row.Cells[0].Value.ToString();
                switch (drive.GetDriveEntryType(key))
                {
                    case DriveEntryType.Directory:
                        if (MessageBox.Show(
                            String.Format(Resources.DeleteDirectoryQuestion, key),
                            Resources.DeleteDirectoryCaption,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            drive.DeleteDirectory(key);
                            row.Selected = false;
                        }
                        break;
                    case DriveEntryType.File:
                        if (MessageBox.Show(
                            String.Format(Resources.DeleteFileQuestion, key),
                            Resources.DeleteFileCaption,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            drive.DeleteFile(key);
                            row.Selected = false;
                        }
                        break;
                }
            }
            RefreshDriveEntries(driveKey, directoryKey);
        }

        private void AddHdfsServer()
        {
            var form = new HdfsDriveForm();
            if (form.ShowDialog() != DialogResult.OK) return;

            var drive = form.Drive;
            if (!_drives.ContainsKey(drive.Key))
            {
                _drives.Add(drive.Key, drive);
                StoreHdfsDrives();
                var node = leftDirectoryTree.Nodes.Add(drive.Key, drive.Label);
                node.Nodes.Add(Resources.LoadingText);
                node = rightDirectoryTree.Nodes.Add(drive.Key, drive.Label);
                node.Nodes.Add(Resources.LoadingText);
            }
            else
            {
                MessageBox.Show(Resources.HdfsDriveExistsMessage, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void EditHdfsServer(HdfsDrive drive)
        {
            var currentDriveKey = drive.Key;
            var driveForm = new HdfsDriveForm { Drive = drive };
            if (driveForm.ShowDialog() != DialogResult.OK) return;

            _drives.Remove(currentDriveKey);
            var newDrive = driveForm.Drive;
            if (!_drives.ContainsKey(newDrive.Key))
            {
                _drives.Add(newDrive.Key, newDrive);
                StoreHdfsDrives();
                var nodes = leftDirectoryTree.Nodes.Find(currentDriveKey, false);
                if (nodes.Length == 1)
                {
                    leftDirectoryTree.Nodes[nodes[0].Index].Name = newDrive.Key;
                    leftDirectoryTree.Nodes[nodes[0].Index].Text = newDrive.Label;
                    leftDirectoryTree.Nodes[nodes[0].Index].Nodes.Clear();
                    leftDirectoryTree.Nodes[nodes[0].Index].Nodes.Add(Resources.LoadingText);
                }
                nodes = rightDirectoryTree.Nodes.Find(currentDriveKey, false);
                if (nodes.Length == 1)
                {
                    rightDirectoryTree.Nodes[nodes[0].Index].Name = newDrive.Key;
                    rightDirectoryTree.Nodes[nodes[0].Index].Text = newDrive.Label;
                    rightDirectoryTree.Nodes[nodes[0].Index].Nodes.Clear();
                    rightDirectoryTree.Nodes[nodes[0].Index].Nodes.Add(Resources.LoadingText);
                }
            }
            else
            {
                MessageBox.Show(Resources.HdfsDriveExistsMessage, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void RemoveHdfsServer(HdfsDrive drive)
        {
            try
            {
                if (MessageBox.Show(
                        String.Format(Resources.RemoveHdfsServerQuestion, drive.Label),
                        Resources.RemoveHdfsServerCaption,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;

                _drives.Remove(drive.Key);
                StoreHdfsDrives();
                leftDirectoryTree.Nodes.RemoveByKey(drive.Key);
                rightDirectoryTree.Nodes.RemoveByKey(drive.Key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

    }
}
