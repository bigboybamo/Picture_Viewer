using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Picture_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void oFD1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void BtnLoadImages_Click(object sender, EventArgs e)
        {
            imageList1.Images.Clear();
            listView1.Clear();

            oFD1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            oFD1.Title = "Open Image Files";
            oFD1.Filter = "JPEGS|*.jpg|GIFS|*.gif|PNG|*.png";

            DialogResult ofdResults = oFD1.ShowDialog();

            if (ofdResults == DialogResult.Cancel)
            {

                return;

            }

            try
            {

                int numOfFiles = oFD1.FileNames.Length;
                string[] aryFilePaths = new string[numOfFiles];
                int counter = 0;

                foreach (string singleFile in oFD1.FileNames)
                {
                    aryFilePaths[counter] = singleFile;
                    imageList1.Images.Add(Image.FromFile(singleFile));
                    counter++;
                }
                listView1.LargeImageList = imageList1;
                for (int i = 0; i < counter; i++)
                {

                    listView1.Items.Add(aryFilePaths[i], i);

                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOriginalImage();
        }

        private void GetOriginalImage()
        {
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                string bigFileName = listView1.SelectedItems[i].Text;
                pictureBox1.Image = Image.FromFile(bigFileName);
                panel1.AutoScrollMinSize = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);

            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                GetZoom(2);

            }
            else if (e.Button == MouseButtons.Left)
            {

                GetOriginalImage();

            }
        }

        private void GetZoom(int zoomSize)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            int newWidth = pictureBox1.Image.Width / zoomSize;
            int newHeight = pictureBox1.Image.Height / zoomSize;
            Bitmap bmpNew = new Bitmap(newWidth, newHeight);
            Graphics gr = Graphics.FromImage(bmpNew);
            gr.DrawImage(bmp, 0, 0, bmpNew.Width, bmpNew.Height);
            pictureBox1.Image = bmpNew;
            panel1.AutoScrollMinSize = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
        }
    }
}
