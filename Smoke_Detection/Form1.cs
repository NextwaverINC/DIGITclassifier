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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using csmatio.io;
using csmatio.types;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Smoke_Detection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mouseClicked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        Boolean mouseClicked;
        Point startPoint = new Point();
        Point endPoint = new Point();
        Rectangle rectCropArea;
        Rectangle rectCropArea2;
        Rectangle r;
        Rectangle r2;
        Bitmap image;
        Bitmap myBitmap;
        Bitmap myBitmap2;

        int CountWhite = 0;
        int CountWhite2 = 0;
        protected string[] pFileNames;
        protected string[] pFileNames2;
        protected int pCurrentImage = -1;
        protected int pCurrentImage2 = -1;

        string[] dirs;
        string[] dirs2;

        Rectangle[] cr = new Rectangle[10];
        Rectangle rcrop2 = new Rectangle();
        int numcr = 0;

        private void Split()
        {
            int countBpx = 0;
            int Numsc = 0;
            int Numrec = 0;
            int count = 0;
            int count2 = 0;

            Numcount();
            dirs = Directory.GetFiles(@"D:\Deep_learning\Own_Data\Colum1\", "*.png");
            dirs2 = Directory.GetFiles(@"Image_Test/", "*.png");
            count = dirs.Length;
            count2 = dirs2.Length;
            lbStatus.Text = "Processing";
            Bitmap Cropimage;

            for (int i = 0; i < ptbCrop2.Height; i++)
            {
                for (int j = 0; j < ptbCrop2.Width; j++)
                {
                    if (((Bitmap)ptbCrop2.Image).GetPixel(j, i).ToArgb() == Color.Black.ToArgb())
                    {
                        countBpx++;
                    }
                }
            }
            int Avg = countBpx / ptbCrop2.Height;
            lbShowr.Text = Avg.ToString();

            countBpx = 0;

            for (int i = 0; i < ptbCrop2.Height; i++)
            {
                countBpx = 0;
                for (int j = 0; j < ptbCrop2.Width; j++)
                {
                    if (((Bitmap)ptbCrop2.Image).GetPixel(j, i).ToArgb() == Color.Black.ToArgb())
                    {
                        countBpx++;
                    }
                }

                if (countBpx >= Avg)
                {
                    Numsc++;

                    if (Numsc >= 8)
                    {
                        if (i < ptbCrop2.Height)
                        {
                            rcrop2 = new Rectangle(0, i - 20, ptbCrop2.Width, 40);
                            int numrec = 0;
                            Graphics g2 = ptbCrop2.CreateGraphics();
                            Brush redBrush = new SolidBrush(Color.Red);
                            Pen pen = new Pen(redBrush, 1);
                            g2.DrawRectangle(pen, rcrop2);
                            i = i + 30;
                            if (rcrop2.Y < 0)
                            {
                                rcrop2.Y = 0;
                            }
                            if (rcrop2.Y > 800)
                            {
                                rcrop2.Y = 800;
                                rcrop2.Height = ptbCrop2.Height - 800; 
                            }

                            if (rcrop2.Height > 0 && rcrop2.Y <= 800)
                            {
                                //Cropimage = myBitmap.Clone(r, ((Bitmap)ptbCrop.Image).PixelFormat);
                                //Cropimage.Save("D:/Deep_learning/Own_Data/Colum1/" + (count += 1) + ".jpg");

                                Cropimage = myBitmap2.Clone(rcrop2, ((Bitmap)ptbCrop2.Image).PixelFormat);
                                Cropimage.Save("Image_Test/" + (count2 += 1) + ".png");
                                Cropimage.Dispose();
                            }
                        }


                        Numrec++;

                    }
                }
                else
                {
                    Numsc = 0;
                }
            }




        }

        private void Split_for_Reg()
        {
            int countBpx = 0;
            int Numsc = 0;
            int Numrec = 0;
            int Namepic = 0;

            Numcount();
            lbStatus.Text = "Processing";
            Bitmap Cropimage;

            for (int i = 0; i < ptbCrop2.Height; i++)
            {
                for (int j = 0; j < ptbCrop2.Width; j++)
                {
                    if (((Bitmap)ptbCrop2.Image).GetPixel(j, i).ToArgb() == Color.Black.ToArgb())
                    {
                        countBpx++;
                    }
                }
            }
            int Avg = countBpx / ptbCrop2.Height;
            lbShowr.Text = Avg.ToString();

            countBpx = 0;

            for (int i = 0; i < ptbCrop2.Height; i++)
            {
                countBpx = 0;
                for (int j = 0; j < ptbCrop2.Width; j++)
                {
                    if (((Bitmap)ptbCrop2.Image).GetPixel(j, i).ToArgb() == Color.Black.ToArgb())
                    {
                        countBpx++;
                    }
                }

                if (countBpx >= Avg)
                {
                    Numsc++;

                    if (Numsc >= 8)
                    {
                        if (i < ptbCrop2.Height)
                        {
                            rcrop2 = new Rectangle(0, i - 20, ptbCrop2.Width, 40);
                            Graphics g2 = ptbCrop2.CreateGraphics();
                            Brush redBrush = new SolidBrush(Color.Red);
                            Pen pen = new Pen(redBrush, 1);
                            g2.DrawRectangle(pen, rcrop2);
                            i = i + 30;
                            if (rcrop2.Y < 0)
                            {
                                rcrop2.Y = 0;
                            }
                            if (rcrop2.Y > 800)
                            {
                                rcrop2.Y = 800; 
                            }

                            if (rcrop2.Height > 0 && rcrop2.Y <= 800)
                            {
                                Namepic += 1;
                                Cropimage = myBitmap2.Clone(rcrop2, ((Bitmap)ptbCrop2.Image).PixelFormat);
                                Cropimage.Save("Image/" + Namepic  + ".png");
                                Cropimage.Dispose();
                            }
                        }


                        Numrec++;

                    }
                }
                else
                {
                    Numsc = 0;
                }
            }




        }


        private void Cal(int Value)
        {

            numcr = 0;
            textBox1.Text = "";
            ptbHis.Refresh();
            Array.Clear(cr, 0, cr.Length);

            if (rdoStraing.Checked)
            {
                Straning();
            }
            else if (rdoRotate.Checked)
            {
                Rotate(0);
            }



            if (txtHisfile.Text != "")
            {
                int countpx = 0, average = 0;
                chartHis.Series["His"].Points.Clear();
                for (int i = 0; i < Hisbit.Width; i++)
                {
                    countpx = 0;
                    for (int j = 0; j < Hisbit.Height; j++)
                    {
                        if (Hisbit.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                        {
                            countpx++;
                            average++;

                        }
                    }
                    chartHis.Series["His"].Points.AddXY(i, countpx);

                }
                average = average / Hisbit.Width;
                lbArg.Text = "ค่าเฉลี่ย = " + average.ToString();



                //----------------------------------------------------------------------------------------------------------------------------
                int checkstart = 0;
                int start = 0;
                int countpxcal, averagecal = average;
                lbArg.Text = "ค่าเฉลี่ย = " + averagecal.ToString();

                while (start < Hisbit.Width)
                {
                    countpxcal = 0;

                    for (int j = 0; j < Hisbit.Height; j++)
                    {
                        if (Hisbit.GetPixel(start, j).ToArgb() == Color.Black.ToArgb())
                        {
                            countpxcal++;
                        }
                    }

                    textBox1.Text += "W " + start + " = " + countpxcal + "\r\n ";

                    if (checkstart == 1 && countpxcal >= 15)
                    {
                        start = start + 1000;
                        continue;
                    }
                    if (checkstart == 1 && countpxcal >= averagecal && countpxcal <= 15 && numcr < 10)
                    {

                        start = Crop(countpxcal, start, averagecal);
                        numcr++;

                    }
                    else
                    {
                        start += 1;
                    }

                    if (countpxcal >= 15 && checkstart == 0)
                    {
                        checkstart = 1;
                        start += 6;
                        //continue; 
                    }
                }

                Data();
            }// end not null

            btnSave.Enabled = true;
        }

        private void Data()
        {
            var lbnum = new List<Control>();
            lbnum.Add(lbNum1);
            lbnum.Add(lbNum2);
            lbnum.Add(lbNum3);
            lbnum.Add(lbNum4);
            lbnum.Add(lbNum5);
            lbnum.Add(lbNum6);
            lbnum.Add(lbNum7);
            lbnum.Add(lbNum8);
            lbnum.Add(lbNum9);
            lbnum.Add(lbNum10);

            var lbX = new List<Control>();
            lbX.Add(lbX1);
            lbX.Add(lbX2);
            lbX.Add(lbX3);
            lbX.Add(lbX4);
            lbX.Add(lbX5);
            lbX.Add(lbX6);
            lbX.Add(lbX7);
            lbX.Add(lbX8);
            lbX.Add(lbX9);
            lbX.Add(lbX10);

            var lbY = new List<Control>();
            lbY.Add(lbY1);
            lbY.Add(lbY2);
            lbY.Add(lbY3);
            lbY.Add(lbY4);
            lbY.Add(lbY5);
            lbY.Add(lbY6);
            lbY.Add(lbY7);
            lbY.Add(lbY8);
            lbY.Add(lbY9);
            lbY.Add(lbY10);

            var lbW = new List<Control>();
            lbW.Add(lbW1);
            lbW.Add(lbW2);
            lbW.Add(lbW3);
            lbW.Add(lbW4);
            lbW.Add(lbW5);
            lbW.Add(lbW6);
            lbW.Add(lbW7);
            lbW.Add(lbW8);
            lbW.Add(lbW9);
            lbW.Add(lbW10);

            var lbH = new List<Control>();
            lbH.Add(lbH1);
            lbH.Add(lbH2);
            lbH.Add(lbH3);
            lbH.Add(lbH4);
            lbH.Add(lbH5);
            lbH.Add(lbH6);
            lbH.Add(lbH7);
            lbH.Add(lbH8);
            lbH.Add(lbH9);
            lbH.Add(lbH10);

            var ptbhiscrop = new List<PictureBox>();
            ptbhiscrop.Add(ptbShowcase1);
            ptbhiscrop.Add(ptbShowcase2);
            ptbhiscrop.Add(ptbShowcase3);
            ptbhiscrop.Add(ptbShowcase4);
            ptbhiscrop.Add(ptbShowcase5);
            ptbhiscrop.Add(ptbShowcase6);
            ptbhiscrop.Add(ptbShowcase7);
            ptbhiscrop.Add(ptbShowcase8);
            ptbhiscrop.Add(ptbShowcase9);
            ptbhiscrop.Add(ptbShowcase10);

            var btncel = new List<Button>();
            btncel.Add(btndel1);
            btncel.Add(btndel2);
            btncel.Add(btndel3);
            btncel.Add(btndel4);
            btncel.Add(btndel5);
            btncel.Add(btndel6);
            btncel.Add(btndel7);
            btncel.Add(btndel8);
            btncel.Add(btndel9);
            btncel.Add(btndel10);

            var txtchar = new List<TextBox>();
            txtchar.Add(txtChar1);
            txtchar.Add(txtChar2);
            txtchar.Add(txtChar3);
            txtchar.Add(txtChar4);
            txtchar.Add(txtChar5);
            txtchar.Add(txtChar6);
            txtchar.Add(txtChar7);
            txtchar.Add(txtChar8);
            txtchar.Add(txtChar9);
            txtchar.Add(txtChar10);

            for (int i = 0; i < 10; i++)
            {
                lbnum[i].Text = "--";
                lbX[i].Text = "--";
                lbY[i].Text = "--";
                lbW[i].Text = "--";
                lbH[i].Text = "--";
                btncel[i].Enabled = false;
                txtchar[i].Enabled = false;
                txtchar[i].Text = "";
                ptbhiscrop[i].Image = null;

            }

            Graphics gr = ptbHis.CreateGraphics();
            Brush redbrush = new SolidBrush(Color.Red);
            Pen pen = new Pen(redbrush, 1);

            if (numcr >= 1)
            {
                if (rdoManual.Checked)
                {
                    for (int numr = 0; numr <= numcr; numr++)
                    {
                        gr.DrawRectangle(pen, cr[numr]);
                        lbnum[numr].Text = numr.ToString();
                        lbX[numr].Text = cr[numr].X.ToString();
                        lbY[numr].Text = cr[numr].Y.ToString();
                        lbW[numr].Text = cr[numr].Width.ToString();
                        lbH[numr].Text = cr[numr].Height.ToString();
                        btncel[numr].Enabled = Enabled;
                        txtchar[numr].Enabled = Enabled;
                        Bitmap sourceBitmap = new Bitmap(ptbHis.Image, ptbHis.Width, ptbHis.Height);
                        Graphics g = ptbhiscrop[numr].CreateGraphics();

                        if (cr[numr].X + cr[numr].Width > Hisbit.Width)
                        {
                            cr[numr].X = Hisbit.Width - cr[numr].X;
                        }

                        myBitmap = new Bitmap(ptbhiscrop[numr].Width, ptbhiscrop[numr].Height, g);
                        myBitmap = sourceBitmap.Clone(cr[numr], sourceBitmap.PixelFormat);
                        ptbhiscrop[numr].Image = myBitmap;
                        sourceBitmap.Dispose();
                    }
                }
                else
                {
                    for (int numr = 0; numr < numcr; numr++)
                    {
                        if (cr[numr].Width <= 0)
                        {
                            Array.Clear(cr, numr, 1);
                            continue;
                        }
                        gr.DrawRectangle(pen, cr[numr]);
                        lbnum[numr].Text = numr.ToString();
                        lbX[numr].Text = cr[numr].X.ToString();
                        lbY[numr].Text = cr[numr].Y.ToString();
                        lbW[numr].Text = cr[numr].Width.ToString();
                        lbH[numr].Text = cr[numr].Height.ToString();
                        btncel[numr].Enabled = Enabled;
                        txtchar[numr].Enabled = Enabled;
                        Bitmap sourceBitmap = new Bitmap(ptbHis.Image, ptbHis.Width, ptbHis.Height);
                        Graphics g = ptbhiscrop[numr].CreateGraphics();

                        if (cr[numr].X + cr[numr].Width > Hisbit.Width)
                        {
                            cr[numr].X = Hisbit.Width - cr[numr].X;
                        }

                        myBitmap = new Bitmap(ptbhiscrop[numr].Width, ptbhiscrop[numr].Height, g);
                        myBitmap = sourceBitmap.Clone(cr[numr], sourceBitmap.PixelFormat);
                        ptbhiscrop[numr].Image = myBitmap;
                        sourceBitmap.Dispose();
                    }
                }
            }

            string[] name = pFileNames2[pCurrentImage2].Split('\\');
            lbPicname.Text = name[name.Length - 1].ToString();

        }


        private void Straning()
        {
            Hisbit = new Bitmap(pFileNames2[pCurrentImage2]);
        }

        private void Rotate(int value)
        {
            int scalewi = 0;
            int scalehi = 0;
            RotateBilinear fillrotate;
            if (hScrollBar1.Value != 0)
            {
                fillrotate = new RotateBilinear(Convert.ToDouble(value), false);
            }
            else
            {
                fillrotate = new RotateBilinear(0, false);
            }

            fillrotate.FillColor = Color.White;

            Hisbit = new Bitmap(pFileNames2[pCurrentImage2]);
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Hisbit = filter.Apply(Hisbit);
            Hisbit = fillrotate.Apply(Hisbit);
            ptbSlove.Image = Hisbit;


            lbWislove.Text = Hisbit.Width.ToString();
            lbHislove.Text = Hisbit.Height.ToString();
            scalehi = ptbSlove.Height - ptbHis.Height;
            scalewi = ptbSlove.Width - ptbHis.Width;

            lbscale.Text = "ความแตกต่าง Height =  " + scalehi + " Width =  " + scalewi;
        }


        public int Crop(int count, int point_i, int Avg)
        {

            int countstop = 0, errorpoint = 0, startpoint = 0, stoppoint = 0;
            int widt = 0;
            int heigt = 0;
            int loopstop = 0;
            bool keepLooping = true;
            if (count >= Avg && startpoint == 0)
            {
                startpoint = point_i;
            }

            int i = startpoint;

            if (startpoint != 0)
            {
                while (i < Hisbit.Width && keepLooping)
                {
                    countstop = 0;

                    for (int j = 0; j < Hisbit.Height; j++)
                    {
                        if (Hisbit.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                        {
                            countstop++;
                        }

                    }
                    if (countstop < Avg)
                    {
                        if (errorpoint == 0)
                        {
                            errorpoint = i;
                            int ctb = 0;
                            int c_ctb = 0;

                            for (int iss = errorpoint; iss < errorpoint + 3; iss++)
                            {
                                if (errorpoint + 3 < Hisbit.Width)
                                {
                                    ctb = 0;
                                    for (int js = 0; js < Hisbit.Height; js++)
                                    {
                                        if (Hisbit.GetPixel(iss, js).ToArgb() == Color.Black.ToArgb())
                                        {
                                            ctb += 1;
                                        }

                                    }

                                    if (ctb < Avg)
                                    {
                                        c_ctb += 1;
                                    }
                                    else
                                    {
                                        i = iss;
                                        break;
                                    }
                                }
                            }
                            if (c_ctb > 2)
                            {
                                stoppoint = errorpoint + 2;
                                errorpoint = 0;
                                break;
                            }

                            else
                            {

                            }

                        }
                        else
                        {
                            stoppoint = i + 2;
                            loopstop = 1;
                            errorpoint = 0;
                            break;
                        }
                    }
                    else
                    {
                        i += 1;
                    }

                    if (loopstop == 1)
                    {
                        break;
                    }
                }
            }

            widt = stoppoint - startpoint;
            heigt = Hisbit.Height;

            if (((startpoint - 7) + (widt + 10)) >= Hisbit.Width)
            {
                widt = (Hisbit.Width - (startpoint - 7));
                cr[numcr] = new Rectangle(startpoint - 7, 0, widt, heigt);
            }
            if (startpoint - 7 <= 0)
            {
                cr[numcr] = new Rectangle(1, 0, widt + 10, heigt);
            }
            else
            {
                cr[numcr] = new Rectangle(startpoint - 7, 0, widt + 10, heigt);
            }



            if (cr[numcr].Width >= 24)
            {
                cr[numcr].Width = (cr[numcr].Width / 2) + 8;

                if (cr[numcr].Width >= 24)
                {
                    cr[numcr].Width = (cr[numcr].Width / 2) + 8;

                    stoppoint = stoppoint - cr[numcr].Width;

                    return ((cr[numcr].X + cr[numcr].Width));
                }

                stoppoint = stoppoint - cr[numcr].Width;
                return ((cr[numcr].X + cr[numcr].Width));
            }
            else
            {
                return ((cr[numcr].X + cr[numcr].Width));
            }

            startpoint = 0;

        }


        private void Numcount()
        {
            try
            {
                // Only get files that begin with the letter "c."
                //Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                }
            }
            catch (Exception e)
            {
                // Console.WriteLine("The process failed: {0}", e.ToString());
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
            ptbShow.Image = gsImage;
        }
        private int pointer(int _point , int _count , int _numline,int _numptb)
        {
            int point = 0;
            int countblack = 0;
            int numline = 0;
            int numptb = 0;

            numptb = _numptb; 
            numline = _numline;
            point = _point;
            countblack = _count;


            Color Checkcolor;

            while (countblack > 10 )
            {
                countblack = 0;

                if (numline % 2 != 0)
                {
                    point -=3 ;
                }
                else
                {
                    point -=3 ;
                }
                if (numptb == 1)
                {
                }
                else
                {
                    for (int o = 0; o < ptbCrop2.Width; o++)
                    {

                        Checkcolor = ((Bitmap)ptbCrop2.Image).GetPixel(o, point);
                        if (Checkcolor.ToArgb() == Color.Black.ToArgb())
                        {

                            countblack += 1;
                        }
                    }
                }
               
            }
            return point;
           
        }

        protected void ShowCurrentImage()
        {
            if (pCurrentImage >= 0 && pCurrentImage <= pFileNames.Length - 1)
            {
               // ptbShow.Image = Bitmap.FromFile(pFileNames[pCurrentImage]);
            }
        }
        protected void ShowCurrentImageHis()
        {
            if (pCurrentImage2 >= 0 && pCurrentImage2 <= pFileNames2.Length - 1)
            {
                // ptbShow.Image = Bitmap.FromFile(pFileNames[pCurrentImage]);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JPEG|*.jpg|Bitmaps|*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pFileNames = openFileDialog.FileNames;
                txtFilePart.Text = pFileNames.Length.ToString();
                pCurrentImage = 0;
                lbNumimg.Text = (pCurrentImage + 1).ToString();
                txtFilePart.Text = pFileNames[pCurrentImage].ToString();
                ShowCurrentImage();
            }
            if (txtFilePart.Text != "")
            {
                Imag_Process();
            }
            else
            {

            }


        }

        private void ptbShow_Click(object sender, EventArgs e)
        {

        }

        private void ptbShow_MouseUp(object sender, MouseEventArgs e)
        {
            mouseClicked = false;

            if (endPoint.X != -1)
            {
                Point currentPoint = new Point(e.X, e.Y);

            }
            endPoint.X = -1;
            endPoint.Y = -1;
            startPoint.X = -1;
            startPoint.Y = -1;
        }

        private void ptbShow_MouseDown(object sender, MouseEventArgs e)
        {
            mouseClicked = true;

            startPoint.X = e.X;
            startPoint.Y = e.Y;
            endPoint.X = -1;
            endPoint.Y = -1;

            rectCropArea = new Rectangle(new Point(e.X, e.Y), new Size());
        }

        private void ptbShow_MouseMove(object sender, MouseEventArgs e)
        {
            Point ptCurrent = new Point(e.X, e.Y);

            if (mouseClicked)
            {
                endPoint = ptCurrent;

                if (e.X > startPoint.X && e.Y > startPoint.Y)
                {
                    rectCropArea.Width = e.X - startPoint.X;
                    rectCropArea.Height = e.Y - startPoint.Y;
                }
                else if (e.X < startPoint.X && e.Y > startPoint.Y)
                {
                    rectCropArea.Width = startPoint.X - e.X;
                    rectCropArea.Height = e.Y - startPoint.Y;
                    rectCropArea.X = e.X;
                    rectCropArea.Y = startPoint.Y;
                }
                else if (e.X > startPoint.X && e.Y < startPoint.Y)
                {
                    rectCropArea.Width = e.X - startPoint.X;
                    rectCropArea.Height = startPoint.Y - e.Y;
                    rectCropArea.X = startPoint.X;
                    rectCropArea.Y = e.Y;
                }
                else
                {
                    rectCropArea.Width = startPoint.X - e.X;
                    rectCropArea.Height = startPoint.Y - e.Y;
                    rectCropArea.X = e.X;
                    rectCropArea.Y = e.Y;
                }
                ptbShow.Refresh();
            }
        }

        private void ptbShow_MouseClick(object sender, MouseEventArgs e)
        {
            ptbShow.Refresh();
        }

        private void ptbShow_Paint(object sender, PaintEventArgs e)
        {
            Pen drawLine = new Pen(Color.Red);
            drawLine.DashStyle = DashStyle.Dash;
            e.Graphics.DrawRectangle(drawLine, rectCropArea);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            lbStatus.Text = "";
            rectCropArea.X = 11;
            rectCropArea.Y = 110;
            rectCropArea.Width = 110;
            rectCropArea.Height = ptbShow.Height - 110;

            rectCropArea2.X = 80;
            rectCropArea2.Y = 110;
            rectCropArea2.Width = 150;
            rectCropArea2.Height = ptbShow.Height - 110;
            ptbCrop2.Refresh();

            if (txtFilePart.Text != "")
            {

                Bitmap sourceBitmap2 = new Bitmap(ptbShow.Image, ptbShow.Width, ptbShow.Height);
                Graphics g2 = ptbCrop2.CreateGraphics();
                myBitmap2 = new Bitmap(ptbCrop2.Width, ptbCrop2.Height, g2);
                ptbShow.Refresh(); // This repositions the dashed box to new location as per coordinates entered.
                g2.DrawImage(sourceBitmap2, new Rectangle(0, 0, ptbCrop2.Width, ptbCrop2.Height), rectCropArea2, GraphicsUnit.Pixel);
                myBitmap2 = sourceBitmap2.Clone(rectCropArea2, sourceBitmap2.PixelFormat);
                ptbCrop2.Image = myBitmap2;
                sourceBitmap2.Dispose();
            }
            else
            {
                    
            }

            int countpx = 0;
            int countall = 0;

            for (int i = 0; i < ptbCrop2.Height; i++)
            {
                countpx = 0;
                for (int j = 0; j < ptbCrop2.Width; j++)
                {
                    if (((Bitmap)ptbCrop2.Image).GetPixel(j, i).ToArgb() == Color.Black.ToArgb()) 
                    {
                        countpx++;
                        countall++;
                    }
                }
            }

        }


        private void btnDraw_Click(object sender, EventArgs e)
        {
            Split();
            int numrec = 0 ; 
            Graphics g2 = ptbCrop2.CreateGraphics();
            Brush redBrush = new SolidBrush(Color.Red);
            Pen pen = new Pen(redBrush, 1);

            //for (int i = 0; i < rcrop2.Length; i++)
            //{
            //    if (rcrop2[i].Width != 0 )
            //    {
            //        numrec++; 
            //    }
            //}
            //for (int i = 0; i < numrec; i++)
            //{
            //    g2.DrawRectangle(pen, rcrop2[i]);
            //}
            //Array.Clear(rcrop2, 0, cr.Length);


            //int count = 0;
            //int count2 = 0;
            //Numcount();
            //dirs  = Directory.GetFiles(@"D:\Deep_learning\Own_Data\Colum1\", "*.jpg");
            //dirs2  = Directory.GetFiles(@"D:\Deep_learning\Own_Data\Colum2\", "*.jpg");
            //count = dirs.Length;
            //count2 = dirs2.Length;
            //lbStatus.Text = "Processing"; 

            //List<Image> list = new List<Image>();
            //Graphics g = ptbCrop.CreateGraphics();
            //Graphics g2 = ptbCrop2.CreateGraphics();
            //Brush redBrush = new SolidBrush(Color.Red);
            //Pen pen = new Pen(redBrush, 1);
            //Bitmap Cropimage;
            //int plus = 15;


            //for (int i = 0; i < 1; i++)
            //{
            //    for (int y = 0; y < 20; y++)   
            //    {
            //        CountWhite = 0;
            //        CountWhite2 = 0;
            //        if (y < 19 && y > 0)
            //        {
            //            r = new Rectangle(i * ptbCrop.Width,
            //                                  (y * (ptbCrop.Height / 20) + 40),
            //                                   ptbCrop.Width - 1,
            //                                   ptbCrop.Height / 22);

            //            r2 = new Rectangle(i * ptbCrop2.Width,
            //                                  (y * (ptbCrop2.Height / 20) + 30),
            //                                   ptbCrop2.Width - 1,
            //                                   ptbCrop2.Height / 22);
            //        }
            //        else
            //        {
            //            r = new Rectangle(i * ptbCrop.Width,
            //                                  (y * (ptbCrop.Height / 20)+30),
            //                                   ptbCrop.Width - 1,
            //                                   40);

            //            r2 = new Rectangle(i * ptbCrop2.Width,
            //                                (y * (ptbCrop2.Height / 20) + 30),
            //                                 ptbCrop2.Width - 1,
            //                                 40);
            //        }


            //        for (int w = 0; w < ptbCrop.Width; w++)
            //            {
            //                if (y < 19 )
            //                {
            //                    Color Checkcolor;
            //                    Checkcolor = ((Bitmap)ptbCrop.Image).GetPixel(w ,  r.Y); 

            //                    if (Checkcolor.ToArgb() == Color.Black.ToArgb())
            //                    {

            //                        CountWhite += 1;
            //                    }
            //                 }
            //            }
            //        for (int w2 = 0; w2 < ptbCrop2.Width; w2++)
            //        {
            //            Color Checkcolor;
            //            Checkcolor = ((Bitmap)ptbCrop2.Image).GetPixel(w2, r2.Y);

            //            if (Checkcolor.ToArgb() == Color.Black.ToArgb())
            //            {

            //                CountWhite2 += 1;
            //            }
            //        }

            //        if (y<19)
            //        {
            //            if (CountWhite > 10)
            //            {
            //                r.Y = pointer(r.Y, CountWhite, y , 1);
            //            }

            //            if (CountWhite2 > 8)
            //            {
            //                r2.Y = pointer(r2.Y, CountWhite2, y , 2);
            //            }
            //        }


            //        if (r2.Y + r2.Height >= ptbCrop2.Height)
            //        {
            //            r2.Y = r2.Y -23;
            //            r2.Height = ptbCrop2.Height - r2.Y - 2;

            //        }
            //        if (r.Y + r.Height >= ptbCrop.Height)
            //        {
            //            r.Y = r.Y - 23;
            //            r.Height = ptbCrop.Height - r.Y - 2;
            //        }

            //        g.DrawRectangle(pen, r);
            //        g2.DrawRectangle(pen, r2);


            //        Cropimage = myBitmap.Clone(r, ((Bitmap)ptbCrop.Image).PixelFormat);
            //        Cropimage.Save("D:/Deep_learning/Own_Data/Colum1/" + (count+=1) + ".jpg");

            //        Cropimage = myBitmap2.Clone(r2, ((Bitmap)ptbCrop2.Image).PixelFormat);
            //        Cropimage.Save("D:/Deep_learning/Own_Data/Colum2/" + (count2 += 1) + ".jpg");
            //        Cropimage.Dispose();
            //        //list.Add(cropImage(ptbCrop.Width, ptbCrop.Height, r));
            //    }
            //}

            lbStatus.Text = "Split Success"; 
        }


        private static Image cropImage(int a , int b, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(a,b);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, System.Drawing.Imaging.PixelFormat.DontCare);
            return (Image)(bmpCrop);
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (pFileNames.Length > 0)
            {
                pCurrentImage = pCurrentImage == 0 ? pFileNames.Length - 1 : --pCurrentImage;
                lbNumimg.Text = (pCurrentImage + 1).ToString();
                txtFilePart.Text = pFileNames[pCurrentImage].ToString();
                ShowCurrentImage();
                Imag_Process();
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (pFileNames.Length > 0)
            {
                pCurrentImage = pCurrentImage == pFileNames.Length - 1 ? 0 : ++pCurrentImage;
                lbNumimg.Text = (pCurrentImage +1).ToString();
                txtFilePart.Text = pFileNames[pCurrentImage].ToString();
                ShowCurrentImage();
                Imag_Process();
            }
        }
        Bitmap Hisbit;
        private void btnHisfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Multiselect = true;
            openFileDialog2.Filter = "PNG|*.png|Bitmaps|*.bmp";
            string file;
            

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                pFileNames2 = openFileDialog2.FileNames;
                txtHisfile.Text = pFileNames2.Length.ToString();
                pCurrentImage2 = 0;
                txtHisfile.Text = pFileNames2[pCurrentImage2].ToString();
                ShowCurrentImageHis();
                Hisbit = new Bitmap(pFileNames2[pCurrentImage2]);
                lbWi.Text = Hisbit.Width.ToString();
                lbHi.Text = Hisbit.Height.ToString();
                ptbHis.Image = Hisbit;
                ptbSlove.Image = Hisbit;

            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Cal(0);
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



        private void button6_Click(object sender, EventArgs e)
        {
            lbNum6.Text = "--";
            lbX6.Text = "--";
            lbY6.Text = "--";
            lbW6.Text = "--";
            lbH6.Text = "--";
            txtChar6.Text = "";
            txtChar6.Enabled = false;
            btndel6.Enabled = false;
            ptbShowcase6.Image = null;
            Array.Clear(cr, 5, 1);
        }

        private void btndel2_Click(object sender, EventArgs e)
        {
            lbNum2.Text = "--";
            lbX2.Text = "--";
            lbY2.Text = "--";
            lbW2.Text = "--";
            lbH2.Text = "--";
            txtChar2.Text = "";
            txtChar2.Enabled = false;
            btndel2.Enabled = false;
            ptbShowcase2.Image = null;
            Array.Clear(cr, 1,1);
        }

        private void btndel3_Click(object sender, EventArgs e)
        {
            lbNum3.Text = "--";
            lbX3.Text = "--";
            lbY3.Text = "--";
            lbW3.Text = "--";
            lbH3.Text = "--";
            txtChar3.Text = "";
            txtChar3.Enabled = false;
            btndel3.Enabled = false;
            ptbShowcase3.Image = null;
            Array.Clear(cr, 2, 1);
        }

        private void btndel4_Click(object sender, EventArgs e)
        {
            lbNum4.Text = "--";
            lbX4.Text = "--";
            lbY4.Text = "--";
            lbW4.Text = "--";
            lbH4.Text = "--";
            txtChar4.Text = "";
            txtChar4.Enabled = false;
            btndel4.Enabled = false;
            ptbShowcase4.Image = null;
            Array.Clear(cr, 3, 1);
        }

        private void btndel5_Click(object sender, EventArgs e)
        {
            lbNum5.Text = "--";
            lbX5.Text = "--";
            lbY5.Text = "--";
            lbW5.Text = "--";
            lbH5.Text = "--";
            txtChar5.Text = "";
            txtChar5.Enabled = false;
            btndel5.Enabled = false;
            ptbShowcase5.Image = null;
            Array.Clear(cr, 4, 1);
        }

        private void btndel7_Click(object sender, EventArgs e)
        {
            lbNum7.Text = "--";
            lbX7.Text = "--";
            lbY7.Text = "--";
            lbW7.Text = "--";
            lbH7.Text = "--";
            txtChar7.Text = "";
            txtChar7.Enabled = false;
            btndel7.Enabled = false;
            ptbShowcase7.Image = null;
            Array.Clear(cr, 6, 1);
        }

        private void btndel8_Click(object sender, EventArgs e)
        {
            lbNum8.Text = "--";
            lbX8.Text = "--";
            lbY8.Text = "--";
            lbW8.Text = "--";
            lbH8.Text = "--";
            txtChar8.Text = "";
            txtChar8.Enabled = false;
            btndel8.Enabled = false;
            ptbShowcase8.Image = null;
            Array.Clear(cr, 7, 1);
        }

        private void btndel9_Click(object sender, EventArgs e)
        {
            lbNum9.Text = "--";
            lbX9.Text = "--";
            lbY9.Text = "--";
            lbW9.Text = "--";
            lbH9.Text = "--";
            txtChar9.Text = "";
            txtChar9.Enabled = false;
            btndel9.Enabled = false;
            ptbShowcase9.Image = null;
            Array.Clear(cr, 8, 1);
        }

        private void btndel10_Click(object sender, EventArgs e)
        {
            lbNum10.Text = "--";
            lbX10.Text = "--";
            lbY10.Text = "--";
            lbW10.Text = "--";
            lbH10.Text = "--";
            txtChar10.Text = "";
            txtChar10.Enabled = false;
            btndel10.Enabled = false;
            ptbShowcase10.Image = null;
            Array.Clear(cr, 9, 1);
        }

        private void btndel1_Click(object sender, EventArgs e)
        {
            lbNum1.Text = "--";
            lbX1.Text = "--";
            lbY1.Text = "--";
            lbW1.Text = "--";
            lbH1.Text = "--";
            txtChar1.Text = "";
            txtChar1.Enabled = false;
            btndel1.Enabled = false;
            ptbShowcase1.Image = null;
            Array.Clear(cr, 0, 1);
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            var txtchar = new List<TextBox>();
            txtchar.Add(txtChar1);
            txtchar.Add(txtChar2);
            txtchar.Add(txtChar3);
            txtchar.Add(txtChar4);
            txtchar.Add(txtChar5);
            txtchar.Add(txtChar6);
            txtchar.Add(txtChar7);
            txtchar.Add(txtChar8);
            txtchar.Add(txtChar9);
            txtchar.Add(txtChar10);

            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=192.168.1.43;Initial Catalog=digitStruct;User ID=sa;Password=28012013xt";
            cnn = new SqlConnection(connetionString);
            cnn.Open();


            int numroows = 0;
            string select = "select COUNT(*)  from Struct as S Where Name ='" + lbPicname.Text +  "'" ;
            SqlCommand  objCmd = new SqlCommand(select, cnn);
            numroows = Convert.ToInt32(objCmd.ExecuteScalar());
            if (numroows > 0)
            {
                MessageBox.Show("รูปนี้มีซ้ำ หรือมีในระบบแล้ว");
            }
            else
            {
                try
                {
                   
                    for (int i = 0; i < cr.Length; i++)
                    {
                        if (cr[i].Width >= 1)
                        {
                            string query = "INSERT INTO Struct(Name,X,Y,W,H,Label) VALUES (@name,@x,@y,@w,@h,@label)";
                            SqlCommand com = new SqlCommand(query, cnn);
                            com.Parameters.AddWithValue("@name", lbPicname.Text);
                            com.Parameters.AddWithValue("@x", cr[i].X);
                            com.Parameters.AddWithValue("@y", cr[i].Y);
                            com.Parameters.AddWithValue("@w", cr[i].Width);
                            com.Parameters.AddWithValue("@h", cr[i].Height);
                            com.Parameters.AddWithValue("@label", txtchar[i].Text);
                            com.ExecuteNonQuery();

                        }
                    }

                    MessageBox.Show("Save Success");
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not open connection ! ");
                    cnn.Close();
                }
            }

            btnSave.Enabled = false; 
        }

        private void rdoRotate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRotate.Checked)
            {
                txtDegree.Enabled = true;
                hScrollBar1.Enabled = true; 
            }
            else
            {
                txtDegree.Enabled = false;
                hScrollBar1.Enabled = false;
            }
        }
        int barvalue = 0;

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            txtDegree.Text = hScrollBar1.Value.ToString();
            barvalue = hScrollBar1.Value;
            Cal(barvalue);

        }

        private void txtDegree_TextChanged(object sender, EventArgs e)
        {
            if (txtDegree.Text != "")
            {
                Cal(Convert.ToInt32(txtDegree.Text));
                hScrollBar1.Value = Convert.ToInt32(txtDegree.Text) ; 
            }
        }


        private void ptbHis_Click(object sender, EventArgs e)
        {
          
        }
        //------------------------------------------------------------
        int mouseX;
        int mouseY;
        int count = 0;
        //------------------------------------------------------------
        private void Manual(int pointX)
        {
            if (numcr < 10)
            {
                cr[numcr] = new Rectangle(pointX - 11, 0, 22, Hisbit.Height);
                if (pointX <= 12)
                {
                    cr[numcr].X = 0;
                }
                Data();
            }

        }
        //------------------------------------------------------------
        private void ptbHis_MouseUp(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            lbmouse.Text = "X = " + mouseX + " Y = " + mouseY;
            Manual(e.X);
            btnSave.Enabled = true; 
            if (count == 0)
            {
                Rectangle show = new Rectangle(e.X - 11, 0, 22, Hisbit.Height);
                Graphics g2 = ptbHis.CreateGraphics();
                Brush redBrush = new SolidBrush(Color.Red);
                Pen pen = new Pen(redBrush, 1);
                g2.DrawRectangle(pen, show);
                count = 1; 
            }
            numcr += 1;
        }

        private void rdoManual_CheckedChanged(object sender, EventArgs e)
        {
            ptbHis.Refresh();
            Array.Clear(cr, 0, cr.Length);
            numcr = 0;
            count = 0;
            if (rdoManual.Checked)
            {
                ptbHis.Enabled = true;
                btnCreat.Enabled = false;
            }
            else
            {
                ptbHis.Enabled = false;
                btnCreat.Enabled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Array.Clear(cr, 0, cr.Length);
            numcr = 0;
            count = 0;
            Data();
            ptbSlove.Refresh();
            ptbHis.Refresh();
        }

        private void btnPrevi_Click(object sender, EventArgs e)
        {
            if (pFileNames2.Length > 0)
            {
                pCurrentImage2 = pCurrentImage2 == 0 ? pFileNames2.Length - 1 : --pCurrentImage2;
                lbnum.Text = (pCurrentImage2 + 1).ToString();
                txtHisfile.Text = pFileNames2[pCurrentImage2].ToString();
                ShowCurrentImageHis();
                Hisbit = new Bitmap(pFileNames2[pCurrentImage2]);
                ptbHis.Image = Hisbit; 
            }
        }

        private void btnNex_Click(object sender, EventArgs e)
        {
            if (pFileNames2.Length > 0)
            {
                pCurrentImage2 = pCurrentImage2 == pFileNames2.Length - 1 ? 0 : ++pCurrentImage2;
                lbnum.Text = (pCurrentImage2 + 1).ToString();
                txtHisfile.Text = pFileNames2[pCurrentImage2].ToString();
                ShowCurrentImageHis();
                Hisbit = new Bitmap(pFileNames2[pCurrentImage2]);
                ptbHis.Image = Hisbit;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string Part = txtFilePart.Text ;
            string Book;
            string Page;
            string Namepic;

            System.IO.DirectoryInfo di = new DirectoryInfo("Image/");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            // --------- Spit Image ----------------------------------
            Split_for_Reg();
            // ---------End Spit Image ----------------------------------

            // SplitName -----------------------------------------
            string[] parts = Part.Split('\\');
            Namepic = parts[parts.Length - 1].ToString();
            Book = Namepic.Substring(1, 4);
            Page = Namepic.Substring(7, 4);
            //end SplitName-----------------------------------------------------


            // ------------ SQL Select ----------------------------------------------
            using (SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-A8BET9V\\CODEFLOW;Initial Catalog=GunRegBook;User ID=sa;Password=28012013xt"))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select GunRegID FROM Record Where BookNo ='"+ Book + "' AND PageNo = '"+ Page + "'", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Record");
                int i = 0; 
                string[] GunRegID = new string[30];
                foreach (DataRow row in ds.Tables["Record"].Rows)
                {
                    GunRegID[i] = (row["GunRegID"].ToString());
                    i++;
                }
            }
            // ------------End SQL Select ----------------------------------------------


            //----------------- Deep Python ------------------------------------------ 
            dirs = Directory.GetFiles(@"Image\", "*.png");
            count = dirs.Length;
            for (int i = 1; i <= count; i++)
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = @"cmd.exe";
                //cmd.StartInfo.Arguments = "activate tensorflow-gpu2";
                cmd.StartInfo.Arguments = @"/C python inference.py --image C:\\Deep\\Smoke_Detection\\Smoke_Detection\bin\\Debug\\Image\\" + i + ".png";  // <-- This will execute the command and wait to close
                cmd.Start();
                cmd.WaitForExit();
            }
            //-----------------End Deep Python ------------------------------------------ 




        }
    }
}
