using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CodeReflection.ScreenCapturingDemo
{
	/// <summary>
	/// Summary description for CaptureWindow.
	/// </summary>
	public class CaptureWindow : System.Windows.Forms.Form
	{		
		private SpyWindow _spyWindow;
		private System.Windows.Forms.StatusBar _statusBar;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem _menuItemDesktop;
		private System.Windows.Forms.MenuItem _menuItemWindow;
		private System.Windows.Forms.MenuItem _menuItemSmallIcon;
		private System.Windows.Forms.MenuItem _menuItemLargeIcon;
		
		private const Keys CaptureActionKeyClear = Keys.C;
		private const Keys CaptureActionKeyDesktop = Keys.D;		
		private const Keys CaptureActionKeyWindow = Keys.W;
		private const Keys CaptureActionKeyWindowSmallIcon = Keys.S;
		private const Keys CaptureActionKeyWindowLargeIcon = Keys.L;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem _menuItemClear;
		private System.Windows.Forms.PictureBox _canvas;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem _menuItemSpyWindows;

		private enum CaptureActions
		{
			None,
			Desktop,
			Window,
			SmallIcon,
			LargeIcon,
			Clear
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CaptureWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//			
			this.KeyPreview = true;
			_menuItemClear.Click += new EventHandler(OnMenuItemClearClicked);
			_menuItemDesktop.Click += new EventHandler(OnMenuItemDesktopClicked);
			_menuItemWindow.Click += new EventHandler(OnMenuItemWindowClicked);
			_menuItemSmallIcon.Click += new EventHandler(OnMenuItemSmallIconClicked);
			_menuItemLargeIcon.Click += new EventHandler(OnMenuItemLargeIconClicked);
			_menuItemSpyWindows.Click += new EventHandler(OnMenuItemSpyWindowsClicked);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
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
			this._statusBar = new System.Windows.Forms.StatusBar();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this._menuItemDesktop = new System.Windows.Forms.MenuItem();
			this._menuItemWindow = new System.Windows.Forms.MenuItem();
			this._menuItemSmallIcon = new System.Windows.Forms.MenuItem();
			this._menuItemLargeIcon = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this._menuItemClear = new System.Windows.Forms.MenuItem();
			this._canvas = new System.Windows.Forms.PictureBox();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this._menuItemSpyWindows = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// _statusBar
			// 
			this._statusBar.Location = new System.Drawing.Point(0, 403);
			this._statusBar.Name = "_statusBar";
			this._statusBar.Size = new System.Drawing.Size(592, 22);
			this._statusBar.TabIndex = 1;
			this._statusBar.Text = "Key Bindings - [D]esktop, [W]indow, Window [S]mall Icon, Window [L]arge Icon, [C]" +
				"lear Image";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._menuItemDesktop,
																					  this._menuItemWindow,
																					  this._menuItemSmallIcon,
																					  this._menuItemLargeIcon,
																					  this.menuItem2,
																					  this._menuItemClear});
			this.menuItem1.Text = "Actions";
			// 
			// _menuItemDesktop
			// 
			this._menuItemDesktop.Index = 0;
			this._menuItemDesktop.Text = "Capture the Desktop";
			// 
			// _menuItemWindow
			// 
			this._menuItemWindow.Index = 1;
			this._menuItemWindow.Text = "Capture this window";
			// 
			// _menuItemSmallIcon
			// 
			this._menuItemSmallIcon.Index = 2;
			this._menuItemSmallIcon.Text = "Capture this window\'s small icon";
			// 
			// _menuItemLargeIcon
			// 
			this._menuItemLargeIcon.Index = 3;
			this._menuItemLargeIcon.Text = "Capture this window\'s large icon";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "-";
			// 
			// _menuItemClear
			// 
			this._menuItemClear.Index = 5;
			this._menuItemClear.Text = "Clear image";
			// 
			// _canvas
			// 
			this._canvas.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
			this._canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this._canvas.Location = new System.Drawing.Point(0, 0);
			this._canvas.Name = "_canvas";
			this._canvas.Size = new System.Drawing.Size(592, 403);
			this._canvas.TabIndex = 2;
			this._canvas.TabStop = false;
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._menuItemSpyWindows});
			this.menuItem3.Text = "Spy";
			// 
			// _menuItemSpyWindows
			// 
			this._menuItemSpyWindows.Index = 0;
			this._menuItemSpyWindows.Text = "Windows...";
			// 
			// CaptureWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 425);
			this.Controls.Add(this._canvas);
			this.Controls.Add(this._statusBar);
			this.Menu = this.mainMenu1;
			this.Name = "CaptureWindow";
			this.Text = "Screen Capturing Demo";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Intercept the key down event, to snag some images
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			CaptureActions action = CaptureActions.None;

			switch(e.KeyCode)
			{
				case CaptureActionKeyDesktop:
					action = CaptureActions.Desktop;
					break;
				case CaptureActionKeyWindow:
					action = CaptureActions.Window;
					break;
				case CaptureActionKeyWindowSmallIcon:
					action = CaptureActions.SmallIcon;
					break;
				case CaptureActionKeyWindowLargeIcon:
					action = CaptureActions.LargeIcon;
					break;	
				case CaptureActionKeyClear:
					action = CaptureActions.Clear;
					break;	
			};
			
			base.OnKeyDown (e);

			if (action != CaptureActions.None)
				this.PerformAction(action);
		}

		/// <summary>
		/// Performs the specified type of capture action and displays the image
		/// </summary>
		/// <param name="action">The action to perform</param>
		private void PerformAction(CaptureActions action)
		{
			Image image = null;

			switch(action)
			{
				case CaptureActions.Desktop:
					image = (Image)ScreenCapturing.GetDesktopWindowCaptureAsBitmap();
					break;
				case CaptureActions.Window:
					image = (Image)ScreenCapturing.GetWindowCaptureAsBitmap((int)this.Handle);
					break;
				case CaptureActions.SmallIcon:
					image = (Image)ScreenCapturing.GetWindowSmallIconAsBitmap((int)this.Handle);
					break;
				case CaptureActions.LargeIcon:
					image = (Image)ScreenCapturing.GetWindowLargeIconAsBitmap((int)this.Handle);
					break;
				default:
					// if it's clear we'll just display a null image
					break;
			};
			
			this.DisplayImage(image, true, PictureBoxSizeMode.Normal /* doesn't matter as audo decide sizing is enabled */);
		}

		/// <summary>
		/// Displays the image
		/// </summary>
		/// <param name="image"></param>
		public void DisplayImage(Image image, bool autoDecideOnSizing, PictureBoxSizeMode manualSizeMode)
		{						
			// snag the canvas' current size
			Rectangle rcCanvas = _canvas.ClientRectangle;

			// if we've got an image
			if (image != null && autoDecideOnSizing)
			{
				// see if we need to stretch it to fit
				if (image.Width > rcCanvas.Width || image.Height > rcCanvas.Height)
					_canvas.SizeMode = PictureBoxSizeMode.StretchImage;
				// of if it's small enough to center
				else if (image.Width <= rcCanvas.Width && image.Height <= rcCanvas.Height)
					_canvas.SizeMode = PictureBoxSizeMode.CenterImage;
			}
			else
				_canvas.SizeMode = manualSizeMode;
			
			// either way, display it
			_canvas.Image = image;
		}

		#region My Menu Item Event Handlers

		private void OnMenuItemClearClicked(object sender, EventArgs e)
		{
			this.PerformAction(CaptureActions.Clear);
		}

		private void OnMenuItemDesktopClicked(object sender, EventArgs e)
		{
			this.PerformAction(CaptureActions.Desktop);
		}

		private void OnMenuItemWindowClicked(object sender, EventArgs e)
		{
			this.PerformAction(CaptureActions.Window);
		}

		private void OnMenuItemSmallIconClicked(object sender, EventArgs e)
		{
			this.PerformAction(CaptureActions.SmallIcon);
		}

		private void OnMenuItemLargeIconClicked(object sender, EventArgs e)
		{
			this.PerformAction(CaptureActions.LargeIcon);
		}

		private void OnMenuItemSpyWindowsClicked(object sender, EventArgs e)
		{
			if (_spyWindow == null)
			{
				_spyWindow = new SpyWindow();
				_spyWindow.ImageReadyForDisplay += new DisplayImageEventHandler(this.DisplayImage);
				_spyWindow.Closed += new EventHandler(OnSpyWindowClosed);
				_spyWindow.Owner = this;
				_spyWindow.Show();
			}
		}	

		#endregion

		private void OnSpyWindowClosed(object sender, EventArgs e)
		{
			_spyWindow = null;
		}
	}

	public delegate void DisplayImageEventHandler(Image image, bool autoDecideOnSizing, PictureBoxSizeMode manualSizeMode);
}
