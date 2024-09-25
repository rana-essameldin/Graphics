using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Graphic_package
{
    public partial class circle : Form
    {
        List<List<int>> circlePoints = new List<List<int>>();
        private static List<Point> points = new List<Point>();
        List<int> pk = new List<int>();
        int xc, yc;
        int y;
        int l = 0;
        public circle()
        {
            InitializeComponent();
            circlePoints.Clear(); 
            panel2.Invalidate();
            panel3.Invalidate();
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
            panel3.Invalidate();
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
            l = 0;
            points.Clear(); 
            panel2.Invalidate();
            panel3.Invalidate();
            int r = Convert.ToInt32(textBox3.Text);
             xc = Convert.ToInt32(textBox1.Text);
             yc = Convert.ToInt32(textBox2.Text);
           
            List<List<int>> circlePoints = Midpoint(xc, yc, r);
            panel2.Invalidate();
            panel3.Invalidate();

        }

        private List<List<int>> Midpoint(int xc, int yc, int r)
        {
            
            Bitmap pic = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
            int x = 0;
            int y = r;
            int p = 1 - r;
            pk.Add(p);
            points.Add(new Point(x , y ));
            Point point = new Point(x + xc, y + yc);
            circlePoints.Add(new List<int> { x , y });
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
                pk.Add(p);
                cp(xc, yc, x, y, pic);
                points.Add(new Point(x,y));
                circlePoints.Add(new List<int> { x , y  });
            }
            return circlePoints;
        }
        void cp(int xc, int yc, int x, int y, Bitmap pic)
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                plotPoint(g, xc + x, yc + y);
                plotPoint(g,xc - x, yc + y);
       
                plotPoint(g,xc + x, yc - y);
                

                plotPoint(g,xc - x, yc - y);
                

                plotPoint(g,xc + y, yc + x);
               

                plotPoint(g, xc - y, yc + x);
                

                plotPoint(g, xc + y, yc - x);
                

                plotPoint(g, xc - y, yc - x);
                
            }
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

      

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics graphic = e.Graphics;
            Pen pen = new Pen(Color.Black);
             List<Point> pointscorr = new List<Point>();
            int space = 100;
            Font newFont = new Font("Arial", 9, FontStyle.Regular); // Change "Arial" to the desired font family and 12 to the desired font size

            for (int k = 0; k < points.Count ; k++)
            {
                graphic.DrawString((k+1).ToString(), newFont, Brushes.Black, 3, space);
                graphic.DrawString((xc+points[k].X).ToString(), newFont, Brushes.Black, 23, space);
                graphic.DrawString((yc+points[k].Y).ToString(), newFont, Brushes.Black, 50, space);
                graphic.DrawString(pk[k].ToString(), newFont, Brushes.Black, 112, space);
                graphic.DrawString((2*(xc + points[k].X)).ToString(), newFont, Brushes.Black, 161, space);
                graphic.DrawString((2*(xc + points[k].Y)).ToString(), newFont, Brushes.Black, 239, space);
                space += 20;
                l = k+1;
            }
            for (int m = points.Count-2; m >=0 ; m--)
            {
                graphic.DrawString((l + 1).ToString(), newFont, Brushes.Black, 3, space);
                graphic.DrawString((xc + points[m].Y).ToString(), newFont, Brushes.Black, 23, space);
                graphic.DrawString((yc + points[m].X).ToString(), newFont, Brushes.Black, 50, space);
                graphic.DrawString(pk[m].ToString(), newFont, Brushes.Black, 112, space);
                graphic.DrawString((2 * (xc + points[m].X)).ToString(), newFont, Brushes.Black, 161, space);
                graphic.DrawString((2 * (yc + points[m].Y)).ToString(), newFont, Brushes.Black, 239, space);
                space += 20;
                l++;
            }
            for (int k = 1; k < points.Count; k++)
            {
                graphic.DrawString((l + 1).ToString(), newFont, Brushes.Black, 3, space);
                graphic.DrawString((xc + points[k].Y).ToString(), newFont, Brushes.Black, 23, space);
                graphic.DrawString((yc - points[k].X).ToString(), newFont, Brushes.Black, 50, space);
                graphic.DrawString(pk[k].ToString(), newFont, Brushes.Black, 112, space);
                graphic.DrawString((2 * (xc + points[k].X)).ToString(), newFont, Brushes.Black, 161, space);
                graphic.DrawString((2 * (yc - points[k].Y)).ToString(), newFont, Brushes.Black, 239, space);
                space += 20;
                l ++;
            }
            for (int m = points.Count - 2; m >= 0; m--)
            {
                graphic.DrawString((l + 1).ToString(), newFont, Brushes.Black, 3, space);;
                graphic.DrawString((xc + points[m].X).ToString(), newFont, Brushes.Black, 23, space);
                graphic.DrawString((yc - points[m].Y).ToString(), newFont, Brushes.Black, 50, space);
                graphic.DrawString(pk[m].ToString(), newFont, Brushes.Black, 112, space);
                graphic.DrawString((2 * (xc + points[m].X)).ToString(), newFont, Brushes.Black, 161, space);
                graphic.DrawString((2 * (xc - points[m].Y)).ToString(), newFont, Brushes.Black, 239, space);
                space += 20;
                l ++;
            }

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            int count =4 * points.Count  - 2;
            
            Graphics graphic = e.Graphics;
            Pen pen = new Pen(Color.Black);
            List<Point> pointscorr = new List<Point>();
            int space = 100;
            Font newFont = new Font("Arial", 9, FontStyle.Regular); 
            for (int k = 0; k < points.Count; k++)
            {
                graphic.DrawString((count+k).ToString(), newFont, Brushes.Black, 3, space);
                graphic.DrawString((xc - points[k].X).ToString(), newFont, Brushes.Black, 32, space);
                graphic.DrawString((yc - points[k].Y).ToString(), newFont, Brushes.Black, 70, space);
                graphic.DrawString(pk[k].ToString(), newFont, Brushes.Black, 136, space);
                graphic.DrawString((2 * (xc - points[k].X)).ToString(), newFont, Brushes.Black, 177, space);
                graphic.DrawString((2 * (xc - points[k].Y)).ToString(), newFont, Brushes.Black, 264, space);
                space += 20;
                y = count+k;
            }
            for (int m = points.Count - 2; m >= 0; m--)
            {
                graphic.DrawString((y + 1).ToString(), newFont, Brushes.Black, 3, space);
                graphic.DrawString((xc - points[m].Y).ToString(), newFont, Brushes.Black, 23, space);
                graphic.DrawString((yc - points[m].X).ToString(), newFont, Brushes.Black, 70, space);
                graphic.DrawString(pk[m].ToString(), newFont, Brushes.Black, 136, space);
                graphic.DrawString((2 * (xc - points[m].X)).ToString(), newFont, Brushes.Black, 177, space);
                graphic.DrawString((2 * (yc - points[m].Y)).ToString(), newFont, Brushes.Black, 264, space);
                space += 20;
                l++;
                y++;
            }  
          for (int m = 1; m < points.Count; m++)
            {
                 graphic.DrawString((y + 1).ToString(), newFont, Brushes.Black, 3, space);
                graphic.DrawString((xc - points[m].Y).ToString(), newFont, Brushes.Black, 32, space);
                graphic.DrawString((yc + points[m].X).ToString(), newFont, Brushes.Black, 70, space);
                graphic.DrawString(pk[m].ToString(), newFont, Brushes.Black, 136, space);
                graphic.DrawString((2 * (xc - points[m].X)).ToString(), newFont, Brushes.Black, 177, space);
                graphic.DrawString((2 * (yc + points[m].Y)).ToString(), newFont, Brushes.Black, 264, space);
                space += 20;
                y++;
            }
            for (int k = points.Count - 2; k >= 0; k--)
            {
                graphic.DrawString((y + 1).ToString(), newFont, Brushes.Black, 3, space);
                graphic.DrawString((xc - points[k].X).ToString(), newFont, Brushes.Black, 32, space);
                graphic.DrawString((yc + points[k].Y).ToString(), newFont, Brushes.Black, 70, space);
                graphic.DrawString(pk[k].ToString(), newFont, Brushes.Black, 136, space);
                graphic.DrawString((2 * (xc - points[k].X)).ToString(), newFont, Brushes.Black, 177, space);
                graphic.DrawString((2 * (xc + points[k].Y)).ToString(), newFont, Brushes.Black, 264, space);
                space += 20;
                y ++;
            }
        }

    }

}

   
       
    
    