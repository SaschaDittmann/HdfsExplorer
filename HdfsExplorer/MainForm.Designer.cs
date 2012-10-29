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
            this.leftFileGrid = new System.Windows.Forms.DataGridView();
            this.LeftKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftLastModifiedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftLastAccessedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftSizeTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftDirectoryTree = new System.Windows.Forms.TreeView();
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
            ((System.ComponentModel.ISupportInitialize)(this.leftFileGrid)).BeginInit();
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
            this.leftFileGrid.Size = new System.Drawing.Size(332, 562);
            this.leftFileGrid.TabIndex = 0;
            this.leftFileGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LeftFileGridKeyPress);
            // 
            // LeftKeyColumn
            // 
            this.LeftKeyColumn.DataPropertyName = "Key";
            this.LeftKeyColumn.HeaderText = "Key";
            this.LeftKeyColumn.Name = "LeftKeyColumn";
            this.LeftKeyColumn.Visible = false;
            this.LeftKeyColumn.Width = 31;
            // 
            // LeftNameColumn
            // 
            this.LeftNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LeftNameColumn.DataPropertyName = "Name";
            this.LeftNameColumn.HeaderText = "Name";
            this.LeftNameColumn.Name = "LeftNameColumn";
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
            this.LeftSizeTextColumn.Width = 52;
            // 
            // LeftSizeColumn
            // 
            this.LeftSizeColumn.DataPropertyName = "Size";
            this.LeftSizeColumn.HeaderText = "Size_Org";
            this.LeftSizeColumn.Name = "LeftSizeColumn";
            this.LeftSizeColumn.Visible = false;
            this.LeftSizeColumn.Width = 75;
            // 
            // leftDirectoryTree
            // 
            this.leftDirectoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftDirectoryTree.HideSelection = false;
            this.leftDirectoryTree.Location = new System.Drawing.Point(0, 0);
            this.leftDirectoryTree.Name = "leftDirectoryTree";
            this.leftDirectoryTree.Size = new System.Drawing.Size(166, 562);
            this.leftDirectoryTree.TabIndex = 0;
            this.leftDirectoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DirectoryTreeBeforeExpand);
            this.leftDirectoryTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.LeftDirectoryTreeBeforeSelect);
            this.leftDirectoryTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DirectoryTreeKeyPress);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.leftSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(1008, 562);
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
            this.leftSplitContainer.Size = new System.Drawing.Size(502, 562);
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
            this.rightSplitContainer.Size = new System.Drawing.Size(502, 562);
            this.rightSplitContainer.SplitterDistance = 166;
            this.rightSplitContainer.TabIndex = 0;
            // 
            // rightDirectoryTree
            // 
            this.rightDirectoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightDirectoryTree.HideSelection = false;
            this.rightDirectoryTree.Location = new System.Drawing.Point(0, 0);
            this.rightDirectoryTree.Name = "rightDirectoryTree";
            this.rightDirectoryTree.Size = new System.Drawing.Size(166, 562);
            this.rightDirectoryTree.TabIndex = 0;
            this.rightDirectoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DirectoryTreeBeforeExpand);
            this.rightDirectoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RightDirectoryTreeAfterSelect);
            this.rightDirectoryTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DirectoryTreeKeyPress);
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
            this.rightFileGrid.Size = new System.Drawing.Size(332, 562);
            this.rightFileGrid.TabIndex = 2;
            this.rightFileGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RightFileGridKeyPress);
            // 
            // RightKeyColumn
            // 
            this.RightKeyColumn.DataPropertyName = "Key";
            this.RightKeyColumn.HeaderText = "Key";
            this.RightKeyColumn.Name = "RightKeyColumn";
            this.RightKeyColumn.Visible = false;
            this.RightKeyColumn.Width = 31;
            // 
            // RightNameColumn
            // 
            this.RightNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RightNameColumn.DataPropertyName = "Name";
            this.RightNameColumn.HeaderText = "Name";
            this.RightNameColumn.Name = "RightNameColumn";
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
            this.RightSizeTextColumn.Width = 52;
            // 
            // RightSizeColumn
            // 
            this.RightSizeColumn.DataPropertyName = "Size";
            this.RightSizeColumn.HeaderText = "Size_Org";
            this.RightSizeColumn.Name = "RightSizeColumn";
            this.RightSizeColumn.Visible = false;
            this.RightSizeColumn.Width = 75;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "MainForm";
            this.Text = "HDFS Explorer";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Leave += new System.EventHandler(this.MainFormLeave);
            ((System.ComponentModel.ISupportInitialize)(this.leftFileGrid)).EndInit();
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
            this.ResumeLayout(false);

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
    }
}

