using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HdfsExplorer.Drives;

namespace HdfsExplorer
{
    public partial class MainForm : Form
    {
        private Dictionary<string, IDrive> _drives;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _drives = new Dictionary<string, IDrive>();
            AddStandardDrives();
            AddHdfsDrives();
            RefreshDirectoryTrees();
        }

        private void RefreshDirectoryTrees()
        {
            TreeNode node;

            leftDirectoryTree.Nodes.Clear();
            rightDirectoryTree.Nodes.Clear();
            foreach (var drive in _drives)
            {
                node = leftDirectoryTree.Nodes.Add(drive.Key, drive.Value.Label);
                node.Nodes.Add("Loading...");
                node = rightDirectoryTree.Nodes.Add(drive.Key, drive.Value.Label);
                node.Nodes.Add("Loading...");
            }
        }

        private void AddStandardDrives()
        {
            foreach(var drive in DriveInfo.GetDrives())
            {
                var standardDrive = new StandardDrive(drive);
                _drives.Add(standardDrive.Key, standardDrive);
            }
        }

        private void AddHdfsDrives()
        {
            // Dummy Drive
            var hdfsDrive = new HdfsDrive("Local HDInsight Server", "localhost", 8020);
            _drives.Add(hdfsDrive.Key, hdfsDrive);

            hdfsDrive = new HdfsDrive("Dead Drive", "localhost", 9000);
            _drives.Add(hdfsDrive.Key, hdfsDrive);
        }

        private void directoryTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                RefreshDirectories(e.Node);
            }
            catch (Exception ex)
            {
                e.Node.FirstNode.Text = "Error: " + ex.Message;
                e.Node.FirstNode.ForeColor = Color.Red;
            }
        }

        private void RefreshDirectories(TreeNode node)
        {
            // Get Drive
            var driveNode = node;
            while (driveNode.Level > 0)
                driveNode = driveNode.Parent;
            if (!_drives.ContainsKey(driveNode.Name)) return;
            var drive = _drives[driveNode.Name];

            // Update Directories
            var directories = drive.GetDirectories(node.Name.Split('|')[0]);
            node.Nodes.Clear();
            foreach (var directory in directories)
            {
                var newNode = node.Nodes.Add(directory.Key, directory.Name);
                newNode.Nodes.Add("Loading...");
            }
        }
    }
}
