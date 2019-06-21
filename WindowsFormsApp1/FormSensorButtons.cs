﻿using System;
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

        public FormSensorButtons()
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

            sensors.Add(new Sensor(externalTemp, true));
            lstBtnCalc[externalTemp - 1].Visible = false;

            for (ushort i = 1; i < 33; i++)
            {
                lstBtnCalc[i - 1].Enabled = false;
                if (MODRead(i) != 0)
                {
                    sensors.Add(new Sensor(i,false));
                    lstBtnCalc[i - 1].Enabled = true;
                    lstBtnCalc[i - 1].Click += new EventHandler(ShowFormSensorChart);
                } 
                
            }
            timer1.Interval = Properties.Settings.Default.interval;
            WindowState = FormWindowState.Maximized;

            Count(sender, e);
        }

        private int GetButtonNumber(object sender) => Convert.ToInt32(((Button)sender).Name.ToString().Replace("button", ""));
        private void ShowFormSensorChart(object sender, EventArgs e)
        {
            int sensorNumber = GetButtonNumber(sender);
            FormSensorChart formSensorChart = new FormSensorChart(sensors.Find(s => s.SensorNumber == sensorNumber), sensors.Find(s => s.IsExternal));
            formSensorChart.Show();
            formSensorChart.Text = "Датчик №" + sensorNumber;
        }
        private void Count(object sender, EventArgs e)
        {
            foreach (Sensor sensor in sensors)
            {
                if (!sensor.IsExternal)
                {
                    sensor.measurements.Add(new ChartPoint(MODRead(sensor.SensorNumber) / 10.0, DateTime.Now));
                    lstBtnCalc[sensor.SensorNumber - 1].Text = sensor.SensorNumber.ToString() + ": " + sensor.GetLastMeasurement().ToString() + " C";
                }
                else if (sensor.IsExternal)
                {
                    sensor.measurements.Add(new ChartPoint(MODRead(sensor.SensorNumber) / 10.0, DateTime.Now));
                    labelExtTemp.Text = "Температура окружающей среды: " + sensor.GetLastMeasurement().ToString() + " C";
                }
            }
        }
        private ushort MODRead(ushort startAdress)
        {
            ushort[] result;
            try
            {
                result = MainForm.ModBUS.ReadHoldingRegisters(1, startAdress, 1);
            }
            catch
            {
                result = new ushort[] { 0 };
            }
            return result[0];
        }   

    }

}