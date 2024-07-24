using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ObkatkaCom
{
    public partial class FormSensorButtons : Form
    {
        
        private Button[] lstBtnCalc;
        private Label[] listVoltageLabels;
        public ushort externalTemp = 1;
        private IProtocol sensorProtocol;

        public FormSensorButtons(IProtocol iProtocol)
        {
            sensorProtocol = iProtocol;
            InitializeComponent();           
        }
        public FormSensorButtons(IProtocol iProtocol, List<Sensor> restore)
        {
            iProtocol.Restore(restore);
            sensorProtocol = iProtocol;
            InitializeComponent();           
        }
        public void FormSensorButtons_Load(object sender, EventArgs e)
        {
            lstBtnCalc = new Button[]
            {
                button1, button2, button3, button4, button5, button6, button7, button8,
                button9, button10, button11, button12, button13, button14, button15, button16,
                button17, button18, button19, button20, button21, button22, button23, button24,
                button25, button26, button27, button28, button29, button30, button31, button32
            };
            listVoltageLabels = new Label[]
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label10,
                label11, label12, label13, label14, label15, label16, label17, label18, label19, label20,
                label21, label22, label23, label24, label25, label26, label27, label28, label29, label30,
                label31, label32 
            };

            for (ushort i = 1; i < 33; i++)
            {
                lstBtnCalc[i - 1].Enabled = false;
                if (sensorProtocol.IsActiveSensor(i))
                {
                    lstBtnCalc[i - 1].Enabled = true;
                    listVoltageLabels[i - 1].Enabled = true;
                    listVoltageLabels[i - 1].Visible = true;
                    lstBtnCalc[i - 1].Click += new EventHandler(ShowFormSensorChart);
                }

            }
            timer1.Interval = Properties.Settings.Default.interval;
            WindowState = FormWindowState.Maximized;

            OnCount(sender, e);
        }
        private int GetButtonNumber(object sender) => Convert.ToInt32(((Button)sender).Name.ToString().Replace("button", ""));

        private void ShowFormSensorChart(object sender, EventArgs e)
        {
            int sensorNumber = GetButtonNumber(sender);
            if (!Application.OpenForms.OfType<FormSensorChart>().Cast<FormSensorChart>().Any(form => form.GetFormNumber() == sensorNumber))
            {
                FormSensorChart formSensorChart = new FormSensorChart(sensorProtocol.GetSensor(sensorNumber), sensorProtocol.GetSensor(externalTemp));
                formSensorChart.Show();
                formSensorChart.Text = "Датчик №" + sensorNumber;
            }
            else
            {
                Application.OpenForms.OfType<FormSensorChart>().Cast<FormSensorChart>().First(form => form.GetFormNumber() == sensorNumber).Activate();
                Application.OpenForms.OfType<FormSensorChart>().Cast<FormSensorChart>().First(form => form.GetFormNumber() == sensorNumber).Show();
            }
        }
        private void OnCount(object sender, EventArgs e)
        {
            sensorProtocol.UpdateSensors();
            for (ushort i = 1; i < 33; i++)
            {

                if (sensorProtocol.IsActiveSensor(i))
                {
                    if (!lstBtnCalc[i - 1].Enabled)
                    {
                        lstBtnCalc[i - 1].Enabled = true;
                        listVoltageLabels[i - 1].Enabled = true;
                        listVoltageLabels[i - 1].Visible = true;
                        lstBtnCalc[i - 1].Click += new EventHandler(ShowFormSensorChart);
                    }
                    string voltage = "";
                    if (sensorProtocol is LockedWirelessProtocol || sensorProtocol is WirelessProtocol)
                    {
                        voltage = $"{sensorProtocol.GetSensor(i).Voltage} v";
                    }

                    if (!sensorProtocol.GetSensor(i).IsExternal)
                    {
                        var text = $"{sensorProtocol.GetSensor(i).SensorNumber}:   {sensorProtocol.GetSensor(i).GetLastMeasurement()} ˚С";
                        lstBtnCalc[i - 1].Text = text;
                        
                        listVoltageLabels[i - 1].Text = voltage;
                    }
                    else
                    {
                        lstBtnCalc[i - 1].Enabled = false;
                        labelExtTemp.Text = 
                            "Температура окружающей среды: " + 
                            sensorProtocol.GetSensor(i).GetLastMeasurement().ToString() +
                            " C";
                        label1.Text = voltage;
                        lstBtnCalc[0].Text = $"{sensorProtocol.GetSensor(i).SensorNumber}: {sensorProtocol.GetSensor(i).GetLastMeasurement()}C";
                    }
                    switch (sensorProtocol.GetSensor(i).state)
                    {
                        case State.RunInStarted:
                            lstBtnCalc[sensorProtocol.GetSensor(i).SensorNumber - 1].BackColor = Color.LightGreen;
                            break;
                        case State.RunInEnded:
                            lstBtnCalc[sensorProtocol.GetSensor(i).SensorNumber - 1].BackColor = Color.Yellow;
                            break;
                        case State.WaitingForStart:
                            lstBtnCalc[sensorProtocol.GetSensor(i).SensorNumber - 1].BackColor = Color.WhiteSmoke;
                            break;
                    }
                }
                else
                {
                    lstBtnCalc[i - 1].Enabled = false;
                }

            }
        }
        

        private void FormSensorButtons_FormClosed(object sender, FormClosedEventArgs e)
        {
            sensorProtocol.Close();
            sensorProtocol.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}
