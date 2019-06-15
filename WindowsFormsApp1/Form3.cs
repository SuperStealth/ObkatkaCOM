using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {

        private readonly Form2 _form2;
        private readonly int _num;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(Form2 form2, int num)
        {
            InitializeComponent();
            _form2 = form2;
            _num = num;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            chart1.Series.Clear();
            chart1.Series.Add("Датчик" + (_num));
            chart1.Series[0].XValueType = ChartValueType.Time;
            chart1.Series[0].Points.DataBindXY(_form2.numbers[_num - 1].Select(item => item.time).ToArray(), _form2.numbers[_num - 1].Select(item => item.temp).ToArray());
            chart1.Series[0].ChartType = SeriesChartType.Line;

            chart1.Series.Add("Наружняя температура");
            chart1.Series[1].XValueType = ChartValueType.Time;
            chart1.Series[1].Points.DataBindXY(_form2.numbers[_form2.externalTemp - 1].Select(item => item.time).ToArray(), _form2.numbers[_form2.externalTemp - 1].Select(item => item.temp).ToArray());
            chart1.Series[1].ChartType = SeriesChartType.Line;

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
        }

        public void Refresher(object sender, EventArgs e)
        {
            //chart1.Series.Add("Датчик" + (_num));
            chart1.Series[0].Points.DataBindXY(_form2.numbers[_num - 1].Select(item => item.time).ToArray(), _form2.numbers[_num - 1].Select(item => item.temp).ToArray());
            //chart1.Series[0].ChartType = SeriesChartType.Line;

            //chart1.Series.Add("Наружняя температура");
            chart1.Series[1].Points.DataBindXY(_form2.numbers[_form2.externalTemp - 1].Select(item => item.time).ToArray(), _form2.numbers[_form2.externalTemp - 1].Select(item => item.temp).ToArray());
            //chart1.Series[1].ChartType = SeriesChartType.Line;
        }

        public void SaveAsBitmap(Control control, string fileName)
        {
            //get the instance of the graphics from the control
            Graphics g = control.CreateGraphics();

            //new bitmap object to save the image
            Bitmap bmp = new Bitmap(control.Width, control.Height);

            //Drawing control to the bitmap
            control.DrawToBitmap(bmp, new Rectangle(0, 0, control.Width, control.Height));

            bmp.Save(fileName);
            bmp.Dispose();

        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {

        }
    }
}
