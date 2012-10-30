using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HdfsExplorer
{
    public partial class DriveEntryNameForm : Form
    {
        public DriveEntryNameForm()
        {
            InitializeComponent();
        }

        public string SaveButtonText
        {
            get { return SaveButton.Text; }
            set { SaveButton.Text = value; }
        }

        public string FormCaption
        {
            get { return Text; }
            set { Text = value; }
        }

        public string DriveEntryName
        {
            get { return NameTextBox.Text; }
            set { NameTextBox.Text = value; }
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
