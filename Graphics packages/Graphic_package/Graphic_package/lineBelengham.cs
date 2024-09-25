using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Graphic_package
{
    public partial class lineBelengham : Form
    {
        private static List<Point> points = new List<Point>();
        static List<int> pk = new List<int>();
        public lineBelengham()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 home = new Form1();
            home.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
            points.Clear();
            panel2.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            int xAxisY = panel1.Height / 2;
            g.DrawLine(pen, 0, xAxisY, panel1.Width, xAxisY);
         
            int yAxisX = panel1.Width / 2;
            g.DrawLine(pen, yAxisX, 0, yAxisX, panel1.Height);
            pen.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            points.Clear();
            pk.Clear();
            int.TryParse(textBox1.Text, out int x1);
            int.TryParse(textBox2.Text, out int y1);
            int.TryParse(textBox3.Text, out int x2);
            int.TryParse(textBox4.Text, out int y2);
            List<List<int>> points1 = DrawLinebresnham(x1, y1, x2, y2);
            panel2.Invalidate();
            using (Graphics g = panel1.CreateGraphics())
            {
                foreach (List<int> p in points1)
                {
                    plotPoint(g, p[0], p[1]);
                }
            }
        }


        public static List<List<int>> DrawLinebresnham(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1);     //dx = 16      // dy =5;
            int dy = Math.Abs(y2 - y1);                  //(32,3) to (48,8)
            int sx = x1 < x2 ? 1 : -1;                   //sx = 1;
            int sy = y1 < y2 ? 1 : -1;                   //sy =1;
            int err = dx - dy;
            int p =2 *dy-dx ;
            pk.Add(0);
            pk.Add(p);
            int err2;
            int xcalc = x1;
            int ycalc = y1;
            List<List<int>> points1 = new List<List<int>>();
            while (true)
            {
                while (xcalc < x2)
                {
                    xcalc++;
                    if (p < 0)
                    {
                        p += 2 * dy;
                        pk.Add(p);
                    }

                    else
                    {
                        ycalc++;
                        p += 2 * (dy - dx);
                        pk.Add(p);
                    }
                }
                points.Add(new Point(x1, y1));
                points1.Add(new List<int> { x1, y1 });

                if (x1 == x2 && y1 == y2)
                    break;
                //err2 = 2.(dx- dy) = 2(11)=22
                err2 = 2 * err;

                if (err2 > -dy) //if y2>y1 up
                {
                    err -= dy;                           //err = 5;      
                    x1 += sx; //walk x                   //33
                }

                if (err2 < dx)
                {
                    err += dx; ;                         //err = 11
                    y1 += sy;                            //4
                }
            }
           
                return points1;

        }
        void plotPoint(Graphics g, int x, int y)
        {
            Brush b = Brushes.Red;

            // Adjusting origin based on panel size
            int originX = panel1.Width / 2;
            int originY = panel1.Height / 2;

            // Adjusting coordinates for negative values
            int adjustedX = originX + x;
            int adjustedY = originY - y;

            g.FillRectangle(b, adjustedX, adjustedY, 2, 2);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            {
                Graphics g = e.Graphics;
                Pen pen = new Pen(Color.Black);

              
                int yOffset = 100; 
                for (int i = 0; i < points.Count ; i++)
                {
                    g.DrawString((i+1).ToString(), Font, Brushes.Black, 51, yOffset);
                    g.DrawString(points[i].X.ToString(), Font, Brushes.Black, 170, yOffset);
                    g.DrawString(points[i].Y.ToString(), Font, Brushes.Black, 200, yOffset);
                    g.DrawString(pk[i].ToString(), Font, Brushes.Black, 300, yOffset);
                    yOffset += 20;
                }
            }
        }
        
    }
}

