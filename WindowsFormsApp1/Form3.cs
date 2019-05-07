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

        private Form2 _form2;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(Form2 form2)
        {
            InitializeComponent();
            _form2 = form2;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            chart1.Series[0].Points.DataBindY(_form2.numbers[1]);
            chart1.Series[0].ChartType = SeriesChartType.Line;
        }

        public void Refresher()
        {
            chart1.Series[0].Points.DataBindY(_form2.numbers[1]);
        }

        private void Form3_Deactivate(object sender, EventArgs e)
        {
            _form2.form3 = null;
        }
    }
}
