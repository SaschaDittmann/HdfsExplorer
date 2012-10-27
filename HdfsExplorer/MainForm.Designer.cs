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
            this.leftDirectoryTree = new System.Windows.Forms.TreeView();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftFileGrid = new System.Windows.Forms.DataGridView();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.rightDirectoryTree = new System.Windows.Forms.TreeView();
            this.rightFileGrid = new System.Windows.Forms.DataGridView();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).BeginInit();
            this.leftSplitContainer.Panel1.SuspendLayout();
            this.leftSplitContainer.Panel2.SuspendLayout();
            this.leftSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFileGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightFileGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // leftDirectoryTree
            // 
            this.leftDirectoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftDirectoryTree.Location = new System.Drawing.Point(0, 0);
            this.leftDirectoryTree.Name = "leftDirectoryTree";
            this.leftDirectoryTree.Size = new System.Drawing.Size(166, 562);
            this.leftDirectoryTree.TabIndex = 0;
            this.leftDirectoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.directoryTree_BeforeExpand);
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
            // leftFileGrid
            // 
            this.leftFileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leftFileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftFileGrid.Location = new System.Drawing.Point(0, 0);
            this.leftFileGrid.Name = "leftFileGrid";
            this.leftFileGrid.Size = new System.Drawing.Size(332, 562);
            this.leftFileGrid.TabIndex = 0;
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
            this.rightDirectoryTree.Location = new System.Drawing.Point(0, 0);
            this.rightDirectoryTree.Name = "rightDirectoryTree";
            this.rightDirectoryTree.Size = new System.Drawing.Size(166, 562);
            this.rightDirectoryTree.TabIndex = 0;
            this.rightDirectoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.directoryTree_BeforeExpand);
            // 
            // rightFileGrid
            // 
            this.rightFileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rightFileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightFileGrid.Location = new System.Drawing.Point(0, 0);
            this.rightFileGrid.Name = "rightFileGrid";
            this.rightFileGrid.Size = new System.Drawing.Size(332, 562);
            this.rightFileGrid.TabIndex = 2;
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 540);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(1008, 22);
            this.mainStatusStrip.TabIndex = 2;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "MainForm";
            this.Text = "HDFS Explorer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.leftSplitContainer.Panel1.ResumeLayout(false);
            this.leftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).EndInit();
            this.leftSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftFileGrid)).EndInit();
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightFileGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView leftDirectoryTree;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.SplitContainer leftSplitContainer;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private System.Windows.Forms.TreeView rightDirectoryTree;
        private System.Windows.Forms.DataGridView leftFileGrid;
        private System.Windows.Forms.DataGridView rightFileGrid;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
    }
}

