using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace TestEventSender.Classes
{
    class ScreenCapture
    {
        [DllImport("avicap32.dll", CharSet=CharSet.Auto)]
        private static extern IntPtr capCreateCaptureWindow( string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hwndParent, int nID);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage( IntPtr hWnd, eAvicapMessage msg, int wParam, int lParam );


        #region enums privés avicap32.dll

        public enum eAvicapMessage : int
        {
            WM_CAP_START = 0x400,
            WM_CAP_SET_CALLBACK_ERROR = (WM_CAP_START + 2),
            WM_CAP_SET_CALLBACK_STATUS = (WM_CAP_START + 3),
            WM_CAP_SET_CALLBACK_YIELD = (WM_CAP_START + 4),
            WM_CAP_SET_CALLBACK_FRAME = (WM_CAP_START + 5),
            WM_CAP_SET_CALLBACK_VIDEOSTREAM = (WM_CAP_START + 6),
            WM_CAP_SET_CALLBACK_WAVESTREAM = (WM_CAP_START + 7),
            WM_CAP_GET_USER_DATA = (WM_CAP_START + 8),
            WM_CAP_SET_USER_DATA = (WM_CAP_START + 9),
            WM_CAP_DRIVER_CONNECT = (WM_CAP_START + 10),
            WM_CAP_DRIVER_DISCONNECT = (WM_CAP_START + 11),
            WM_CAP_DRIVER_GET_NAME = (WM_CAP_START + 12),
            WM_CAP_DRIVER_GET_VERSION = (WM_CAP_START + 13),
            WM_CAP_DRIVER_GET_CAPS = (WM_CAP_START + 14),
            WM_CAP_FILE_SET_CAPTURE_FILE = (WM_CAP_START + 20),
            WM_CAP_FILE_GET_CAPTURE_FILE = (WM_CAP_START + 21),
            WM_CAP_FILE_SAVEAS = (WM_CAP_START + 23),
            WM_CAP_FILE_SAVEDIB = (WM_CAP_START + 25),
            WM_CAP_FILE_ALLOCATE = (WM_CAP_START + 22),
            WM_CAP_FILE_SET_INFOCHUNK = (WM_CAP_START + 24),
            WM_CAP_EDIT_COPY = (WM_CAP_START + 30),
            WM_CAP_SET_AUDIOFORMAT = (WM_CAP_START + 35),
            WM_CAP_GET_AUDIOFORMAT = (WM_CAP_START + 36),
            WM_CAP_DLG_VIDEOFORMAT = (WM_CAP_START + 41),
            WM_CAP_DLG_VIDEOSOURCE = (WM_CAP_START + 42),
            WM_CAP_DLG_VIDEODISPLAY = (WM_CAP_START + 43),
            WM_CAP_GET_VIDEOFORMAT = (WM_CAP_START + 44),
            WM_CAP_SET_VIDEOFORMAT = (WM_CAP_START + 45),
            WM_CAP_DLG_VIDEOCOMPRESSION = (WM_CAP_START + 46),
            WM_CAP_SET_PREVIEW = (WM_CAP_START + 50),
            WM_CAP_SET_OVERLAY = (WM_CAP_START + 51),
            WM_CAP_SET_PREVIEWRATE = (WM_CAP_START + 52),
            WM_CAP_SET_SCALE = (WM_CAP_START + 53),
            WM_CAP_GET_STATUS = (WM_CAP_START + 54),
            WM_CAP_SET_SCROLL = (WM_CAP_START + 55),
            WM_CAP_GRAB_FRAME = (WM_CAP_START + 60),
            WM_CAP_GRAB_FRAME_NOSTOP = (WM_CAP_START + 61),
            WM_CAP_SEQUENCE = (WM_CAP_START + 62),
            WM_CAP_SEQUENCE_NOFILE = (WM_CAP_START + 63),
            WM_CAP_SET_SEQUENCE_SETUP = (WM_CAP_START + 64),
            WM_CAP_GET_SEQUENCE_SETUP = (WM_CAP_START + 65),
            WM_CAP_SET_MCI_DEVICE = (WM_CAP_START + 66),
            WM_CAP_GET_MCI_DEVICE = (WM_CAP_START + 67),
            WM_CAP_STOP = (WM_CAP_START + 68),
            WM_CAP_ABORT = (WM_CAP_START + 69),
            WM_CAP_SINGLE_FRAME_OPEN = (WM_CAP_START + 70),
            WM_CAP_SINGLE_FRAME_CLOSE = (WM_CAP_START + 71),
            WM_CAP_SINGLE_FRAME = (WM_CAP_START + 72),
            WM_CAP_PAL_OPEN = (WM_CAP_START + 80),
            WM_CAP_PAL_SAVE = (WM_CAP_START + 81),
            WM_CAP_PAL_PASTE = (WM_CAP_START + 82),
            WM_CAP_PAL_AUTOCREATE = (WM_CAP_START + 83),
            WM_CAP_PAL_MANUALCREATE = (WM_CAP_START + 84),
            // Following added post VFW 1.1
            WM_CAP_SET_CALLBACK_CAPCONTROL = (WM_CAP_START + 85),
            WM_CAP_END = (WM_CAP_START + 181)
        }
        #endregion

        // Declarations
        [DllImport("coredll.dll")]
        public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        [DllImport("coredll.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        const int SRCCOPY = 0x00CC0020;


        public static void Snapshot(string fileName, Rectangle rectangle)
        {
            //Use a zeropointer to get hold of the screen context
            IntPtr deviceContext = GetDC(IntPtr.Zero);

            using (Bitmap capture = new Bitmap(rectangle.Width, rectangle.Height))
            //Get screen context
            using (Graphics deviceGraphics = Graphics.FromHdc(deviceContext))
            //Get graphics from bitmap
            using (Graphics captureGraphics = Graphics.FromImage(capture))
            {
                // Blit the image data
                BitBlt(captureGraphics.GetHdc(), 0, 0,
                    rectangle.Width, rectangle.Height, deviceGraphics.GetHdc(),
                    rectangle.Left, rectangle.Top, SRCCOPY);

                //Save to disk
                capture.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        public static Bitmap CaptureWindow(IntPtr hwnd)
        {
            bool bRetVal;
             
            //sans interruption Preview
            // bRetVal = (SendMessage( hwnd, eAvicapMessage.WM_CAP_GRAB_FRAME_NOSTOP, 0,0) != 0 );
            
            //avec interruption Preview
            bRetVal = (SendMessage( hwnd, eAvicapMessage.WM_CAP_GRAB_FRAME, 0, 0) != 0 );
            if (!bRetVal)
                throw new Exception("test");

            // Copie vers le presse-papiers :
            bRetVal = (SendMessage( hwnd, eAvicapMessage.WM_CAP_EDIT_COPY, 0, 0) != 0 );
            if (!bRetVal)
                throw new Exception("test2");

            // Recupération du presse-papiers :
            return GetBitmapFrame();
        }
        
        /// <summary>
        /// Retourne la représentation Bitmap d'un objet du presse-papiers.
        /// </summary>
        /// <returns>Image Bitmap (si le format existe ou null).</returns>
        public static Bitmap GetBitmapFrame()
        {
            // (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
            IDataObject iData = Clipboard.GetDataObject();
            if (!iData.GetDataPresent(DataFormats.Bitmap)) 
                return null;

            return (Bitmap)iData.GetData(DataFormats.Bitmap);
        }

        public void SaveImage(Image img, String pathname)
        {
            img.Save(pathname);
        }
    }
}
