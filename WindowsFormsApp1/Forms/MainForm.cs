using System;
using System.Windows.Forms;
using System.IO.Ports;
using Modbus.Device;

namespace ObkatkaCom
{

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private IProtocol modBUS;
        private IDStorage idStorage;
        private void FillDropDownListWithTypes()
        {
            toolStripMenuItemSensorType.DropDownItems.Add("Проводной", null, SetType_Click);
            toolStripMenuItemSensorType.DropDownItems.Add("Беспроводной", null, SetType_Click);
            toolStripMenuItemSensorType.DropDownItems.Add("Беспроводной(новый)", null, SetType_Click);
            foreach (ToolStripMenuItem tt in toolStripMenuItemSensorType.DropDownItems)
            {
                tt.Checked = tt.Text == Properties.Settings.Default.type;
            }
#if DEBUG
            toolStripMenuItemSensorType.DropDownItems.Add("Тестовый", null, SetType_Click);
#endif
        }
        private void SetType_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem tt in toolStripMenuItemSensorType.DropDownItems)
            {
                tt.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;
            Properties.Settings.Default.type = ((ToolStripMenuItem)sender).Text;
            Properties.Settings.Default.Save();
            if (modBUS != null) ChangePort(modBUS.PortName);
        }

        private void FillDropDownListWithPorts()
        {
            if (SerialPort.GetPortNames().Length == 0)
            {
                portNumberList.DropDownItems.Add("<NULL>", null, SetPort_Click);
                ChangePort("<NULL>");
            }
            else
            {
                foreach (string portName in SerialPort.GetPortNames())
                {
                    portNumberList.DropDownItems.Add(portName, null, SetPort_Click);
                    if (portName == Properties.Settings.Default.port)
                    {
                        ((ToolStripMenuItem)portNumberList.DropDownItems[portNumberList.DropDownItems.Count - 1]).Checked = true;
                    }
                }
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDropDownListWithPorts();
            FillDropDownListWithTypes();
            idStorage = new IDStorage();
        }

        private void ChangePort(string port)
        {
            if (modBUS != null)
            {
                modBUS.Close();
                modBUS.Dispose();
                if (port != "<NULL>")
                {
                    modBUS.PortName = port;
                }
            }
            
        }

        private void SetPort_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem tt in portNumberList.DropDownItems)
            {
                tt.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;
            Properties.Settings.Default.port = ((ToolStripMenuItem)sender).Text;
            Properties.Settings.Default.Save();
            ChangePort(Properties.Settings.Default.port);
        }

        private IProtocol ActivatePort()
        {
            if (Properties.Settings.Default.type == "Проводной")
            {
                return new WiredProtocol(Properties.Settings.Default.port);
            }
            else if (Properties.Settings.Default.type == "Беспроводной")
            {
                return new WirelessProtocol(Properties.Settings.Default.port, idStorage);
            }
            else if (Properties.Settings.Default.type == "Беспроводной(новый)")
            {
                return new LockedWirelessProtocol(Properties.Settings.Default.port, idStorage);
            }
            else
            {
                return new TestProtocol(Properties.Settings.Default.port, idStorage);
            }
        }
        private void NewRunInMenuItem_Click(object sender, EventArgs e)
        {
            FormSensorButtons formSensorButtons = new FormSensorButtons(ActivatePort())
            {
                MdiParent = this
            };
            formSensorButtons.Show();
        }

        private void OpenRunInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "файл бэкапа (*.bkp)|*.bkp";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;
                    FormPrint formSensorButtons = new FormPrint()
                    {
                        MdiParent = this
                    };
                    formSensorButtons.Show();
                }
            }
        }
        private void IntervalTextBox_TextChanged(object sender, EventArgs e)
        {
            int interval = Convert.ToInt32(((ToolStripTextBox)sender).Text);
            if (interval > 100)
            {
                Properties.Settings.Default.interval = interval;
                Properties.Settings.Default.Save();
            }
        }

        private void OpenReserveCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "файл бэкапа (*.bkp)|*.bkp";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;
                    FormSensorButtons formSensorButtons = new FormSensorButtons(ActivatePort(), Backup.Restore(filePath))
                    {
                        MdiParent = this
                    };
                    formSensorButtons.Show();
                }
            }
        }

        private void MatchIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIDMatch formIDMatch = new FormIDMatch(idStorage);
            formIDMatch.Show();
        }

        private void RenameIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var message = "Идет запись портов, дождитесь завершения";
            MessageBox.Show(message);
            var protocol = new LockedWirelessProtocol(Properties.Settings.Default.port, idStorage);
            protocol.UpdateIDs();
            protocol.Dispose();
            MessageBox.Show("Запись завершена");
        }
    }
}