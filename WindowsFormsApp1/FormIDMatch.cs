using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormIDMatch : Form
    {
        public FormIDMatch()
        {
            InitializeComponent();
        }
        private void FormIDMatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Properties.Settings.Default.startId = Convert.ToInt32(textBox1.Text);
                Properties.Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("Неверное значение!");
            }
        }
    }
}
