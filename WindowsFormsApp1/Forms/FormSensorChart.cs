﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ObkatkaCom
{
    public partial class FormSensorChart : Form
    {

        private Sensor _sensor;
        private Sensor _externalSensor;
        public FormSensorChart()
        {
            InitializeComponent();
        }

        public FormSensorChart(Sensor sensor, Sensor externalSensor)
        {
            InitializeComponent();
            _sensor = sensor;
            _externalSensor = externalSensor;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            chart1.Series.Add("Датчик" + (_sensor.SensorNumber));
            chart1.Series[0].XValueType = ChartValueType.Time;
            
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 3;

            chart1.Series.Add("Наружняя температура");
            chart1.Series[1].XValueType = ChartValueType.Time;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart1.Series[1].BorderWidth = 3;
            Text = "Датчик №" + _sensor.SensorNumber;
            Refresher(sender, e);

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            
            tCycle.Interval = Properties.Settings.Default.interval;
            if (_sensor.state == State.RunInStarted)
            {
                buttonStart.Text = "Остановить обкатку";
            }
            if (_sensor.state == State.RunInEnded)
            {
                buttonStart.Text = "Сбросить обкатку";
            }
            nameTextBox.Text = _sensor.Name;
            serialNumberTextBox.Text = _sensor.SerialNum;
            comboBoxFreonMark.Text = _sensor.FreonMark;
            freonQuantityTextBox.Text = _sensor.FreonQuantity;
            addInfoTextBox.Text = _sensor.AddInfo;
        }



        public void Refresher(object sender, EventArgs e)
        {
            var times = _sensor.GetTimeArray();
            var temperatures = _sensor.GetTempArray();
            chart1.Series[0].Points.DataBindXY(times, temperatures);

            if (_externalSensor != null)
            {
                var externalTimes = _externalSensor.GetTimeArray(_sensor.StartTime, _sensor.StopTime);
                var externalTemps = _externalSensor.GetTempArray(_sensor.StartTime, _sensor.StopTime);
                chart1.Series[1].Points.DataBindXY(externalTimes, externalTemps);
                StringBuilder avgTemperatureString = new StringBuilder("Средняя температура окружающей среды: ");
                avgTemperatureString.Append(externalTemps.Length > 0 ? externalTemps.Average().ToString("0.##") : "?");
                avgTemperatureString.Append("C");
                averageTemperatureLabel.Text = avgTemperatureString.ToString();
            }
        }

        public void SaveAsBitmap(Control control, string fileName)
        {
            control.CreateGraphics();

            Bitmap bmp = new Bitmap(control.Width, control.Height);

            control.DrawToBitmap(bmp, new Rectangle(0, 0, control.Width, control.Height));
            bmp = bmp.Clone(new Rectangle(20, 33, control.Width - 40, control.Height - 80), bmp.PixelFormat);
            bmp.Save(fileName);

            bmp.Dispose();
        }

        public int GetFormNumber()
        {
            return _sensor.SensorNumber;
        }
        public void SaveAsTXT(string fileName)
        {
            StreamWriter textFile = new StreamWriter(fileName);
            textFile.WriteLine(_sensor.Name);
            textFile.WriteLine(_sensor.SerialNum);
            textFile.WriteLine(_sensor.FreonMark);
            textFile.WriteLine(_sensor.FreonQuantity);
            textFile.WriteLine(_sensor.AddInfo);
            for (int i = 0; i < _sensor.measurements.Count; i++)
                if (_sensor.measurements[i].time >= _sensor.StartTime && _sensor.measurements[i].time <= _sensor.StopTime)
                {
                    textFile.WriteLine(_sensor.measurements[i].time + "," + _sensor.measurements[i].temp.ToString().Replace(",","."));
                }
            textFile.Close();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog
            {
                Filter = "Картинка (*.bmp)|*.bmp|Текст (.txt)|*.txt"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                switch (save.FilterIndex)
                {
                    case 1:
                        SaveAsBitmap(this, save.FileName);
                        break;
                    case 2:
                        SaveAsTXT(save.FileName);
                        break;
                }
                
            };
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text == "Начать обкатку")
            {
                _sensor.StartTime = DateTime.Now;
                timePickerFrom.Value = DateTime.Now;
                timePickerTo.Value = DateTime.Now.AddHours(3);
                buttonStart.Text = "Остановить обкатку";
                _sensor.state = State.RunInStarted;
            }
            else if (buttonStart.Text == "Остановить обкатку")
            {
                _sensor.StopTime = DateTime.Now;
                timePickerTo.Value = DateTime.Now;
                buttonStart.Text = "Сбросить обкатку";
                _sensor.state = State.RunInEnded;
            }
            else
            {
                _sensor.StartTime = DateTime.MinValue;
                _sensor.StopTime = DateTime.MaxValue;
                addInfoTextBox.Text = "";
                serialNumberTextBox.Text = "";
                nameTextBox.Text = "";
                comboBoxFreonMark.Text = "";
                freonQuantityTextBox.Text = "";
                buttonStart.Text = "Начать обкатку";
                _sensor.state = State.WaitingForStart;
            }
        }

        private void ExternalTempCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (externalTempCheckBox.Checked)
            {
                chart1.Series[1].Enabled = true;
            }
            else
            {
                chart1.Series[1].Enabled = false;
            }
        }

        private void TimePicker_ValueChanged(object sender, EventArgs e)
        {
            _sensor.StartTime = timePickerFrom.Value;
            _sensor.StopTime = timePickerTo.Value;
            timePickerFrom.Value = _sensor.StartTime;
            timePickerTo.Value = _sensor.StopTime;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (!(nameTextBox.Text == "")) chart1.Titles.FindByName("TitleName").Text = nameTextBox.Text;
            else chart1.Titles.FindByName("TitleName").Text = "_______________________________________";
            if (!(serialNumberTextBox.Text == "")) chart1.Titles.FindByName("TitleSerial").Text = "Серийный номер: " + serialNumberTextBox.Text;
            else chart1.Titles.FindByName("TitleSerial").Text = "Серийный номер: ____________________";
            if (!(comboBoxFreonMark.Text == "")) chart1.Titles.FindByName("TitleFreon").Text = "Марка фреона: " + comboBoxFreonMark.Text + ", количество: " + freonQuantityTextBox.Text + "г";
            else chart1.Titles.FindByName("TitleFreon").Text = "Марка фреона: _______________, количество: _____________г";
            if (!(addInfoTextBox.Text == "")) chart1.Titles.FindByName("TitleAddInfo").Text = "Марка компрессора: " + addInfoTextBox.Text;
            else chart1.Titles.FindByName("TitleAddInfo").Text = "Марка компрессора: _____________________";
            chart1.Titles.FindByName("TitleAvgExternalTemp").Text = averageTemperatureLabel.Text;
            chart1.Titles.FindByName("TitleDate").Text = DateTime.Now.Date.ToLongDateString();
            chart1.Series[0].BorderWidth = 20;
            chart1.Printing.PageSetup();
            chart1.Printing.PrintPreview();
            chart1.Series[0].BorderWidth = 3;
        }

        private void FormSensorChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sensor.Name = nameTextBox.Text;
            _sensor.SerialNum = serialNumberTextBox.Text;
            _sensor.FreonMark = comboBoxFreonMark.Text;
            _sensor.FreonQuantity = freonQuantityTextBox.Text;
            _sensor.AddInfo = addInfoTextBox.Text;
        }
    }
}
