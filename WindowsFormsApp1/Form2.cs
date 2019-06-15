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

        public bool loading;
        public int tNext = 0;
        public int Step, StepCount;
        public bool[] active = new bool[32];
        List<Button> lstBtnCalc;
        public int externalTemp = 1;
        Form2 form2;

        public struct Point
        {
            public double temp;
            public DateTime time;

            public Point(double temp, DateTime time)
            {
                this.temp = temp;
                this.time = time;
            }
        }

        public List<Point>[] numbers;

        public Form2()
        {
            InitializeComponent();
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            lstBtnCalc = new List<Button>
            {
                button1, button2, button3, button4, button5, button6, button7, button8,
                button9, button10, button11, button12, button13, button14, button15, button16,
                button17, button18, button19, button20, button21, button22, button23, button24,
                button25, button26, button27, button28, button29, button30, button31, button32
            };

            form2 = this;
            numbers = new List<Point>[32];

            for (ushort i = 1; i < 33; i++)
            {
                active[i-1] = false;
                lstBtnCalc[i - 1].Visible = false;
                if (MODRead(1, i)[0] != 0)
                {
                    active[i-1] = true;
                    lstBtnCalc[i - 1].Visible = true;
                    lstBtnCalc[i - 1].Click += new EventHandler(ShowForm3);
                } 
                numbers[i-1] = new List<Point>();
            }
            active[externalTemp - 1] = false;
            lstBtnCalc[externalTemp - 1].Visible = false;
            WindowState = FormWindowState.Maximized;
        }

        private void ShowForm3(object sender, EventArgs e)
        {
            int lastClick = Convert.ToInt32(((Button)sender).Name.ToString().Replace("button", ""));

            Form3 form3 = new Form3(form2, lastClick);
            form3.Show();
            form3.Text = "Датчик №" + lastClick;
        }

        private void Count(object sender, EventArgs e)
        {
            for (ushort i = 1; i < 33; i++)
            {
                if (active[i - 1] == true)
                {                    
                    numbers[i - 1].Add(new Point( MODRead(1, i)[0] / 10.0, DateTime.Now));
                    lstBtnCalc[i - 1].Text = i.ToString() + ": " + numbers[i-1].Last().temp.ToString() + " C";
                }
                else if (i == externalTemp)
                {
                    numbers[i - 1].Add(new Point(MODRead(1, i)[0] / 10.0, DateTime.Now));
                    labelExtTemp.Text = "Температура окружающей среды: " + numbers[i-1].Last().temp.ToString() + " C";
                }                        
            }
        }

        private ushort[] MODRead(byte aa, ushort rr)
        {
            ushort[] dd;
            try
            {
                dd = Form1.ModBUS.ReadHoldingRegisters(aa, rr, 1);
            }
            catch
            {
                dd = new ushort[] { 0 };
            }
            return dd;
        }   

    }

}
