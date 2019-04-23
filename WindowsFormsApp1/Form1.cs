using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Modbus.Device;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        public static IModbusSerialMaster ModBUS;

        public Form1()
        {
            InitializeComponent();
        }

        public bool ModBusOpen()
        {
            return sp485.IsOpen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)mmSpeed.DropDownItems.Add("9600", null, SetBaud_Click)).Checked = true;
            mmSpeed.DropDownItems.Add("14400", null, SetBaud_Click);
            mmSpeed.DropDownItems.Add("19200", null, SetBaud_Click);
            mmSpeed.DropDownItems.Add("38400", null, SetBaud_Click);
            mmSpeed.DropDownItems.Add("57600", null, SetBaud_Click);
            mmSpeed.DropDownItems.Add("115200", null, SetBaud_Click);

            if (SerialPort.GetPortNames().Length == 0) {
                mmNuber.DropDownItems.Add("<NULL>", null, SetPort_Click);
                ChangePort("<NULL>");
            }
            else
            { 
                foreach (string p in SerialPort.GetPortNames())
                {
                    mmNuber.DropDownItems.Add(p,null, SetPort_Click);
                }
                ((ToolStripMenuItem)mmNuber.DropDownItems[0]).Checked = true;
                ChangePort(SerialPort.GetPortNames()[0]);
                ModBUS = ModbusSerialMaster.CreateRtu(sp485);
            }
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
                foreach (ToolStripMenuItem tt in mmSpeed.DropDownItems)
                {
                    if (tt.Checked) sp485.BaudRate = Convert.ToInt32(tt.Text);
                }
                sp485.Open();
                sp485.DiscardOutBuffer();
                sp485.DiscardInBuffer();
                ModBUS = ModbusSerialMaster.CreateRtu(sp485);
            }
        }
        private void SetBaud_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem tt in mmSpeed.DropDownItems)
            {
                tt.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;
            if (sp485.IsOpen) ChangePort(sp485.PortName);
        }
        private void SetPort_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem tt in mmNuber.DropDownItems)
            {
                tt.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;
            ChangePort(((ToolStripItem)sender).Text);   
        }
        private void новаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 cf = new Form2();
            cf.MdiParent = this;
            cf.Show();
        }
    }
}
