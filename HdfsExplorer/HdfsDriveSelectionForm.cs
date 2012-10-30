using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HdfsExplorer.Drives;

namespace HdfsExplorer
{
    public partial class HdfsDriveSelectionForm : Form
    {
        private readonly List<HdfsDrive> _hdfsDrives;
        
        public HdfsDriveSelectionForm(List<HdfsDrive> hdfsDrives)
        {
            InitializeComponent();
            _hdfsDrives = hdfsDrives;
        }

        private void HdfsDriveSelectionFormLoad(object sender, EventArgs e)
        {
            HdfsServerList.DataSource = _hdfsDrives;
            SelectButton.Enabled = _hdfsDrives.Count > 0;
        }

        public string SelectButtonText
        {
            get { return SelectButton.Text; }
            set { SelectButton.Text = value; }
        }

        public string FormCaption
        {
            get { return Text; }
            set { Text = value; }
        }

        public HdfsDrive SelectedDrive
        {
            get { return HdfsServerList.SelectedItem as HdfsDrive; }
        }

        private void SelectButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
