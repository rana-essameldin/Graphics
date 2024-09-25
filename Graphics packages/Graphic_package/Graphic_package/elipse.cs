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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Graphic_package
{
    public partial class elipse : Form
    {
        int rx, ry,xc,yc;
        static List<int>pk=new List<int>();
        int count = 0;
        static List<int> par2= new List<int>();
        static List<int> par3 = new List<int>();
        private static List<Point> points = new List<Point>();
        public elipse()
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
            panel2.Invalidate();
            pk.Clear();
            points.Clear();
            
            
            panel4.Invalidate();
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

        private void elipse_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            points.Clear();
            par2.Clear();
            par3.Clear();
            panel2.Invalidate();
            panel4.Invalidate();
            int.TryParse(textBox1.Text, out  xc);
            int.TryParse(textBox2.Text, out  yc);
            int.TryParse(textBox3.Text, out rx);
            int.TryParse(textBox4.Text, out ry);

            List<Point> points1 = DrawEllipse(xc, yc, rx, ry);

            using (Graphics g = panel1.CreateGraphics())
            {
                for(int i = 0; i < points1.Count - 1; i++) { 
                    plotPoint(g, xc + points[i].X, yc + points[i].Y);
                    plotPoint(g, xc - points[i].X, yc + points[i].Y);
                    plotPoint(g, xc + points[i].X, yc - points[i].Y);
                    plotPoint(g, xc - points[i].X, yc - points[i].Y);
                }
            }

        }

        

        static List<Point> DrawEllipse(int xc, int yc, int rx, int ry)
        {
           
            int x = 0;
            int y = ry;

            int p1 = ry * ry - rx * rx * ry + (rx * rx) / 4;
            pk.Add(p1);
            par2.Add(2 * ry * ry * x);
            par3.Add(2 * rx * rx * y);
            points.Add(new Point(x, y));
            
            while (2 * ry * ry * x < 2 * rx * rx * y)
            {
               
                x++;

                if (p1 < 0)
                {
                    par2.Add( 2 * ry * ry * x);
                    par3.Add(2 * rx * rx * y);
                    p1 = p1 + 2 * ry * ry * x + ry * ry;
                    pk.Add(p1);
                }
                else
                {
                    y--;
                    par2.Add(2 * ry * ry * x);
                    par3.Add(2 * rx * rx * y);
                    p1 = p1 + 2 * ry * ry * x - 2 * rx * rx * y + ry * ry;
                    pk.Add(p1);
                }

                points.Add(new Point(x, y));
            }

            int p2 = (int)((ry * ry * (x + 0.5) 
                * (x + 0.5)) + (rx * rx * (y - 1)
                * (y - 1)) - (rx * rx * ry * ry));
            pk.Add(p2);
            
            while (y >= 0)
            {
                points.Add(new Point(x, y));

                y--;

                if (p2 > 0)
                {
                    par2.Add(2 * ry * ry * x);
                    par3.Add(2 * rx * rx * y);
                    p2 = p2 - 2 * rx * rx * y + rx * rx;
                    pk.Add(p2);
                }
                else
                {
                    x++;
                    par2.Add(2 * ry * ry * x);
                    par3.Add(2 * rx * rx * y);
                    p2 = p2 + 2 * ry * ry * x - 2 * rx * rx * y + rx * rx;
                    pk.Add(p2);
                }
            }

            return points;
        }


        //List<List<int>> ellipsePoints = new List<List<int>>();
        //HashSet<Point> visitedPoints = new HashSet<Point>();
        //double angleStep = 0.01;
        ////points.Add(new Point(x, y));
        //for (double angle = 0; angle < 2 * Math.PI; angle += angleStep)
        //{
        //    int x = (int)Math.Round(xCenter + xRadius * Math.Cos(angle));
        //    int y = (int)Math.Round(yCenter + yRadius * Math.Sin(angle));
        //    Point point = new Point(x, y);
        //    // Check if the point has been visited before
        //    if (!visitedPoints.Contains(point))
        //    {
        //        ellipsePoints.Add(new List<int> { x, y });
        //        visitedPoints.Add(point);
        //        points.Add(point);
        //    }


        void plotPoint(Graphics g, int x, int y)
        {
            Brush b = Brushes.Red;

            int originX = panel1.Width / 2;
            int originY = panel1.Height / 2;
            int adjustedX = originX + x;
            int adjustedY = originY - y;

            if (x >= 0 && y >= 0)
            {
                g.FillRectangle(b, adjustedX, adjustedY, 2, 2);
            }
            else if (x < 0 && y >= 0)
            {
                g.FillRectangle(b, adjustedX, adjustedY, 2, 2);
            }
            else if (x < 0 && y < 0)
            {
                g.FillRectangle(b, adjustedX, adjustedY, 2, 2);
            }
            else
            {
                g.FillRectangle(b, adjustedX, adjustedY, 2, 2);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            Graphics graphic = e.Graphics;
            Pen pen = new Pen(Color.Black);
            
            int space = 100;

            for (int k = 0; k < points.Count-1; k++)
            {
                graphic.DrawString((k + 1).ToString(), Font, Brushes.Black, 13, space);
                graphic.DrawString((xc +points[k].X).ToString(), Font, Brushes.Black, 52, space);
                graphic.DrawString((yc + points[k].Y).ToString(), Font, Brushes.Black,90 , space);
                graphic.DrawString((pk[k]).ToString(), Font, Brushes.Black, 133, space);

                graphic.DrawString((par2[k]).ToString(), Font, Brushes.Black, 177, space);

                graphic.DrawString((par3[k]).ToString(), Font, Brushes.Black, 308, space);

                space += 20;
                count = k + 1;
            }
            for (int k = points.Count - 2; k >=0 ; k--)
            {
                graphic.DrawString((count + 1).ToString(), Font, Brushes.Black, 13, space);
                graphic.DrawString((xc + points[k].X).ToString(), Font, Brushes.Black, 52, space);
                graphic.DrawString((yc - points[k].Y).ToString(), Font, Brushes.Black, 90, space);
                graphic.DrawString((pk[k]).ToString(), Font, Brushes.Black, 133, space);
                graphic.DrawString((par2[k]).ToString(), Font, Brushes.Black, 170, space);
                graphic.DrawString((par3[k]).ToString(), Font, Brushes.Black, 308, space);

                space += 20;
                count++;
            }
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

            Graphics graphic = e.Graphics;
            Pen pen = new Pen(Color.Black);
            
            int space = 100;

            for (int k = 0; k < points.Count - 1; k++)
            {
                graphic.DrawString((points.Count*2+1).ToString(), Font, Brushes.Black, 3, space);
                graphic.DrawString((xc - points[k].X).ToString(), Font, Brushes.Black, 38, space);
                graphic.DrawString((yc - points[k].Y).ToString(), Font, Brushes.Black, 119, space);
                graphic.DrawString((pk[k]).ToString(), Font, Brushes.Black, 163, space);
                graphic.DrawString((par2[k]).ToString(), Font, Brushes.Black, 294, space);

                space += 20;
                count++;
            }
            for (int k = points.Count - 1; k >= 0; k--)
            {
                graphic.DrawString((count + 1).ToString(), Font, Brushes.Black, 3, space);
                graphic.DrawString((xc - points[k].X).ToString(), Font, Brushes.Black, 38, space);
                graphic.DrawString((yc + points[k].Y).ToString(), Font, Brushes.Black, 119, space);
                graphic.DrawString((pk[k]).ToString(), Font, Brushes.Black, 163, space);
                graphic.DrawString((par2[k]).ToString(), Font, Brushes.Black, 294, space);

                space += 20;
                count++;
            }
            

        }
    }
}