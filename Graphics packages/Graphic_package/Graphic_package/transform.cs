using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Graphic_package
{
    public partial class transform : Form
    {
        private static Bitmap originalImage;
        public transform()
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
            pictureBox1.Image = null;
            originalImage = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {   
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Image Files (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog1.FileName);
                int newWidth = 732;
                int newHeight = 571;
                Bitmap stretchedImage = new Bitmap(originalImage, newWidth, newHeight);
                originalImage = stretchedImage; 
                pictureBox1.Image = stretchedImage;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (originalImage != null)
                {
                    var angle = float.Parse(textBox1.Text);
                    RotateImage((Bitmap)pictureBox1.Image, angle, pictureBox1);
                }
                else
                {
                    MessageBox.Show("please upload picture first ");
                }
            }
            else
            {
                MessageBox.Show("please input angle");
            }
        }
        public static void RotateImage(Bitmap image, float angle, PictureBox pictureBox)
        {          
                Bitmap rotatedImage = new Bitmap(originalImage.Width, originalImage.Height);
                float cx = originalImage.Width / 2f;
                float cy = originalImage.Height / 2f;
                float radians = (float)(angle * Math.PI / 180);
                for (int x = 0; x < originalImage.Width; x++)
                {
                    for (int y = 0; y < originalImage.Height; y++)
                    {
                        float newX = (float)(Math.Cos(radians) * (x - cx) - Math.Sin(radians) * (y - cy) + cx);
                        float newY = (float)(Math.Sin(radians) * (x - cx) + Math.Cos(radians) * (y - cy) + cy);

                        if (newX >= 0 && newX < originalImage.Width && newY >= 0 && newY < originalImage.Height)
                        {
                            
                            Color pixelColor = originalImage.GetPixel(x, y);

                            rotatedImage.SetPixel((int)newX, (int)newY, pixelColor);
                        }
                    }
                }

            pictureBox.Image = rotatedImage;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog.FileName + "." + ImageFormat.Jpeg);
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                if (originalImage == null)
                {
                    MessageBox.Show("Please load an image first.");
                    return;
                }
                if (int.TryParse(textBox4.Text, out int reflectionAngle))
                {
                    Bitmap reflectedImage = ReflectImage(originalImage, reflectionAngle);
                    pictureBox1.Image = reflectedImage;
                }
            }
            else
            {
                MessageBox.Show("please input angle");
            }
        }
        private Bitmap ReflectImage(Bitmap originalImage, int reflectionAngle)
        {
            Bitmap reflectedImage = new Bitmap(originalImage.Width, originalImage.Height);
            double angleRadians = Math.PI * reflectionAngle / 180.0;
            double cosTheta = Math.Cos(2 * angleRadians);
            double sinTheta = Math.Sin(2 * angleRadians);
            int centerX = originalImage.Width / 2;
            int centerY = originalImage.Height / 2;
            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    int sourceX = (int)((x - centerX) * cosTheta + (y - centerY) * sinTheta + centerX);
                    int sourceY = (int)((y - centerY) * cosTheta - (x - centerX) * sinTheta + centerY);

                    if (sourceX >= 0 && sourceX < originalImage.Width && sourceY >= 0 && sourceY < originalImage.Height)
                    {
                        reflectedImage.SetPixel(x, y, originalImage.GetPixel(sourceX, sourceY));
                    }
                    else
                    {
                        reflectedImage.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return reflectedImage;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int x = int.Parse(textBox2.Text);
                int y = int.Parse(textBox3.Text);
                TranslateImage((Bitmap)pictureBox1.Image, x, y, pictureBox1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        public void TranslateImage(Bitmap image, int X, int Y, PictureBox pictureBox)
        {
            int width = image.Width;
            int height = image.Height;
            int translatedWidth = width + X;
            int translatedHeight = height + Y;
            Bitmap translatedImage = new Bitmap(translatedWidth, translatedHeight);
            for (int h = 0; h < width; h++)
            {
                for (int w = 0; w < height; w++)
                {
                    int translatedX = h + X;
                    int translatedY = w + Y;
                    if (translatedX >= 0 && translatedX < translatedWidth && translatedY >= 0 && translatedY < translatedHeight)
                    {

                        Color color = image.GetPixel(h, w);
                        translatedImage.SetPixel(translatedX, translatedY, color);
                    }
                }
            }
            pictureBox.Image = translatedImage;
        }
       private void button8_Click(object sender, EventArgs e)
       {
             if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int newX, newY;
            if (!int.TryParse(textBox5.Text, out newX) || !int.TryParse(textBox6.Text, out newY))
            {
                MessageBox.Show("Please enter valid integer values for X and Y.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {      
                pictureBox1.Invalidate();
                Bitmap scaledImage = new Bitmap(originalImage, new Size(newX, newY));
                pictureBox1.Image = scaledImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error scaling image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox7.Text))
            {
                if (originalImage == null)
                {
                    MessageBox.Show("Please load an image first.");
                    return;
                }
                
                    if (double.TryParse(textBox7.Text, out double shearFactor))
                    {

                        Bitmap shearedImage = ShearImage(originalImage, shearFactor);
                        pictureBox1.Image = shearedImage;
                    }
                }
             else
                {
                    MessageBox.Show("enter sheared factor first");
                }
            
        }
        private Bitmap ShearImage(Bitmap originalImage, double shearFactor)
        {
            Bitmap shearedImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    int sourceX = (int)(x + shearFactor * y);
                    int sourceY = y;

                    if (sourceX >= 0 && sourceX < originalImage.Width && sourceY >= 0 && sourceY < originalImage.Height)
                    {
                        shearedImage.SetPixel(x, y, originalImage.GetPixel(sourceX, sourceY));
                    }
                    else
                    {
                        shearedImage.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return shearedImage;
        }
    }
}



