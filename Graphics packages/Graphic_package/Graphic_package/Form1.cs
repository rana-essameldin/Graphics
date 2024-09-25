using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphic_package
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void label1_Click(object sender, EventArgs e)
        {   
                
        }
        private void button1_Click(object sender, EventArgs e)
        {
            line_DDA view2 = new line_DDA();
            view2.Show();
            Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            lineBelengham view3 = new lineBelengham();
            view3.Show();
            Visible = false;
        }   
        private void button3_Click(object sender, EventArgs e)
        {
            circle view4 = new circle();
            view4.Show();
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            elipse view5 = new elipse();
            view5.Show();
            Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            transform view6 = new transform();
            view6.Show();
            Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DrawALL VIEW7 = new DrawALL();
            VIEW7.Show();
            Visible = false;
        }
    }
}
