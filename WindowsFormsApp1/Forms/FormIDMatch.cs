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
        private IDStorage _idStorage;
        public FormIDMatch(IDStorage idStorage)
        {
            InitializeComponent();
            _idStorage = idStorage;
        }
        private void FormIDMatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string idCSV = "";
                foreach (TextBox item in Controls.OfType<TextBox>())
                {
                    if (item.Name == "textBox1")
                        idCSV = item.Text;
                }
                    
                for (int i = 2; i < 33; i++)
                {
                    idCSV += ",";
                    foreach (TextBox item in Controls.OfType<TextBox>())
                    {
                        if (item.Name == "textBox" + i.ToString())
                            idCSV += item.Text;
                    }
                }
                Properties.Settings.Default.idCSV = idCSV;
                Properties.Settings.Default.Save();
                _idStorage.UpdateSensorsIDs();
            }
            catch
            {
                MessageBox.Show("Неверное значение!");
            }
        }

        private void FormIDMatch_Load(object sender, EventArgs e)
        {            
            if (!_idStorage.IsEmpty())
                foreach (TextBox item in Controls.OfType<TextBox>())
                {
                    item.Text = _idStorage.GetSensorID(Convert.ToInt32(item.Name.Substring(7)) - 1);
                }
        }
    }
}
