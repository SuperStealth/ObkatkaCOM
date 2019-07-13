using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
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

            chart1.Series.Add("Наружняя температура");
            chart1.Series[1].XValueType = ChartValueType.Time;
            chart1.Series[1].ChartType = SeriesChartType.Line;

            Refresher(sender, e);

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            
            tCycle.Interval = Properties.Settings.Default.interval;

            if (_sensor.StartTime != DateTime.MinValue)
            {
                StartObkatkaProcess();
            }
        }

        private void StartObkatkaProcess()
        {
            buttonStart.Text = "Остановить обкатку";
        }

        public void Refresher(object sender, EventArgs e)
        {
            var times = _sensor.GetTimeArray();
            var temperatures = _sensor.GetTempArray();
            chart1.Series[0].Points.DataBindXY(times, temperatures);

            var externalTimes = _externalSensor.GetTimeArray(_sensor.StartTime, _sensor.StopTime);
            var externalTemps = _externalSensor.GetTempArray(_sensor.StartTime, _sensor.StopTime);
            chart1.Series[1].Points.DataBindXY(externalTimes, externalTemps);

        }

        public void SaveAsBitmap(Control control, string fileName)
        {
            control.CreateGraphics();

            Bitmap bmp = new Bitmap(control.Width, control.Height);

            control.DrawToBitmap(bmp, new Rectangle(0, 0, control.Width, control.Height));
            bmp = bmp.Clone(new Rectangle(20, 35, control.Width - 40, control.Height - 85), bmp.PixelFormat);
            bmp.Save(fileName);

            bmp.Dispose();
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
                StartObkatkaProcess();
            }
            else if (buttonStart.Text == "Остановить обкатку")
            {
                _sensor.StopTime = DateTime.Now;
                buttonStart.Text = "Сбросить обкатку";
            }
            else
            {
                _sensor.StartTime = DateTime.MinValue;
                _sensor.StopTime = DateTime.MaxValue;
                buttonStart.Text = "Начать обкатку";
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

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            _sensor.Name = nameTextBox.Text;
            _sensor.SerialNum = serialNumberTextBox.Text;
            _sensor.FreonMark = freonMarkTextBox.Text;
            _sensor.FreonQuantity = freonQuantityTextBox.Text;
            _sensor.AddInfo = addInfoTextBox.Text;
        }
    }
}
