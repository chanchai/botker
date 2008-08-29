using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;
using TestEventSender.Classes;

namespace TestEventSender
{

    public partial class Form1 : Form
    {
        internal static IntPtr MakeLParam(int lo, int hi)
        {
            return (IntPtr)(((short)hi << 16) | (lo & 0xffff));
        } 

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;

        [DllImport("user32",SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


        public static void LeftClick(IntPtr windowHandle, Point point)
        {
            SendMessage(windowHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero);
            SendMessage(windowHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero);

             //MakeLParam(point.X, point.Y));

            /*
            Thread.Sleep(100);
            SendMessage(windowHandle, MOUSEEVENTF_LEFTDOWN, (IntPtr)0, (IntPtr)0);
            Thread.Sleep(100);
            SendMessage(windowHandle, MOUSEEVENTF_LEFTUP, (IntPtr)0, (IntPtr)0);
            Thread.Sleep(1000);
            */
        }

        public static void LeftDrag(IntPtr windowHandle, Point from, Point to)
        {
            /*
            SendMessage(windowHandle, MOUSEEVENTF_MOVE, (IntPtr)0, MakeLParam(from.X, from.Y));
            Thread.Sleep(100);
            SendMessage(windowHandle, MOUSEEVENTF_LEFTDOWN, (IntPtr)0, (IntPtr)0);
            Thread.Sleep(100);
            SendMessage(windowHandle, MOUSEEVENTF_MOVE, (IntPtr)0, MakeLParam(from.X, from.Y));
            Thread.Sleep(100);
            SendMessage(windowHandle, MOUSEEVENTF_LEFTUP, (IntPtr)0, (IntPtr)0);
            Thread.Sleep(1000);
            */
        }

        
        public Form1()
        {
            InitializeComponent();
        }

        override protected void OnClick(EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process[] prc = System.Diagnostics.Process.GetProcessesByName("WinEventReceiver");

            Bitmap bmp ;
            Point pos = new Point(50,50);

            if (prc.Count() > 0)
            {
                foreach (System.Diagnostics.Process p in prc)
                {
                    // LeftClick(p.MainWindowHandle, pos);
                    try
                    {
                        bmp = ScreenCapture.CaptureWindow(p.MainWindowHandle);
                        if (bmp != null)
                            bmp.Save("Test.bmp");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Process not found");
            }
            
        }
    }
}
