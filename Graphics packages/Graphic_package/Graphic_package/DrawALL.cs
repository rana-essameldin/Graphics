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
    public partial class DrawALL : Form
    {
        public DrawALL()
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
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out int x1);
            int.TryParse(textBox2.Text, out int y1);
            int.TryParse(textBox3.Text, out int x2);
            int.TryParse(textBox4.Text, out int y2);


            List<List<int>> points = lineDDA(x1, y1, x2, y2);
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
           
              PointF[] pointsArray;
            pointsArray = new PointF[steps + 1]; 

            float x = x1;
            float y = y1;
            pointsArray[0] = new PointF(x1, y1); 
            List<List<int>> points = new List<List<int>>();

            int xf = (int)Math.Round(x);
            int yf = (int)Math.Round(y);
            points.Add(new List<int> { xf, yf });

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

            // Adjusting coordinates for negative values
            int adjustedX = originX + x;
            int adjustedY = originY - y;
            g.FillRectangle(b, adjustedX, adjustedY, 2, 2);
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

        private void button4_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox5.Text, out int x1);
            int.TryParse(textBox6.Text, out int y1);
            int.TryParse(textBox7.Text, out int x2);
            int.TryParse(textBox8.Text, out int y2);
            List<List<int>> points1 = DrawLinebresnham(x1, y1, x2, y2);
            
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
            int err2;
            List<List<int>> points1 = new List<List<int>>();
            while (true)
            {
                List<Point> points = new List<Point>();
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

        private void button5_Click(object sender, EventArgs e)
        {
            
            int r = Convert.ToInt32(textBox11.Text);
            int xc = Convert.ToInt32(textBox9.Text);
            int yc = Convert.ToInt32(textBox10.Text);
            List<List<int>> circlePoints = Midpoint(xc, yc, r);
            using (Graphics g = panel1.CreateGraphics())
            {
                foreach (List<int> p in circlePoints)
                {
                    plotPoint(g, p[0], p[1]);

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            int.TryParse(textBox12.Text, out int xc);
            int.TryParse(textBox13.Text, out int yc);
            int.TryParse(textBox14.Text, out int xr);
            int.TryParse(textBox15.Text, out int yr);

            List<List<int>> points = DrawEllipse(xc, yc, xr, yr);

            using (Graphics g = panel1.CreateGraphics())
            {
                foreach (List<int> p in points)
                {
                    plotPoint(g, p[0], p[1]);
                    
                }
            }
        }
        List<List<int>> DrawEllipse(int xCenter, int yCenter, int xRadius, int yRadius)
        {
            List<List<int>> ellipsePoints = new List<List<int>>();
            HashSet<Point> visitedPoints = new HashSet<Point>();
            double angleStep = 0.01;
            for (double angle = 0; angle < 2 * Math.PI; angle += angleStep)
            {
                int x = (int)Math.Round(xCenter + xRadius * Math.Cos(angle));
                int y = (int)Math.Round(yCenter + yRadius * Math.Sin(angle));
                Point point = new Point(x, y);

                if (!visitedPoints.Contains(point))
                {
                    ellipsePoints.Add(new List<int> { x, y });
                    visitedPoints.Add(point);
                    
                }
            }

            return ellipsePoints;
        }
        private List<List<int>> Midpoint(int xc, int yc, int r)
        {

            List<List<int>> circlePoints = new List<List<int>>();
            Bitmap pic = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
            int x = 0;
            int y = r;
            int p = 1 - r;
            
            
            circlePoints.Add(new List<int> { x + xc, y + yc });

            cp(xc, yc, x, y, pic);
            while (x < y)
            {
                x++;
                if (p < 0)
                {
                    p += 2 * x + 1;
                }
                else
                {
                    y--;
                    p += 2 * (x - y) + 1;
                }
                cp(xc, yc, x, y, pic);

            }
            return circlePoints;
        }
        void cp(int xc, int yc, int x, int y, Bitmap pic)
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                plotPoint(g, xc + x, yc + y);


                plotPoint(g, xc - x, yc + y);


                plotPoint(g, xc + x, yc - y);


                plotPoint(g, xc - x, yc - y);


                plotPoint(g, xc + y, yc + x);


                plotPoint(g, xc - y, yc + x);


                plotPoint(g, xc + y, yc - x);


                plotPoint(g, xc - y, yc - x);

            }
        }
    }
}
