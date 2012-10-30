namespace HdfsExplorer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.leftFileGrid = new System.Windows.Forms.DataGridView();
            this.LeftKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftLastModifiedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftLastAccessedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftSizeTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftDirectoryTree = new System.Windows.Forms.TreeView();
            this.directoryTreeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addHdfsServerTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editHdfsServerTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeHdfsServerTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSeperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newFolderTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameFolderTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFolderTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSeperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSeperator3 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.rightDirectoryTree = new System.Windows.Forms.TreeView();
            this.rightFileGrid = new System.Windows.Forms.DataGridView();
            this.RightKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightLastModifiedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightLastAccessedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightSizeTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.addHdfsServerButton = new System.Windows.Forms.ToolStripButton();
            this.editHdfsServerButton = new System.Windows.Forms.ToolStripButton();
            this.removeHdfsServerButton = new System.Windows.Forms.ToolStripButton();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mainStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.leftFileGrid)).BeginInit();
            this.directoryTreeContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).BeginInit();
            this.leftSplitContainer.Panel1.SuspendLayout();
            this.leftSplitContainer.Panel2.SuspendLayout();
            this.leftSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightFileGrid)).BeginInit();
            this.mainToolStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftFileGrid
            // 
            this.leftFileGrid.AllowUserToAddRows = false;
            this.leftFileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leftFileGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LeftKeyColumn,
            this.LeftNameColumn,
            this.LeftLastModifiedColumn,
            this.LeftLastAccessedColumn,
            this.LeftTypeColumn,
            this.LeftSizeTextColumn,
            this.LeftSizeColumn});
            this.leftFileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftFileGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.leftFileGrid.Location = new System.Drawing.Point(0, 0);
            this.leftFileGrid.Name = "leftFileGrid";
            this.leftFileGrid.RowHeadersVisible = false;
            this.leftFileGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.leftFileGrid.Size = new System.Drawing.Size(332, 509);
            this.leftFileGrid.TabIndex = 0;
            this.leftFileGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LeftFileGridCellDoubleClick);
            this.leftFileGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LeftFileGridKeyPress);
            this.leftFileGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LeftFileGridKeyUp);
            // 
            // LeftKeyColumn
            // 
            this.LeftKeyColumn.DataPropertyName = "Key";
            this.LeftKeyColumn.HeaderText = "Key";
            this.LeftKeyColumn.Name = "LeftKeyColumn";
            this.LeftKeyColumn.ReadOnly = true;
            this.LeftKeyColumn.Visible = false;
            this.LeftKeyColumn.Width = 31;
            // 
            // LeftNameColumn
            // 
            this.LeftNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LeftNameColumn.DataPropertyName = "Name";
            this.LeftNameColumn.HeaderText = "Name";
            this.LeftNameColumn.Name = "LeftNameColumn";
            this.LeftNameColumn.ReadOnly = true;
            // 
            // LeftLastModifiedColumn
            // 
            this.LeftLastModifiedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LeftLastModifiedColumn.DataPropertyName = "LastModified";
            this.LeftLastModifiedColumn.HeaderText = "Date modified";
            this.LeftLastModifiedColumn.Name = "LeftLastModifiedColumn";
            this.LeftLastModifiedColumn.ReadOnly = true;
            this.LeftLastModifiedColumn.Width = 97;
            // 
            // LeftLastAccessedColumn
            // 
            this.LeftLastAccessedColumn.DataPropertyName = "LastAccessed";
            this.LeftLastAccessedColumn.HeaderText = "Date accessed";
            this.LeftLastAccessedColumn.Name = "LeftLastAccessedColumn";
            this.LeftLastAccessedColumn.ReadOnly = true;
            this.LeftLastAccessedColumn.Visible = false;
            this.LeftLastAccessedColumn.Width = 104;
            // 
            // LeftTypeColumn
            // 
            this.LeftTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LeftTypeColumn.DataPropertyName = "Type";
            this.LeftTypeColumn.HeaderText = "Type";
            this.LeftTypeColumn.Name = "LeftTypeColumn";
            this.LeftTypeColumn.ReadOnly = true;
            this.LeftTypeColumn.Width = 56;
            // 
            // LeftSizeTextColumn
            // 
            this.LeftSizeTextColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LeftSizeTextColumn.DataPropertyName = "SizeText";
            this.LeftSizeTextColumn.HeaderText = "Size";
            this.LeftSizeTextColumn.Name = "LeftSizeTextColumn";
            this.LeftSizeTextColumn.ReadOnly = true;
            this.LeftSizeTextColumn.Width = 52;
            // 
            // LeftSizeColumn
            // 
            this.LeftSizeColumn.DataPropertyName = "Size";
            this.LeftSizeColumn.HeaderText = "Size_Org";
            this.LeftSizeColumn.Name = "LeftSizeColumn";
            this.LeftSizeColumn.ReadOnly = true;
            this.LeftSizeColumn.Visible = false;
            this.LeftSizeColumn.Width = 75;
            // 
            // leftDirectoryTree
            // 
            this.leftDirectoryTree.ContextMenuStrip = this.directoryTreeContextMenu;
            this.leftDirectoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftDirectoryTree.HideSelection = false;
            this.leftDirectoryTree.Location = new System.Drawing.Point(0, 0);
            this.leftDirectoryTree.Name = "leftDirectoryTree";
            this.leftDirectoryTree.Size = new System.Drawing.Size(166, 509);
            this.leftDirectoryTree.TabIndex = 0;
            this.leftDirectoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DirectoryTreeBeforeExpand);
            this.leftDirectoryTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.LeftDirectoryTreeBeforeSelect);
            this.leftDirectoryTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DirectoryTreeKeyPress);
            this.leftDirectoryTree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DirectoryTreeKeyUp);
            // 
            // directoryTreeContextMenu
            // 
            this.directoryTreeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addHdfsServerTreeMenuItem,
            this.editHdfsServerTreeMenuItem,
            this.removeHdfsServerTreeMenuItem,
            this.toolStripMenuSeperator1,
            this.newFolderTreeMenuItem,
            this.renameFolderTreeMenuItem,
            this.deleteFolderTreeMenuItem,
            this.toolStripMenuSeperator2,
            this.copyTreeMenuItem,
            this.cutTreeMenuItem,
            this.pasteTreeMenuItem,
            this.toolStripMenuSeperator3,
            this.refreshTreeMenuItem});
            this.directoryTreeContextMenu.Name = "contextMenuStrip1";
            this.directoryTreeContextMenu.Size = new System.Drawing.Size(185, 264);
            this.directoryTreeContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.DirectoryTreeContextMenuOpening);
            // 
            // addHdfsServerTreeMenuItem
            // 
            this.addHdfsServerTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.AddServer;
            this.addHdfsServerTreeMenuItem.Name = "addHdfsServerTreeMenuItem";
            this.addHdfsServerTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.addHdfsServerTreeMenuItem.Text = "Add HDFS Server";
            this.addHdfsServerTreeMenuItem.Click += new System.EventHandler(this.AddHdfsServerTreeMenuItemClick);
            // 
            // editHdfsServerTreeMenuItem
            // 
            this.editHdfsServerTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.EditServer;
            this.editHdfsServerTreeMenuItem.Name = "editHdfsServerTreeMenuItem";
            this.editHdfsServerTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.editHdfsServerTreeMenuItem.Text = "Edit HDFS Server";
            this.editHdfsServerTreeMenuItem.Click += new System.EventHandler(this.EditHdfsServerTreeMenuItemClick);
            // 
            // removeHdfsServerTreeMenuItem
            // 
            this.removeHdfsServerTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.RemoveServer;
            this.removeHdfsServerTreeMenuItem.Name = "removeHdfsServerTreeMenuItem";
            this.removeHdfsServerTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.removeHdfsServerTreeMenuItem.Text = "Remove HDFS Server";
            this.removeHdfsServerTreeMenuItem.Click += new System.EventHandler(this.RemoveHdfsServerTreeMenuItemClick);
            // 
            // toolStripMenuSeperator1
            // 
            this.toolStripMenuSeperator1.Name = "toolStripMenuSeperator1";
            this.toolStripMenuSeperator1.Size = new System.Drawing.Size(181, 6);
            // 
            // newFolderTreeMenuItem
            // 
            this.newFolderTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.NewFolder;
            this.newFolderTreeMenuItem.Name = "newFolderTreeMenuItem";
            this.newFolderTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.newFolderTreeMenuItem.Text = "New Folder";
            this.newFolderTreeMenuItem.Click += new System.EventHandler(this.NewFolderTreeMenuItemClick);
            // 
            // renameFolderTreeMenuItem
            // 
            this.renameFolderTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.RenameFolder;
            this.renameFolderTreeMenuItem.Name = "renameFolderTreeMenuItem";
            this.renameFolderTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.renameFolderTreeMenuItem.Text = "Rename Folder";
            this.renameFolderTreeMenuItem.Click += new System.EventHandler(this.RenameFolderTreeMenuItemClick);
            // 
            // deleteFolderTreeMenuItem
            // 
            this.deleteFolderTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.DeleteFolder;
            this.deleteFolderTreeMenuItem.Name = "deleteFolderTreeMenuItem";
            this.deleteFolderTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteFolderTreeMenuItem.Text = "Delete Folder";
            this.deleteFolderTreeMenuItem.Click += new System.EventHandler(this.DeleteFolderTreeMenuItemClick);
            // 
            // toolStripMenuSeperator2
            // 
            this.toolStripMenuSeperator2.Name = "toolStripMenuSeperator2";
            this.toolStripMenuSeperator2.Size = new System.Drawing.Size(181, 6);
            // 
            // copyTreeMenuItem
            // 
            this.copyTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.Copy;
            this.copyTreeMenuItem.Name = "copyTreeMenuItem";
            this.copyTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.copyTreeMenuItem.Text = "Copy";
            this.copyTreeMenuItem.Click += new System.EventHandler(this.CopyTreeMenuItemClick);
            // 
            // cutTreeMenuItem
            // 
            this.cutTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.Cut;
            this.cutTreeMenuItem.Name = "cutTreeMenuItem";
            this.cutTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.cutTreeMenuItem.Text = "Cut";
            this.cutTreeMenuItem.Click += new System.EventHandler(this.CutTreeMenuItemClick);
            // 
            // pasteTreeMenuItem
            // 
            this.pasteTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.Paste;
            this.pasteTreeMenuItem.Name = "pasteTreeMenuItem";
            this.pasteTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.pasteTreeMenuItem.Text = "Paste";
            this.pasteTreeMenuItem.Click += new System.EventHandler(this.PasteTreeMenuItemClick);
            // 
            // toolStripMenuSeperator3
            // 
            this.toolStripMenuSeperator3.Name = "toolStripMenuSeperator3";
            this.toolStripMenuSeperator3.Size = new System.Drawing.Size(181, 6);
            // 
            // refreshTreeMenuItem
            // 
            this.refreshTreeMenuItem.Image = global::HdfsExplorer.Properties.Resources.Refresh;
            this.refreshTreeMenuItem.Name = "refreshTreeMenuItem";
            this.refreshTreeMenuItem.ShortcutKeyDisplayString = "F5";
            this.refreshTreeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.refreshTreeMenuItem.Text = "Refresh";
            this.refreshTreeMenuItem.Click += new System.EventHandler(this.RefreshTreeMenuItemClick);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 28);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.leftSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(1008, 509);
            this.mainSplitContainer.SplitterDistance = 502;
            this.mainSplitContainer.TabIndex = 1;
            // 
            // leftSplitContainer
            // 
            this.leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftSplitContainer.Name = "leftSplitContainer";
            // 
            // leftSplitContainer.Panel1
            // 
            this.leftSplitContainer.Panel1.Controls.Add(this.leftDirectoryTree);
            // 
            // leftSplitContainer.Panel2
            // 
            this.leftSplitContainer.Panel2.Controls.Add(this.leftFileGrid);
            this.leftSplitContainer.Size = new System.Drawing.Size(502, 509);
            this.leftSplitContainer.SplitterDistance = 166;
            this.leftSplitContainer.TabIndex = 0;
            // 
            // rightSplitContainer
            // 
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSplitContainer.Name = "rightSplitContainer";
            // 
            // rightSplitContainer.Panel1
            // 
            this.rightSplitContainer.Panel1.Controls.Add(this.rightDirectoryTree);
            // 
            // rightSplitContainer.Panel2
            // 
            this.rightSplitContainer.Panel2.Controls.Add(this.rightFileGrid);
            this.rightSplitContainer.Size = new System.Drawing.Size(502, 509);
            this.rightSplitContainer.SplitterDistance = 166;
            this.rightSplitContainer.TabIndex = 0;
            // 
            // rightDirectoryTree
            // 
            this.rightDirectoryTree.ContextMenuStrip = this.directoryTreeContextMenu;
            this.rightDirectoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightDirectoryTree.HideSelection = false;
            this.rightDirectoryTree.Location = new System.Drawing.Point(0, 0);
            this.rightDirectoryTree.Name = "rightDirectoryTree";
            this.rightDirectoryTree.Size = new System.Drawing.Size(166, 509);
            this.rightDirectoryTree.TabIndex = 0;
            this.rightDirectoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DirectoryTreeBeforeExpand);
            this.rightDirectoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RightDirectoryTreeAfterSelect);
            this.rightDirectoryTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DirectoryTreeKeyPress);
            this.rightDirectoryTree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DirectoryTreeKeyUp);
            // 
            // rightFileGrid
            // 
            this.rightFileGrid.AllowUserToAddRows = false;
            this.rightFileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rightFileGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RightKeyColumn,
            this.RightNameColumn,
            this.RightLastModifiedColumn,
            this.RightLastAccessedColumn,
            this.RightTypeColumn,
            this.RightSizeTextColumn,
            this.RightSizeColumn});
            this.rightFileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightFileGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.rightFileGrid.Location = new System.Drawing.Point(0, 0);
            this.rightFileGrid.Name = "rightFileGrid";
            this.rightFileGrid.RowHeadersVisible = false;
            this.rightFileGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rightFileGrid.Size = new System.Drawing.Size(332, 509);
            this.rightFileGrid.TabIndex = 2;
            this.rightFileGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RightFileGridCellDoubleClick);
            this.rightFileGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RightFileGridKeyPress);
            this.rightFileGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RightFileGridKeyUp);
            // 
            // RightKeyColumn
            // 
            this.RightKeyColumn.DataPropertyName = "Key";
            this.RightKeyColumn.HeaderText = "Key";
            this.RightKeyColumn.Name = "RightKeyColumn";
            this.RightKeyColumn.ReadOnly = true;
            this.RightKeyColumn.Visible = false;
            this.RightKeyColumn.Width = 31;
            // 
            // RightNameColumn
            // 
            this.RightNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RightNameColumn.DataPropertyName = "Name";
            this.RightNameColumn.HeaderText = "Name";
            this.RightNameColumn.Name = "RightNameColumn";
            this.RightNameColumn.ReadOnly = true;
            // 
            // RightLastModifiedColumn
            // 
            this.RightLastModifiedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RightLastModifiedColumn.DataPropertyName = "LastModified";
            this.RightLastModifiedColumn.HeaderText = "Date modified";
            this.RightLastModifiedColumn.Name = "RightLastModifiedColumn";
            this.RightLastModifiedColumn.ReadOnly = true;
            this.RightLastModifiedColumn.Width = 97;
            // 
            // RightLastAccessedColumn
            // 
            this.RightLastAccessedColumn.DataPropertyName = "LastAccessed";
            this.RightLastAccessedColumn.HeaderText = "Date accessed";
            this.RightLastAccessedColumn.Name = "RightLastAccessedColumn";
            this.RightLastAccessedColumn.ReadOnly = true;
            this.RightLastAccessedColumn.Visible = false;
            this.RightLastAccessedColumn.Width = 104;
            // 
            // RightTypeColumn
            // 
            this.RightTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RightTypeColumn.DataPropertyName = "Type";
            this.RightTypeColumn.HeaderText = "Type";
            this.RightTypeColumn.Name = "RightTypeColumn";
            this.RightTypeColumn.ReadOnly = true;
            this.RightTypeColumn.Width = 56;
            // 
            // RightSizeTextColumn
            // 
            this.RightSizeTextColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RightSizeTextColumn.DataPropertyName = "SizeText";
            this.RightSizeTextColumn.HeaderText = "Size";
            this.RightSizeTextColumn.Name = "RightSizeTextColumn";
            this.RightSizeTextColumn.ReadOnly = true;
            this.RightSizeTextColumn.Width = 52;
            // 
            // RightSizeColumn
            // 
            this.RightSizeColumn.DataPropertyName = "Size";
            this.RightSizeColumn.HeaderText = "Size_Org";
            this.RightSizeColumn.Name = "RightSizeColumn";
            this.RightSizeColumn.ReadOnly = true;
            this.RightSizeColumn.Visible = false;
            this.RightSizeColumn.Width = 75;
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addHdfsServerButton,
            this.editHdfsServerButton,
            this.removeHdfsServerButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1008, 25);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // addHdfsServerButton
            // 
            this.addHdfsServerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addHdfsServerButton.Image = global::HdfsExplorer.Properties.Resources.AddServer;
            this.addHdfsServerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addHdfsServerButton.Name = "addHdfsServerButton";
            this.addHdfsServerButton.Size = new System.Drawing.Size(23, 22);
            this.addHdfsServerButton.Text = "Add HDFS Server";
            this.addHdfsServerButton.Click += new System.EventHandler(this.AddHdfsServerButtonClick);
            // 
            // editHdfsServerButton
            // 
            this.editHdfsServerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editHdfsServerButton.Image = global::HdfsExplorer.Properties.Resources.EditServer;
            this.editHdfsServerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editHdfsServerButton.Name = "editHdfsServerButton";
            this.editHdfsServerButton.Size = new System.Drawing.Size(23, 22);
            this.editHdfsServerButton.Text = "Edit HDFS Server";
            this.editHdfsServerButton.Click += new System.EventHandler(this.EditHdfsServerButtonClick);
            // 
            // removeHdfsServerButton
            // 
            this.removeHdfsServerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeHdfsServerButton.Image = global::HdfsExplorer.Properties.Resources.RemoveServer;
            this.removeHdfsServerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeHdfsServerButton.Name = "removeHdfsServerButton";
            this.removeHdfsServerButton.Size = new System.Drawing.Size(23, 22);
            this.removeHdfsServerButton.Text = "Remove HDFS Server";
            this.removeHdfsServerButton.Click += new System.EventHandler(this.RemoveHdfsServerButtonClick);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 540);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(1008, 22);
            this.mainStatusStrip.TabIndex = 3;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // mainStatusLabel
            // 
            this.mainStatusLabel.Name = "mainStatusLabel";
            this.mainStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainSplitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "HDFS Explorer";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Leave += new System.EventHandler(this.MainFormLeave);
            ((System.ComponentModel.ISupportInitialize)(this.leftFileGrid)).EndInit();
            this.directoryTreeContextMenu.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.leftSplitContainer.Panel1.ResumeLayout(false);
            this.leftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).EndInit();
            this.leftSplitContainer.ResumeLayout(false);
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightFileGrid)).EndInit();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView leftDirectoryTree;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.SplitContainer leftSplitContainer;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private System.Windows.Forms.TreeView rightDirectoryTree;
        private System.Windows.Forms.DataGridView rightFileGrid;
        private System.Windows.Forms.DataGridView leftFileGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftKeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftLastModifiedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftLastAccessedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftSizeTextColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftSizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightKeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightLastModifiedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightLastAccessedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightSizeTextColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightSizeColumn;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton addHdfsServerButton;
        private System.Windows.Forms.ToolStripButton removeHdfsServerButton;
        private System.Windows.Forms.ToolStripButton editHdfsServerButton;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel mainStatusLabel;
        private System.Windows.Forms.ContextMenuStrip directoryTreeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addHdfsServerTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editHdfsServerTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeHdfsServerTreeMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSeperator1;
        private System.Windows.Forms.ToolStripMenuItem newFolderTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameFolderTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderTreeMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSeperator2;
        private System.Windows.Forms.ToolStripMenuItem copyTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteTreeMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSeperator3;
        private System.Windows.Forms.ToolStripMenuItem refreshTreeMenuItem;
    }
}

