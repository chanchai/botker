using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Drawing.Imaging;


namespace CodeReflection.ScreenCapturingDemo
{
	/// <summary>
	/// Summary description for SpyWindow.
	/// </summary>
	public class SpyWindow : System.Windows.Forms.Form
	{			
		public event DisplayImageEventHandler ImageReadyForDisplay;
		private bool _capturing;
		private Image _finderHome;
		private Image _finderGone;		
		private Cursor _cursorDefault;
		private Cursor _cursorFinder;
		private IntPtr _hPreviousWindow;

		private System.Windows.Forms.Button _buttonOK;
		private System.Windows.Forms.Button _buttonCancel;
		private System.Windows.Forms.Button _buttonCapture;
		private System.Windows.Forms.PictureBox _pictureBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox _textBoxRect;
		private System.Windows.Forms.TextBox _textBoxStyle;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox _textBoxHandle;
		private System.Windows.Forms.TextBox _textBoxClass;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _textBoxText;
		private System.Windows.Forms.Label label2;
        private ListBox listBox1;		
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the SpyWindow class
		/// </summary>
		public SpyWindow()
		{		
			this.InitializeComponent();	
			
			_cursorDefault = Cursor.Current;
			_cursorFinder = EmbeddedResources.LoadCursor(EmbeddedResources.Finder);
			_finderHome = EmbeddedResources.LoadImage(EmbeddedResources.FinderHome);
			_finderGone = EmbeddedResources.LoadImage(EmbeddedResources.FinderGone);
			
			_pictureBox.Image = _finderHome;
			_pictureBox.MouseDown += new MouseEventHandler(OnFinderToolMouseDown);	
			_buttonOK.Click += new EventHandler(OnButtonOKClicked);
			_buttonCancel.Click += new EventHandler(OnButtonCancelClicked);
			_buttonCapture.Click += new EventHandler(OnButtonCaptureClicked);
			_buttonCapture.Enabled = false;
			_textBoxHandle.TextChanged += new EventHandler(OnTextBoxHandleTextChanged);

			this.AcceptButton = _buttonOK;
			this.CancelButton = _buttonCancel;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();

					if (_capturing)
						this.CaptureMouse(false);
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
this._buttonOK = new System.Windows.Forms.Button();
this._buttonCancel = new System.Windows.Forms.Button();
this._buttonCapture = new System.Windows.Forms.Button();
this._pictureBox = new System.Windows.Forms.PictureBox();
this.label7 = new System.Windows.Forms.Label();
this.label6 = new System.Windows.Forms.Label();
this._textBoxRect = new System.Windows.Forms.TextBox();
this._textBoxStyle = new System.Windows.Forms.TextBox();
this.label5 = new System.Windows.Forms.Label();
this.label4 = new System.Windows.Forms.Label();
this.label3 = new System.Windows.Forms.Label();
this._textBoxHandle = new System.Windows.Forms.TextBox();
this._textBoxClass = new System.Windows.Forms.TextBox();
this.label1 = new System.Windows.Forms.Label();
this._textBoxText = new System.Windows.Forms.TextBox();
this.label2 = new System.Windows.Forms.Label();
this.listBox1 = new System.Windows.Forms.ListBox();
((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
this.SuspendLayout();
// 
// _buttonOK
// 
this._buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
this._buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
this._buttonOK.Location = new System.Drawing.Point(195, 240);
this._buttonOK.Name = "_buttonOK";
this._buttonOK.Size = new System.Drawing.Size(75, 23);
this._buttonOK.TabIndex = 7;
this._buttonOK.Text = "OK";
// 
// _buttonCancel
// 
this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
this._buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
this._buttonCancel.Location = new System.Drawing.Point(275, 240);
this._buttonCancel.Name = "_buttonCancel";
this._buttonCancel.Size = new System.Drawing.Size(75, 23);
this._buttonCancel.TabIndex = 8;
this._buttonCancel.Text = "Cancel";
// 
// _buttonCapture
// 
this._buttonCapture.FlatStyle = System.Windows.Forms.FlatStyle.System;
this._buttonCapture.Location = new System.Drawing.Point(275, 100);
this._buttonCapture.Name = "_buttonCapture";
this._buttonCapture.Size = new System.Drawing.Size(75, 25);
this._buttonCapture.TabIndex = 14;
this._buttonCapture.Text = "Capture";
// 
// _pictureBox
// 
this._pictureBox.Location = new System.Drawing.Point(90, 60);
this._pictureBox.Name = "_pictureBox";
this._pictureBox.Size = new System.Drawing.Size(32, 32);
this._pictureBox.TabIndex = 27;
this._pictureBox.TabStop = false;
// 
// label7
// 
this.label7.Location = new System.Drawing.Point(15, 65);
this.label7.Name = "label7";
this.label7.Size = new System.Drawing.Size(70, 20);
this.label7.TabIndex = 26;
this.label7.Text = "Finder Tool:";
// 
// label6
// 
this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
this.label6.Location = new System.Drawing.Point(10, 10);
this.label6.Name = "label6";
this.label6.Size = new System.Drawing.Size(345, 40);
this.label6.TabIndex = 25;
this.label6.Text = "Drag the Finder Tool over a window to select it, then release the mouse button. O" +
    "r enter a window handle (in hexadecimal).";
// 
// _textBoxRect
// 
this._textBoxRect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
this._textBoxRect.BackColor = System.Drawing.SystemColors.Window;
this._textBoxRect.Location = new System.Drawing.Point(65, 412);
this._textBoxRect.Name = "_textBoxRect";
this._textBoxRect.Size = new System.Drawing.Size(285, 20);
this._textBoxRect.TabIndex = 24;
// 
// _textBoxStyle
// 
this._textBoxStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
this._textBoxStyle.BackColor = System.Drawing.SystemColors.Window;
this._textBoxStyle.Location = new System.Drawing.Point(65, 387);
this._textBoxStyle.Name = "_textBoxStyle";
this._textBoxStyle.Size = new System.Drawing.Size(285, 20);
this._textBoxStyle.TabIndex = 23;
// 
// label5
// 
this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
this.label5.Location = new System.Drawing.Point(10, 412);
this.label5.Name = "label5";
this.label5.Size = new System.Drawing.Size(55, 20);
this.label5.TabIndex = 22;
this.label5.Text = "Rect:";
// 
// label4
// 
this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
this.label4.Location = new System.Drawing.Point(10, 387);
this.label4.Name = "label4";
this.label4.Size = new System.Drawing.Size(55, 20);
this.label4.TabIndex = 21;
this.label4.Text = "Style:";
// 
// label3
// 
this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
this.label3.Location = new System.Drawing.Point(10, 337);
this.label3.Name = "label3";
this.label3.Size = new System.Drawing.Size(55, 20);
this.label3.TabIndex = 17;
this.label3.Text = "Caption:";
// 
// _textBoxHandle
// 
this._textBoxHandle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
this._textBoxHandle.BackColor = System.Drawing.SystemColors.Window;
this._textBoxHandle.Location = new System.Drawing.Point(65, 312);
this._textBoxHandle.Name = "_textBoxHandle";
this._textBoxHandle.Size = new System.Drawing.Size(200, 20);
this._textBoxHandle.TabIndex = 18;
// 
// _textBoxClass
// 
this._textBoxClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
this._textBoxClass.BackColor = System.Drawing.SystemColors.Window;
this._textBoxClass.Location = new System.Drawing.Point(65, 362);
this._textBoxClass.Name = "_textBoxClass";
this._textBoxClass.Size = new System.Drawing.Size(285, 20);
this._textBoxClass.TabIndex = 19;
// 
// label1
// 
this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
this.label1.Location = new System.Drawing.Point(10, 312);
this.label1.Name = "label1";
this.label1.Size = new System.Drawing.Size(55, 20);
this.label1.TabIndex = 15;
this.label1.Text = "Handle:";
// 
// _textBoxText
// 
this._textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
this._textBoxText.BackColor = System.Drawing.SystemColors.Window;
this._textBoxText.Location = new System.Drawing.Point(65, 337);
this._textBoxText.Name = "_textBoxText";
this._textBoxText.Size = new System.Drawing.Size(285, 20);
this._textBoxText.TabIndex = 20;
// 
// label2
// 
this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
this.label2.Location = new System.Drawing.Point(10, 362);
this.label2.Name = "label2";
this.label2.Size = new System.Drawing.Size(55, 20);
this.label2.TabIndex = 16;
this.label2.Text = "Class:";
// 
// listBox1
// 
this.listBox1.FormattingEnabled = true;
this.listBox1.Location = new System.Drawing.Point(23, 122);
this.listBox1.Name = "listBox1";
this.listBox1.Size = new System.Drawing.Size(172, 173);
this.listBox1.TabIndex = 28;
// 
// SpyWindow
// 
this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
this.ClientSize = new System.Drawing.Size(362, 481);
this.Controls.Add(this.listBox1);
this.Controls.Add(this._pictureBox);
this.Controls.Add(this.label7);
this.Controls.Add(this.label6);
this.Controls.Add(this._textBoxRect);
this.Controls.Add(this._textBoxStyle);
this.Controls.Add(this.label5);
this.Controls.Add(this.label4);
this.Controls.Add(this.label3);
this.Controls.Add(this._textBoxHandle);
this.Controls.Add(this._textBoxClass);
this.Controls.Add(this.label1);
this.Controls.Add(this._textBoxText);
this.Controls.Add(this.label2);
this.Controls.Add(this._buttonCancel);
this.Controls.Add(this._buttonOK);
this.Controls.Add(this._buttonCapture);
this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
this.Name = "SpyWindow";
this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
this.Text = "Find Window";
((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
this.ResumeLayout(false);
this.PerformLayout();

		}
		#endregion		

		/// <summary>
		/// Processes window messages sent to the Spy Window
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{									
			switch(m.Msg)
			{
				/*
				 * stop capturing events as soon as the user releases the left mouse button
				 * */
				case (int)Win32.WindowMessages.WM_LBUTTONUP:
					this.CaptureMouse(false);
					break;
				/*
				 * handle all the mouse movements
				 * */
				case (int)Win32.WindowMessages.WM_MOUSEMOVE:
					this.HandleMouseMovements();
					break;			
			};

			base.WndProc (ref m);
		}
		
		/// <summary>
		/// Processes the mouse down events for the finder tool 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFinderToolMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				this.CaptureMouse(true);
		}

		/// <summary>
		/// Raises the ImageReadyForDisplay event
		/// </summary>
		/// <param name="image"></param>
		protected virtual void OnImageReadyForDisplay(Image image)
		{
			try
			{
				if (this.ImageReadyForDisplay != null)
					this.ImageReadyForDisplay(image, false, PictureBoxSizeMode.CenterImage);
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
		
		/// <summary>
		/// Returns the caption of a window
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		private string GetWindowText(IntPtr hWnd)
		{			
			StringBuilder sb = new StringBuilder(256);							
			Win32.GetWindowText(hWnd, sb, 256);								
			return sb.ToString();
		}

		/// <summary>
		/// Returns the name of a window's class
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		private string GetClassName(IntPtr hWnd)
		{			
			StringBuilder sb = new StringBuilder(256);							
			Win32.GetClassName(hWnd, sb, 256);								
			return sb.ToString();
		}

		/// <summary>
		/// Captures or releases the mouse
		/// </summary>
		/// <param name="captured"></param>
		private void CaptureMouse(bool captured)
		{
			// if we're supposed to capture the window
			if (captured)
			{
				// capture the mouse movements and send them to ourself
				Win32.SetCapture(this.Handle);

				// set the mouse cursor to our finder cursor
				Cursor.Current = _cursorFinder;

				// change the image to the finder gone image
				_pictureBox.Image = _finderGone;
			}
			// otherwise we're supposed to release the mouse capture
			else
			{
				// so release it
				Win32.ReleaseCapture();

				// put the default cursor back
				Cursor.Current = _cursorDefault;

				// change the image back to the finder at home image
				_pictureBox.Image = _finderHome;

				// and finally refresh any window that we were highlighting
				if (_hPreviousWindow != IntPtr.Zero)
				{
					WindowHighlighter.Refresh(_hPreviousWindow);
					_hPreviousWindow = IntPtr.Zero;
				}
			}

			// save our capturing state
			_capturing = captured;
		}

		/// <summary>
		/// Handles all mouse move messages sent to the Spy Window
		/// </summary>
		private void HandleMouseMovements()
		{
			// if we're not capturing, then bail out
			if (!_capturing)
				return;
											
			try
			{
				// capture the window under the cursor's position
				IntPtr hWnd = Win32.WindowFromPoint(Cursor.Position);

				// if the window we're over, is not the same as the one before, and we had one before, refresh it
				if (_hPreviousWindow != IntPtr.Zero && _hPreviousWindow != hWnd)
					WindowHighlighter.Refresh(_hPreviousWindow);

				// if we didn't find a window.. that's pretty hard to imagine. lol
				if (hWnd == IntPtr.Zero)
				{
					_textBoxHandle.Text = null;
					_textBoxClass.Text = null;
					_textBoxText.Text = null;
					_textBoxStyle.Text = null;
					_textBoxRect.Text = null;
				}
				else
				{
					// save the window we're over
					_hPreviousWindow = hWnd;

					// handle
					_textBoxHandle.Text = string.Format("{0}", hWnd.ToInt32().ToString());

					// class
					_textBoxClass.Text = this.GetClassName(hWnd);

					// caption
					_textBoxText.Text = this.GetWindowText(hWnd);
					
					Win32.Rect rc = new Win32.Rect();
					Win32.GetWindowRect(hWnd, ref rc);
						
					// rect
					_textBoxRect.Text = string.Format("[{0} x {1}], ({2},{3})-({4},{5})", rc.right - rc.left, rc.bottom - rc.top, rc.left, rc.top, rc.right, rc.bottom);

					// highlight the window
					WindowHighlighter.Highlight(hWnd);
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		/// <summary>
		/// Occurs when the OK button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnButtonOKClicked(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// Occurs when the Cancel button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnButtonCancelClicked(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

        /// <summary>
        /// split and zoom
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="count"></param>
        /// <param name="mode">1: horizontalement ou 2:verticalement</param>
        /// <param name="zoom">valeur de zoom</param>
        private Bitmap[] SplitImage(Image bmp, int count, int mode, int zoom)
        {
            Bitmap[] bmpList = new Bitmap[count];
            int width, height;
            Rectangle srcRect, dstRect;
            Bitmap bmpZoom;

            if(mode == 1)
            {
                width = bmp.Width * zoom ;
                height = bmp.Height /count * zoom ;
            }
            else
            {
                width = bmp.Width /count * zoom ;
                height = bmp.Height * zoom ;
            }

            if(mode == 1)
                srcRect = new Rectangle(0, 0, bmp.Width, bmp.Height/count);
            else
                srcRect = new Rectangle(0, 0, bmp.Width/count, bmp.Height);

            dstRect = new Rectangle(0, 0, width, height);
            
            for(int i = 0; i < count; i+=1)
            {   
                if(mode==1)
                    bmpZoom = new Bitmap(bmp.Width * zoom, bmp.Height /count * zoom);
                else
                    bmpZoom = new Bitmap(bmp.Width /count * zoom, bmp.Height * zoom);

                Graphics g = Graphics.FromImage(bmpZoom);
                g.DrawImage(bmp, dstRect, srcRect, GraphicsUnit.Pixel);
                g.Dispose();
                
                bmpList[i] = bmpZoom;
            }

            return bmpList;
        }


        public Bitmap BitmapTo1Bpp(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;

            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format1bppIndexed);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);

            for (int y = 0; y < h; y++)
            {
                byte[] scan = new byte[(w + 7) / 8];

                for (int x = 0; x < w; x++)
                {
                    Color c = img.GetPixel(x, y);
                    if (c.GetBrightness() >= 0.7) 
                        scan[x / 8] |= (byte)(0x80 >> (x % 8));
                }

                Marshal.Copy(scan, 0, (IntPtr)((int)data.Scan0 + data.Stride * y), scan.Length);
            }
            bmp.UnlockBits(data);

            return bmp;
        }

        /// <summary>
        /// Copies a bitmap into a 1bpp/8bpp bitmap of the same dimensions, fast
        /// </summary>
        /// <param name="b">original bitmap</param>
        /// <param name="bpp">1 or 8, target bpp</param>
        /// <returns>a 1bpp copy of the bitmap</returns>
        public Bitmap CopyToBpp(Bitmap b, int bpp)
        {
            if (bpp != 1 && bpp != 8) 
                throw new System.ArgumentException("1 or 8", "bpp");

            // Plan: built into Windows GDI is the ability to convert
            // bitmaps from one format to another. Most of the time, this
            // job is actually done by the graphics hardware accelerator card
            // and so is extremely fast. The rest of the time, the job is done by
            // very fast native code.
            // We will call into this GDI functionality from C#. Our plan:
            // (1) Convert our Bitmap into a GDI hbitmap (ie. copy unmanaged->managed)
            // (2) Create a GDI monochrome hbitmap
            // (3) Use GDI "BitBlt" function to copy from hbitmap into monochrome (as above)
            // (4) Convert the monochrone hbitmap into a Bitmap (ie. copy unmanaged->managed)

            int w = b.Width, h = b.Height;
            IntPtr hbm = b.GetHbitmap(); // this is step (1)

            // Step (2): create the monochrome bitmap.
            // "BITMAPINFO" is an interop-struct which we define below.
            // In GDI terms, it's a BITMAPHEADERINFO followed by an array of two RGBQUADs
            Win32.BITMAPINFO bmi = new Win32.BITMAPINFO();

            bmi.biSize = 40;  // the size of the BITMAPHEADERINFO struct
            bmi.biWidth = w;
            bmi.biHeight = h;
            bmi.biPlanes = 1; // "planes" are confusing. We always use just 1. Read MSDN for more info.
            bmi.biBitCount = (short)bpp; // ie. 1bpp or 8bpp
            bmi.biCompression = Win32.BI_RGB; // ie. the pixels in our RGBQUAD table are stored as RGBs, not palette indexes
            bmi.biSizeImage = (uint)(((w + 7) & 0xFFFFFFF8) * h / 8);
            bmi.biXPelsPerMeter = 1000000; // not really important
            bmi.biYPelsPerMeter = 1000000; // not really important

            // Now for the colour table.
            uint ncols = (uint)1 << bpp; // 2 colours for 1bpp; 256 colours for 8bpp
            bmi.biClrUsed = ncols;
            bmi.biClrImportant = ncols;
            bmi.cols = new uint[256]; // The structure always has fixed size 256, even if we end up using fewer colours
            if (bpp == 1) 
            { 
                bmi.cols[0] = Win32.MAKERGB(0, 0, 0); 
                bmi.cols[1] = Win32.MAKERGB(255, 255, 255); 
            }
            else 
            { 
                for (int i = 0; i < ncols; i++) 
                    bmi.cols[i] = Win32.MAKERGB(i, i, i); 
            }

            // For 8bpp we've created an palette with just greyscale colours.
            // You can set up any palette you want here. Here are some possibilities:
            // greyscale: for (int i=0; i<256; i++) bmi.cols[i]=MAKERGB(i,i,i);
            // rainbow: bmi.biClrUsed=216; bmi.biClrImportant=216; int[] colv=new int[6]{0,51,102,153,204,255};
            //          for (int i=0; i<216; i++) bmi.cols[i]=MAKERGB(colv[i/36],colv[(i/6)%6],colv[i%6]);
            // optimal: a difficult topic: http://en.wikipedia.org/wiki/Color_quantization
            // 
            // Now create the indexed bitmap "hbm0"
            IntPtr bits0; // not used for our purposes. It returns a pointer to the raw bits that make up the bitmap.
            IntPtr hbm0 = Win32.CreateDIBSection(IntPtr.Zero, ref bmi, Win32.DIB_RGB_COLORS, out bits0, IntPtr.Zero, 0);
 
            //
            // Step (3): use GDI's BitBlt function to copy from original hbitmap into monocrhome bitmap
            // GDI programming is kind of confusing... nb. The GDI equivalent of "Graphics" is called a "DC".
            IntPtr sdc = Win32.GetDC(IntPtr.Zero);       // First we obtain the DC for the screen
            
            // Next, create a DC for the original hbitmap
            IntPtr hdc = Win32.CreateCompatibleDC(sdc); Win32.SelectObject(hdc, hbm);
            
            // and create a DC for the monochrome hbitmap
            IntPtr hdc0 = Win32.CreateCompatibleDC(sdc); Win32.SelectObject(hdc0, hbm0);
            
            // Now we can do the BitBlt:
            Win32.BitBlt(hdc0, 0, 0, w, h, hdc, 0, 0, Win32.SRCCOPY);
            
            // Step (4): convert this monochrome hbitmap back into a Bitmap:
            Bitmap b0 = Bitmap.FromHbitmap(hbm0);
            
            // Finally some cleanup.
            Win32.DeleteDC(hdc);
            Win32.DeleteDC(hdc0);
            Win32.ReleaseDC(IntPtr.Zero, sdc);
            Win32.DeleteObject(hbm);
            Win32.DeleteObject(hbm0);
 
            //
            return b0;
        }

        private Bitmap ZoomImage(Image bmp, int zoom)
        {
            Bitmap bmpZoom = new Bitmap(bmp.Width * zoom, bmp.Height * zoom);
            Graphics g = Graphics.FromImage(bmpZoom);
            
            Rectangle srcRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle dstRect = new Rectangle(0, 0, bmpZoom.Width, bmpZoom.Height);

            g.DrawImage(bmp, dstRect, srcRect, GraphicsUnit.Pixel);
            g.Dispose();
            return bmpZoom;
        }

        private void DoOCR(Bitmap image, String lang)
        {
                tessnet2.Tesseract ocr = new tessnet2.Tesseract();

                ocr.Init(lang, false);

                List<tessnet2.Word> result = ocr.DoOCR(image, Rectangle.Empty);

                foreach (tessnet2.Word word in result)
                    listBox1.Items.Add(word.Text);
                // Console.WriteLine("{0} : {1}", word.Confidence, word.Text);
        }

		/// <summary>
		/// Occurs when the user wants to capture the window that we've captured
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnButtonCaptureClicked(object sender, EventArgs e)
		{
            Bitmap[] bmpList;

//			try
//			{
                DateTime dtStart, dtEnd;

				// parse the window handle
				int handle = int.Parse(_textBoxHandle.Text);
				
				// capture that window
				Bitmap image = ScreenCapturing.GetWindowCaptureAsBitmap(handle);
                
                bmpList = this.SplitImage(image, 3, 1, 2);

 
                // image = this.CopyToBpp(bmpList[0], 8);
                // image = this.BitmapTo1Bpp(bmpList[0]);
                image = bmpList[0];

                dtStart = DateTime.Now;
                DoOCR(image, "fra");
                dtEnd = DateTime.Now;

                TimeSpan tsDiff = dtEnd.Subtract(dtStart);
                MessageBox.Show(tsDiff.Milliseconds+" s");
            
				// fire our image read event, which the main window will display for us
                this.OnImageReadyForDisplay(image);
/*          }
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
 */
		}

		/// <summary>
		/// Occurs when the handle textbox changes, to determine if the capture button is enabled
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTextBoxHandleTextChanged(object sender, EventArgs e)
		{
			_buttonCapture.Enabled = (_textBoxHandle.Text != null && _textBoxHandle.Text != string.Empty);
		}

 
	}
}
