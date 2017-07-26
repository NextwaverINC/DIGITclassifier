namespace Smoke_Detection
{
    partial class Crop_Image
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ptbImageshow = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbNumpic = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ptbImageshow)).BeginInit();
            this.SuspendLayout();
            // 
            // ptbImageshow
            // 
            this.ptbImageshow.Location = new System.Drawing.Point(12, 78);
            this.ptbImageshow.Name = "ptbImageshow";
            this.ptbImageshow.Size = new System.Drawing.Size(788, 974);
            this.ptbImageshow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbImageshow.TabIndex = 0;
            this.ptbImageshow.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "File Path";
            // 
            // txtFilepath
            // 
            this.txtFilepath.Location = new System.Drawing.Point(99, 12);
            this.txtFilepath.Multiline = true;
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.Size = new System.Drawing.Size(631, 20);
            this.txtFilepath.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(736, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbNumpic
            // 
            this.lbNumpic.AutoSize = true;
            this.lbNumpic.Location = new System.Drawing.Point(259, 49);
            this.lbNumpic.Name = "lbNumpic";
            this.lbNumpic.Size = new System.Drawing.Size(10, 13);
            this.lbNumpic.TabIndex = 8;
            this.lbNumpic.Text = "-";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(348, 44);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "Next >>>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(99, 44);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(75, 23);
            this.btnPre.TabIndex = 10;
            this.btnPre.Text = "<<< Previos";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // Crop_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 1053);
            this.Controls.Add(this.btnPre);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lbNumpic);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFilepath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ptbImageshow);
            this.Name = "Crop_Image";
            this.Text = "Crop_Image";
            ((System.ComponentModel.ISupportInitialize)(this.ptbImageshow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ptbImageshow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilepath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbNumpic;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
    }
}