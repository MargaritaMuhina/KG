using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) 
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить картинку как...";
                sfd.OverwritePrompt = true; // показывать ли "Перезаписать файл" если пользователь указывает имя файла, который уже существует
                sfd.CheckPathExists = true; // отображает ли диалоговое окно предупреждение, если пользователь указывает путь, который не существует
                                            
                sfd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                sfd.ShowHelp = true; // отображается ли кнопка Справка в диалоговом окне
                                     
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox2.Image.Save(sfd.FileName);
                    }
                    catch 
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch 
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) 
            {
             
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap output = new Bitmap(input.Width, input.Height);
                
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        
                        float R = (float)((pixel & 0x00FF0000) >> 16); 
                        float G = (float)((pixel & 0x0000FF00) >> 8); 
                        float B = (float)(pixel & 0x000000FF); 
                        R = G = B = (R + G + B) / 3.0f;
                        // собираем новый пиксель по частям (по каналам)
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                       
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                
                pictureBox2.Image = output;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) 
            {
                
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap output = new Bitmap(input.Width, input.Height);
                int threshold = 128; // пороговое значение для бинаризации

                for (int j = 0; j < input.Height; j++)
                {
                    for (int i = 0; i < input.Width; i++)
                    {
                        Color originalColor = input.GetPixel(i, j);
                        int grayscale = (int)(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B);

                        if (grayscale >= threshold)
                        {
                            output.SetPixel(i, j, Color.FromArgb(255, 255, 255)); 
                        }
                        else
                        {
                            output.SetPixel(i, j, Color.FromArgb(0, 0, 0)); 
                        }
                    }
                }

                pictureBox2.Image = output; 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap output = new Bitmap(input.Width, input.Height);
                int intensityChange = 50; // константа для изменения интенсивности 

                for (int j = 0; j < input.Height; j++)
                {
                    for (int i = 0; i < input.Width; i++)
                    {
                        Color originalColor = input.GetPixel(i, j);

                        int newR = Math.Max(0, Math.Min(255, originalColor.R + intensityChange));
                        int newG = Math.Max(0, Math.Min(255, originalColor.G + intensityChange));
                        int newB = Math.Max(0, Math.Min(255, originalColor.B + intensityChange));
                        Color newColor = Color.FromArgb(newR, newG, newB);

                        output.SetPixel(i, j, newColor);
                    }
                }

                pictureBox2.Image = output;
            }
        }
    }
}
