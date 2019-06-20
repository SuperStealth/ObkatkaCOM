using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    
    public partial class Form2 : Form
    {
        private List<Sensor> sensors = new List<Sensor>();
        private Button[] lstBtnCalc;
        public ushort externalTemp = 1;
        Form2 form2;

        public Form2()
        {
            InitializeComponent();
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            lstBtnCalc = new Button[]
            {
                button1, button2, button3, button4, button5, button6, button7, button8,
                button9, button10, button11, button12, button13, button14, button15, button16,
                button17, button18, button19, button20, button21, button22, button23, button24,
                button25, button26, button27, button28, button29, button30, button31, button32
            };

            form2 = this;

            sensors.Add(new Sensor(externalTemp, true));
            lstBtnCalc[externalTemp - 1].Visible = false;

            for (ushort i = 1; i < 33; i++)
            {
                lstBtnCalc[i - 1].Enabled = false;
                if (MODRead(1, i)[0] != 0)
                {
                    sensors.Add(new Sensor(i,false));
                    lstBtnCalc[i - 1].Enabled = true;
                    lstBtnCalc[i - 1].Click += new EventHandler(ShowForm3);
                } 
                
            }
            
            WindowState = FormWindowState.Maximized;
        }

        private int GetButtonNumber(object sender) => Convert.ToInt32(((Button)sender).Name.ToString().Replace("button", ""));
        private void ShowForm3(object sender, EventArgs e)
        {
            int lastClickedButton = GetButtonNumber(sender);
            Form3 form3 = new Form3(sensors.Find(s => s.SensorNumber == lastClickedButton), sensors.Find(s => s.IsExternal));
            form3.Show();
            form3.Text = "Датчик №" + lastClickedButton;
        }
        private void Count(object sender, EventArgs e)
        {
            foreach (Sensor sensor in sensors)
            {
                if (!sensor.IsExternal)
                {
                    sensor.measurements.Add(new ChartPoint(MODRead(1, sensor.SensorNumber)[0] / 10.0, DateTime.Now));
                    lstBtnCalc[sensor.SensorNumber - 1].Text = sensor.SensorNumber.ToString() + ": " + sensor.GetLastMeasurement().ToString() + " C";
                }
                else if (sensor.IsExternal)
                {
                    sensor.measurements.Add(new ChartPoint(MODRead(1, sensor.SensorNumber)[0] / 10.0, DateTime.Now));
                    labelExtTemp.Text = "Температура окружающей среды: " + sensor.GetLastMeasurement().ToString() + " C";
                }
            }
        }
        private ushort[] MODRead(byte slaveAdress, ushort startAdress)
        {
            ushort[] result;
            try
            {
                result = Form1.ModBUS.ReadHoldingRegisters(slaveAdress, startAdress, 1);
            }
            catch
            {
                result = new ushort[] { 0 };
            }
            return result;
        }   

    }

}
