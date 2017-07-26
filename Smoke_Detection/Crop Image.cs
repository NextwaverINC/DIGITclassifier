using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using AForge.Imaging;
using System.Drawing.Imaging;
using AForge;

namespace Smoke_Detection
{
    public partial class Crop_Image : Form
    {
        public Crop_Image()
        {
            InitializeComponent();
        }


        Bitmap orgimg;
        Bitmap image;
        protected string[] pFileNames;
        protected int pCurrentImage = -1;

        protected void ShowCurrentImage()
        {
            if (pCurrentImage >= 0 && pCurrentImage <= pFileNames.Length - 1)
            {
                // ptbShow.Image = Bitmap.FromFile(pFileNames[pCurrentImage]);
            }
        }

        private void Imag_Process()
        {
            image = new Bitmap(pFileNames[pCurrentImage]);
            Bitmap gsImage = Grayscale.CommonAlgorithms.BT709.Apply(image);
            Erosion erod = new Erosion();
            Dilatation dilate = new Dilatation();

            Threshold ths = new Threshold(120);
            gsImage = ths.Apply(gsImage);
            gsImage = dilate.Apply(gsImage);
            gsImage = erod.Apply(gsImage);
            gsImage = erod.Apply(gsImage);
            //gsImage = dilate.Apply(gsImage);
            // gsImage = erod.Apply(gsImage);
            //gsImage = erod.Apply(gsImage);
            ptbImageshow.Image = gsImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JPEG|*.jpg|Bitmaps|*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pFileNames = openFileDialog.FileNames;
                txtFilepath.Text = pFileNames.Length.ToString();
                pCurrentImage = 0;
                lbNumpic.Text = (pCurrentImage + 1).ToString();
                txtFilepath.Text = pFileNames[pCurrentImage].ToString();
                ShowCurrentImage();
            }
            if (txtFilepath.Text != "")
            {
                Imag_Process();
            }
            else
            {

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (pFileNames.Length > 0)
            {
                pCurrentImage = pCurrentImage == pFileNames.Length - 1 ? 0 : ++pCurrentImage;
                lbNumpic.Text = (pCurrentImage + 1).ToString();
                txtFilepath.Text = pFileNames[pCurrentImage].ToString();
                ShowCurrentImage();
                Imag_Process();
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (pFileNames.Length > 0)
            {
                pCurrentImage = pCurrentImage == 0 ? pFileNames.Length - 1 : --pCurrentImage;
                lbNumpic.Text = (pCurrentImage + 1).ToString();
                txtFilepath.Text = pFileNames[pCurrentImage].ToString();
                ShowCurrentImage();
                Imag_Process();
            }
        }
    }
}
