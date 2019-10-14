using System;
using System.Windows.Forms;
using System.IO.Ports;
using Modbus.Device;

namespace WindowsFormsApp1
{

    public partial class MainForm : Form
    {
        public static IModbusSerialMaster ModBUS;
        public SerialPort sp485;
        public MainForm()
        {
            InitializeComponent();
        }

        public bool ModBusOpen()
        {
            return sp485.IsOpen;
        }


        private void GetCheckedSpeed()
        {
            foreach (ToolStripMenuItem toolStripMenuItem in speedList.DropDownItems)
            {
                if (toolStripMenuItem.Text == Properties.Settings.Default.speed)
                {
                    toolStripMenuItem.Checked = true;
                }
            }
        }
        private void FillDropDownListWithTypes()
        {
            toolStripMenuItemSensorType.DropDownItems.Add("Проводной", null, SetType_Click);
            toolStripMenuItemSensorType.DropDownItems.Add("Беспроводной", null, SetType_Click);
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
            if (sp485.IsOpen) ChangePort(sp485.PortName);
        }
        private void FillDropDownListWithSpeeds()
        {
            speedList.DropDownItems.Add("9600", null, SetBaud_Click);
            speedList.DropDownItems.Add("14400", null, SetBaud_Click);
            speedList.DropDownItems.Add("19200", null, SetBaud_Click);
            speedList.DropDownItems.Add("38400", null, SetBaud_Click);
            speedList.DropDownItems.Add("57600", null, SetBaud_Click);
            speedList.DropDownItems.Add("115200", null, SetBaud_Click);
            GetCheckedSpeed();
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
                ChangePort(Properties.Settings.Default.port);
            }
        }

        private void FillDropDownTextBoxWithInterval()
        {
            toolStripTextBoxInterval.Text = Properties.Settings.Default.interval.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDropDownListWithSpeeds();
            FillDropDownListWithPorts();
            FillDropDownTextBoxWithInterval();
            FillDropDownListWithTypes();
        }

        private void ChangePort(string port)
        {
            if (sp485.IsOpen)
            {
                sp485.Close();
                ModBUS.Dispose();
            }
            if (port != "<NULL>")
            {
                sp485.PortName = port;
                foreach (ToolStripMenuItem tt in speedList.DropDownItems)
                {
                    if (tt.Checked) sp485.BaudRate = Convert.ToInt32(tt.Text);
                }
            }
        }
        private void SetBaud_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem tt in speedList.DropDownItems)
            {
                tt.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;
            Properties.Settings.Default.speed = ((ToolStripMenuItem)sender).Text;
            Properties.Settings.Default.Save();
            if (sp485.IsOpen) ChangePort(sp485.PortName);
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
            if (sp485.IsOpen)
            {
                sp485.Close();
                ModBUS.Dispose();
            }
            sp485.Open();
            sp485.DiscardOutBuffer();
            sp485.DiscardInBuffer();
            ModBUS = ModbusSerialMaster.CreateRtu(sp485);
            if (Properties.Settings.Default.type == "Проводной")
            {
                return new WiredProtocol();
            }
            else
            {
                return new WirelessProtocol();
            }
        }
        private void NewObkatkaMenuItem_Click(object sender, EventArgs e)
        {
            FormSensorButtons formSensorButtons = new FormSensorButtons(ActivatePort())
            {
                MdiParent = this
            };
            formSensorButtons.Show();
        }

        private void ОткрытьОбкаткуToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ОткрытьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e)
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


    }
}

