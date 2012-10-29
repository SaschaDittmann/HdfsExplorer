using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HdfsExplorer.Drives;
using HdfsExplorer.Properties;

namespace HdfsExplorer
{
    public partial class HdfsDriveForm : Form
    {
        public HdfsDriveForm()
        {
            InitializeComponent();

            DisplayNameTextBox.Text = "";
            HostTextBox.Text = "";
            PortTextBox.Text = Resources.DefaultHdfsDrivePort;
            UserTextBox.Text = "";
        }

        public HdfsDrive Drive
        {
            get
            {
                ushort port;
                if (!ushort.TryParse(PortTextBox.Text.Trim(), out port))
                    return null;

                return String.IsNullOrEmpty(UserTextBox.Text.Trim())
                           ? new HdfsDrive(
                                 DisplayNameTextBox.Text.Trim(),
                                 HostTextBox.Text.Trim(),
                                 port)
                           : new HdfsDrive(
                                 DisplayNameTextBox.Text.Trim(),
                                 HostTextBox.Text.Trim(),
                                 port,
                                 UserTextBox.Text.Trim());
            }
            set
            {
                DisplayNameTextBox.Text = value.Name;
                HostTextBox.Text = value.Host;
                PortTextBox.Text = value.Port.ToString(CultureInfo.InvariantCulture);
                UserTextBox.Text = value.User;
            }
        }

        private void TestConnectionButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (TestConnection())
                {
                    MessageBox.Show(Resources.ConnectionTestSuccessMessage,
                                    Resources.ConnectionTestCaption,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show(Resources.ConnectionTestFailedMessage,
                                    Resources.ConnectionTestCaption,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ConnectionTestCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            try
            {
                TestConnection();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private bool TestConnection()
        {
            ushort port;
            if (!ushort.TryParse(PortTextBox.Text.Trim(), out port)) return false;

            var drive = String.IsNullOrEmpty(UserTextBox.Text.Trim())
                            ? new HdfsDrive(
                                  DisplayNameTextBox.Text.Trim(),
                                  HostTextBox.Text.Trim(),
                                  port)
                            : new HdfsDrive(
                                  DisplayNameTextBox.Text.Trim(),
                                  HostTextBox.Text.Trim(),
                                  port,
                                  UserTextBox.Text.Trim());

            return drive.TotalSize > 0;
        }
    }
}
