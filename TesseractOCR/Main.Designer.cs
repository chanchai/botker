namespace TesseractOCR
{
    partial class Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnDoOCR = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLang = new System.Windows.Forms.ComboBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.btnDoOCR);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboLang);
            this.panel1.Controls.Add(this.btnSelectImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 33);
            this.panel1.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(310, 6);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(280, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // btnDoOCR
            // 
            this.btnDoOCR.Location = new System.Drawing.Point(229, 5);
            this.btnDoOCR.Name = "btnDoOCR";
            this.btnDoOCR.Size = new System.Drawing.Size(75, 23);
            this.btnDoOCR.TabIndex = 3;
            this.btnDoOCR.Text = "Do OCR";
            this.btnDoOCR.UseVisualStyleBackColor = true;
            this.btnDoOCR.Click += new System.EventHandler(this.btnDoOCR_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lang";
            // 
            // cboLang
            // 
            this.cboLang.FormattingEnabled = true;
            this.cboLang.Items.AddRange(new object[] {
            "ENG",
            "FRA"});
            this.cboLang.Location = new System.Drawing.Point(145, 6);
            this.cboLang.Name = "cboLang";
            this.cboLang.Size = new System.Drawing.Size(55, 21);
            this.cboLang.TabIndex = 1;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(3, 4);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(92, 23);
            this.btnSelectImage.TabIndex = 0;
            this.btnSelectImage.Text = "Select image...";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // lstResult
            // 
            this.lstResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstResult.FormattingEnabled = true;
            this.lstResult.Location = new System.Drawing.Point(0, 33);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(145, 420);
            this.lstResult.TabIndex = 1;
            this.lstResult.SelectedIndexChanged += new System.EventHandler(this.lstResult_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image|*.*";
            this.openFileDialog1.Title = "Select image";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(145, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 422);
            this.panel2.TabIndex = 2;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 455);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Tesseract OCR .NET Wrapper demo";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Button btnDoOCR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLang;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel2;
    }
}

