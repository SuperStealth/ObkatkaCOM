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
  
        public Form2()
        {
            InitializeComponent();
        }

     
        private Point MouseDownLocation;
        private PictureBox LAM;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
           if (e.Button == System.Windows.Forms.MouseButtons.Left) MouseDownLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PictureBox oPictureBox = (PictureBox)sender;
                int X = (int)((e.X + oPictureBox.Left - MouseDownLocation.X)/5)*5;
                int Y = (int)((e.Y + oPictureBox.Top - MouseDownLocation.Y)/5)*5;
              //  if (((X % 5) == 0) & ((Y % 5) == 0))
                {
                    oPictureBox.Left = X; oPictureBox.Top = Y;
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = new ToolStripMenuItem(mStep.Items.Count.ToString());
            mStep.Items.Insert(mStep.Items.Count - 1, tsi);
        }

        private void CreateLAMP()
        {
            PictureBox PB = new PictureBox();
                PB.Location = new System.Drawing.Point(70, 120);
                PB.Size = new System.Drawing.Size(48, 12);
                PB.TabStop = false;
                PB.SizeMode = PictureBoxSizeMode.Normal;
                PB.BorderStyle = BorderStyle.FixedSingle;
                PB.Visible = true;
                PB.ContextMenuStrip = contextMenuStrip1;
                PB.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseDown);
                PB.MouseMove += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseMove);
                PB.Paint += new System.Windows.Forms.PaintEventHandler(pictureBox1_Paint);
                
            Controls.Add(PB);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CreateLAMP();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Добавить":
                    CreateLAMP();
                    break;
                case "Повернуть":
                    if (LAM.Width == 48)
                        { LAM.Size = new System.Drawing.Size(12, 48); } else
                        { LAM.Size = new System.Drawing.Size(48, 12); };
                    break;
                case "Удалить":
                    LAM.Dispose();
                    break;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            LAM = (PictureBox)((ContextMenuStrip)sender).SourceControl;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.FillRegion(Brushes.Yellow,e.Graphics.Clip);
        }
    }
}
