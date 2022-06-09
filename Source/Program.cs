using System;
using System.Windows.Forms;

namespace Pixels {
	public static class Program {

		// The main entry point of the application
		[STAThread]
		public static void Main() {

			// Setup for drawing Windows forms
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );

			// Create the main form
			Application.Run( new Forms.Canvas() );

		}

	}
}
