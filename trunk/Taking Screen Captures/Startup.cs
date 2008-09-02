using System;
using System.Windows.Forms;

namespace CodeReflection.ScreenCapturingDemo
{
	/// <summary>
	/// Summary description for Startup.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new CaptureWindow());
		}
	}
}
