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
        private bool[] active = new bool[32];
        List<Button> lstBtnCalc;
        public int externalTemp = 1;
        Form2 form2;
        public Form3 form3;
        public List<double>[] numbers;

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
            numbers = new List<double>[32];

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
                numbers[i-1] = new List<double>();
            }
            active[externalTemp - 1] = false;
            lstBtnCalc[externalTemp - 1].Visible = false;
        }

        private void ShowForm3(object sender, EventArgs e)
        {
            form3 = new Form3(form2);
            form3.Show();
        }

        private void Count(object sender, EventArgs e)
        {
            for (ushort i = 1; i < 33; i++)
            {
                if (active[i - 1] == true)
                {
                    numbers[i-1].Add((MODRead(1, i)[0] / 10.0));
                    lstBtnCalc[i - 1].Text = i.ToString() + ": " + numbers[i-1].Last().ToString() + " C";
                }
                else if (i == externalTemp)
                {
                    numbers[i-1].Add((MODRead(1, i)[0] / 10.0));
                    labelExtTemp.Text = "Температура окружающей среды: " + numbers[i-1].Last().ToString() + " C";
                }                        
            }
            if (form3 != null)
            {
                form3.Refresher();
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
                ((Form1)MdiParent).tbErr.AppendText("Устройство с адресом <" + aa.ToString() + ">, регистр <" + rr.ToString() + "> не отвечает...");
                ((Form1)MdiParent).tbErr.Visible = true;
                ((Form1)MdiParent).tbErr.Refresh();
                //                MessageBox.Show("Устройство с адресом <" + aa.ToString() + ">, регистр <" + rr.ToString() + "> не отвечает...");
                dd = new ushort[] { 0 };
            }
            return dd;
        }




        //private void tCycle_Tick(object sender, EventArgs e)
        //{
        //    if ((!readState) & (Form1.ModBUS != null))
        //    {
        //        readState = true;
        //        if (sBtS.Adr > 0)
        //        {
        //            var reg = MODRead(sBtS.Adr, sBtS.Reg);
        //            int a = (int)reg[0] & (1 << sBtS.Msk);
        //            int b = 0;
        //            if (sBtst.Adr > 0)
        //            {
        //                reg = MODRead(sBtst.Adr, sBtst.Reg);
        //                b = (int)reg[0] & (1 << sBtst.Msk);
        //            };
        //            if (a > 0)
        //            {
        //                if (b == 0) btStart_Click(sender, e); else btHZ_Manual();
        //            }
        //            readState = false;
        //        }
        //    };
        //    if (tNext > 0)
        //    {
        //        tNext--;
        //        lTime.Text = tNext.ToString(); lTime.Refresh();
        //        if (tNext == 0)
        //        {
        //            Step++;
        //            if (Step > (StepCount - 1))
        //            {
        //                Step = 0;
        //                Stop();
        //                lTime.Text = "";
        //            }
        //            else
        //            {
        //                tNext = sTim.power[Step];
        //                textBox1.Text = tNext.ToString();
        //                btStep_Click(sender, e);
        //            };
        //            Refresh();
        //        }
        //    }
        //}

    }

}
