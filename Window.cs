using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixels {
	public partial class Window : Form {
		public Window() {
			InitializeComponent();
		}

		const int WIDTH = 256;
		const int HEIGHT = 256;
		const int TOTAL = WIDTH * HEIGHT;

		Random random;
		SolidBrush brush;
		Graphics graphics;
		int xDensity = 1;
		int yDensity = 1;

		int color = 0;

		int red = 0;
		int green = 0;
		int blue = 0;

		private void Window_Load( object sender, EventArgs e ) {
			random = new Random();
			brush = new SolidBrush( Color.White );
			graphics = CreateGraphics();
			xDensity = Width / WIDTH;
			yDensity = Height / HEIGHT;
		}

		private int Clamp( int value, int min, int max ) {
			return ( value < min ) ? min : ( value > max ) ? max : value;
		}

		private int RandomInt( int min, int max ) {
			return (int)(random.NextDouble() * ( max - min ) + min);
		}

		private Color GetColor( int x, int y ) {
			//int red = random.Next( 0, 256 );
			//int green = random.Next( 0, 256 );
			//int blue = random.Next( 0, 256 );

			//color = Clamp( color + RandomInt( -255, 90 ), 0, 255 );

			red = Clamp( red + RandomInt( -100, 60 ) * ( x / 2 ) / ( y + 1 ), 0, 255 );
			green = Clamp( green + RandomInt( -100, 50 ) * ( x / 2 ) / ( y + 1 ), 0, 255 );
			blue = Clamp( blue + RandomInt( -40, 40 ) * ( x / 2 ) / ( y + 1 ), 0, 255 );

			return Color.FromArgb( 255, red, green, blue );
		}

		private void Render() {
			graphics.Clear( BackColor );
			for ( int y = 0; y < Width; y += yDensity ) {
				for ( int x = 0; x < Height; x += xDensity ) {
					brush.Color = GetColor( x, y );
					graphics.FillRectangle( brush, x, y, xDensity, yDensity );
				}
			}
		}

		private void Window_Paint( object sender, PaintEventArgs e ) {
			Render();
		}

		private void Window_KeyPress( object sender, KeyPressEventArgs e ) {
			Render();
		}
	}
}
