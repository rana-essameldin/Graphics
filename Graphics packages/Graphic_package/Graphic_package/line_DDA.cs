using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphic_package
{
    public partial class line_DDA : Form
    {
        List<List<int>> points = new List<List<int>>();
        private static PointF[] pointsArray;
        public line_DDA()
        {
            InitializeComponent();
            pointsArray = new PointF[0];
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
            pointsArray = new PointF[0]; 
            panel2.Invalidate();
        }

       

        
        private void button3_Click_1(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out int x1);
            int.TryParse(textBox2.Text, out int y1);
            int.TryParse(textBox3.Text, out int x2);
            int.TryParse(textBox4.Text, out int y2);


            List<List<int>> points = lineDDA(x1,y1,x2,y2);
            panel2.Invalidate();
            using (Graphics g = panel1.CreateGraphics())
            {
                foreach (List<int> p in points)
                {
                    plotPoint(g, p[0], p[1]);
                }
            }
        }
        static List<List<int>> lineDDA(int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            float deltaX = (float)dx / steps;
            float deltaY = (float)dy / steps;

            pointsArray = new PointF[steps + 1];
          
            float x = x1;
            float y = y1;
            pointsArray[0] = new PointF(x1, y1);
            List<List<int>> points = new List<List<int>>();
             
            int xf = (int)Math.Round(x);
            int yf = (int)Math.Round(y);
            points.Add( new List<int> { xf,yf });
           
            for (int i = 0; i < steps; i++)
            {
                Console.WriteLine($"({Math.Round(x)}, {Math.Round(y)})");
                x += deltaX;
                y += deltaY;
                points.Add(new List<int> { (int)Math.Round(x), (int)Math.Round(y) });
                pointsArray[i] = new PointF(x, y);
            }
           
            return points; 
        }
        void plotPoint(Graphics g, int x, int y)
        {
            Brush b = Brushes.Red;

            int originX = panel1.Width / 2;
            int originY = panel1.Height / 2;

            int adjustedX = originX + x;
            int adjustedY = originY - y;

            g.FillRectangle(b, adjustedX, adjustedY, 2, 2);

        }

            private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            int xAxisY = panel1.Height / 2;
            g.DrawLine(pen, 0, xAxisY, panel1.Width, xAxisY);

            int yAxisX = panel1.Width / 2;
            g.DrawLine(pen, yAxisX, 0, yAxisX, panel1.Height);

            pen.Dispose();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Pen pen = new Pen(Color.Black);

            int space = 100;
            for (int i = 0; i < pointsArray.Length -1; i++)
            {
                PointF point = pointsArray[i];
                graphic.DrawString(i.ToString(), Font, Brushes.Black, 34, space);
                graphic.DrawString(point.X.ToString("F2"), Font, Brushes.Black, 96, space);
                graphic.DrawString(point.Y.ToString("F2"), Font, Brushes.Black, 158, space);
                graphic.DrawString($"({Math.Round(point.X)}, {Math.Round(point.Y)})", Font, Brushes.Black, 219, space);
                space += 20;
            }
        }
    }
}
