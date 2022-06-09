using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pixels.Forms {
	public partial class Canvas : Form {

		// The form's size is way too dense to make out individual pixels on a modern display.
		private readonly int virtualWidth = 128;
		private readonly int virtualHeight = 128;

		// Initialize a random number generator for color generation and a brush for drawing.
		private readonly Random randomGenerator = new();
		private readonly SolidBrush drawingBrush = new( Color.White );

		// Placeholder for the form's graphics drawing surface.
		private Graphics drawingSurface;

		// Placeholders for the most recent color values in the additive color generator.
		private int latestRed = 0;
		private int latestGreen = 0;
		private int latestBlue = 0;

		// Initialize designer stuff in the constructor.
		public Canvas() {
			InitializeComponent();
		}

		// Renders each virtual pixel to the form's drawing surface.
		private void DoRender() {

			// Start by clearing anything that was previously drawn.
			Console.WriteLine( "Clearing the canvas..." );
			drawingSurface.Clear( BackColor );

			// Calculate the required values for the virtual pixels.
			int pixelCount = virtualWidth * virtualHeight;
			int pixelWidth = Size.Width / virtualWidth;
			int pixelHeight = Size.Height / virtualHeight;
			Console.WriteLine( $"The virtual pixel size is {pixelWidth}x{pixelHeight} ({pixelCount} pixels in total)." );

			// Begin rendering each virtual pixel by iterating over the X and Y positions of each one...
			Console.WriteLine( $"Starting the render..." );
			for ( int xPosition = 0; xPosition < Size.Width; xPosition += pixelHeight ) {
				for ( int yPosition = 0; yPosition < Size.Height; yPosition += pixelWidth ) {

					// Change the color of the brush using one of the generator functions
					drawingBrush.Color = GetPositionColor( xPosition, yPosition );
					//drawingBrush.Color = GetRandomColor();

					// Create a rectangle on the drawing surface to act as the virtual pixel.
					drawingSurface.FillRectangle( drawingBrush, xPosition, yPosition, pixelHeight, pixelWidth );
					Console.WriteLine( $"Rendered X {xPosition}, Y {yPosition} as R {drawingBrush.Color.R}, G {drawingBrush.Color.G}, B {drawingBrush.Color.B}." );

				}
			}
			Console.WriteLine( $"Finished the render." );

		}

		// This produces a random distribution of pixels
		private Color GetRandomColor() {
			int red = randomGenerator.Next( byte.MinValue, byte.MaxValue );
			int green = randomGenerator.Next( byte.MinValue, byte.MaxValue );
			int blue = randomGenerator.Next( byte.MinValue, byte.MaxValue );

			return Color.FromArgb( byte.MaxValue, red, green, blue );
		}

		// This produces an additive pattern of pixels
		private Color GetPositionColor( int xPosition, int yPosition ) {
			latestRed = Clamp( latestRed + randomGenerator.Next( -100, 60 ) * ( xPosition / 2 ) / ( yPosition + 1 ), byte.MinValue, byte.MaxValue );
			latestGreen = Clamp( latestGreen + randomGenerator.Next( -100, 50 ) * ( xPosition / 2 ) / ( yPosition + 1 ), byte.MinValue, byte.MaxValue );
			latestBlue = Clamp( latestBlue + randomGenerator.Next( -40, 40 ) * ( xPosition / 2 ) / ( yPosition + 1 ), byte.MinValue, byte.MaxValue );

			return Color.FromArgb( byte.MaxValue, latestRed, latestGreen, latestBlue );
		}

		// The built-in Math.Clamp is unavailable for .NET Framework 4.8.
		// https://docs.microsoft.com/en-us/dotnet/api/system.math.clamp
		private int Clamp( int value, int minimum, int maximum ) {
			return Math.Min( Math.Max( value, minimum ), maximum );
		}

		// Update the control's graphics property when the form has finished loading.
		private void OnLoad( object sender, EventArgs e ) {
			drawingSurface = CreateGraphics();
		}

		// Start rendering the pixels whenever the form has been painted.
		private void OnPaint( object sender, PaintEventArgs e ) {
			DoRender();
		}

	}
}
