namespace HdfsExplorer
{
    partial class TransferFileForm
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
            this.FileTransferProgressBar = new System.Windows.Forms.ProgressBar();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.TargetLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SourceFilePath = new System.Windows.Forms.Label();
            this.TargetFilePath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileTransferProgressBar
            // 
            this.FileTransferProgressBar.Location = new System.Drawing.Point(12, 57);
            this.FileTransferProgressBar.Name = "FileTransferProgressBar";
            this.FileTransferProgressBar.Size = new System.Drawing.Size(390, 23);
            this.FileTransferProgressBar.TabIndex = 0;
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Location = new System.Drawing.Point(9, 9);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(41, 13);
            this.SourceLabel.TabIndex = 1;
            this.SourceLabel.Text = "Source";
            // 
            // TargetLabel
            // 
            this.TargetLabel.AutoSize = true;
            this.TargetLabel.Location = new System.Drawing.Point(9, 32);
            this.TargetLabel.Name = "TargetLabel";
            this.TargetLabel.Size = new System.Drawing.Size(60, 13);
            this.TargetLabel.TabIndex = 2;
            this.TargetLabel.Text = "Destination";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(327, 86);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // SourceFilePath
            // 
            this.SourceFilePath.AutoSize = true;
            this.SourceFilePath.Location = new System.Drawing.Point(77, 9);
            this.SourceFilePath.Name = "SourceFilePath";
            this.SourceFilePath.Size = new System.Drawing.Size(41, 13);
            this.SourceFilePath.TabIndex = 4;
            this.SourceFilePath.Text = "Source";
            // 
            // TargetFilePath
            // 
            this.TargetFilePath.AutoSize = true;
            this.TargetFilePath.Location = new System.Drawing.Point(77, 32);
            this.TargetFilePath.Name = "TargetFilePath";
            this.TargetFilePath.Size = new System.Drawing.Size(60, 13);
            this.TargetFilePath.TabIndex = 5;
            this.TargetFilePath.Text = "Destination";
            // 
            // TransferFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 123);
            this.Controls.Add(this.TargetFilePath);
            this.Controls.Add(this.SourceFilePath);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.TargetLabel);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.FileTransferProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TransferFileForm";
            this.ShowIcon = false;
            this.Text = "Transfer File(s)";
            this.Load += new System.EventHandler(this.TransferFileFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar FileTransferProgressBar;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Label TargetLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label SourceFilePath;
        private System.Windows.Forms.Label TargetFilePath;
    }
}