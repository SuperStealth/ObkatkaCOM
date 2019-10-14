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
    public partial class FormSensorButtons : Form
    {
        private List<Sensor> sensors = new List<Sensor>();
        private Button[] lstBtnCalc;
        public ushort externalTemp = 1;
        private Backup backup;
        private IProtocol temperatureSensor;
        public FormSensorButtons(IProtocol iProtocol)
        {
            temperatureSensor = iProtocol;
            InitializeComponent();           
        }
        public FormSensorButtons(IProtocol iProtocol, List<Sensor> restore)
        {
            sensors = restore;
            temperatureSensor = iProtocol;
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

            sensors.Add(new Sensor(externalTemp, true));
            lstBtnCalc[externalTemp - 1].Visible = false;

            for (ushort i = 1; i < 33; i++)
            {
                lstBtnCalc[i - 1].Enabled = false;
                if (MODRead(i) != 0)
                {
                    sensors.Add(new Sensor(i, false));
                    lstBtnCalc[i - 1].Enabled = true;
                    lstBtnCalc[i - 1].Click += new EventHandler(ShowFormSensorChart);
                }

            }
            timer1.Interval = Properties.Settings.Default.interval;
            WindowState = FormWindowState.Maximized;

            OnCount(sender, e);
            backup = new Backup(this, Properties.Settings.Default.interval);
        }
        public List<Sensor> GetSensors()
        {

            return sensors;
        }
        private int GetButtonNumber(object sender) => Convert.ToInt32(((Button)sender).Name.ToString().Replace("button", ""));
        private void ShowFormSensorChart(object sender, EventArgs e)
        {
            int sensorNumber = GetButtonNumber(sender);
            if (!Application.OpenForms.OfType<FormSensorChart>().Cast<FormSensorChart>().Any(form => form.GetFormNumber() == sensorNumber))
            {
                FormSensorChart formSensorChart = new FormSensorChart(sensors.Find(s => s.SensorNumber == sensorNumber), sensors.Find(s => s.IsExternal));
                formSensorChart.Show();
                formSensorChart.Text = "Датчик №" + sensorNumber;
            }
        }
        private void OnCount(object sender, EventArgs e)
        {

            foreach (Sensor sensor in sensors)
            {
                if (!sensor.IsExternal)
                {
                    sensor.measurements.Add(new ChartPoint(MODRead(sensor.SensorNumber) / 10.0, DateTime.Now));
                    lstBtnCalc[sensor.SensorNumber - 1].Text = sensor.SensorNumber.ToString() + ": " + sensor.GetLastMeasurement().ToString() + " C";
                }
                else
                {
                    sensor.measurements.Add(new ChartPoint(MODRead(sensor.SensorNumber) / 10.0, DateTime.Now));
                    labelExtTemp.Text = "Температура окружающей среды: " + sensor.GetLastMeasurement().ToString() + " C";
                }
                switch (sensor.state)
                {
                    case State.ObkatkaStarted:
                        lstBtnCalc[sensor.SensorNumber - 1].BackColor = Color.LightGreen;
                        break;
                    case State.ObkatkaEnded:
                        lstBtnCalc[sensor.SensorNumber - 1].BackColor = Color.Yellow;
                        break;
                    case State.WaitingForStart:
                        lstBtnCalc[sensor.SensorNumber - 1].BackColor = Color.WhiteSmoke;
                        break;
                }
            }
        }
        private short MODRead(ushort startAdress)
        {
            ushort[] result;
            try
            {
                result = MainForm.ModBUS.ReadHoldingRegisters(1, startAdress, 1);
            }
            catch
            {
                return 0;
            }
            return (short)result[0];
        }

        private void FormSensorButtons_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }

}
