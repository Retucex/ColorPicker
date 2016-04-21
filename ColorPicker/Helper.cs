using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Content.Res;

namespace ColorPicker
{
	using System.Diagnostics;

	static class Helper
	{
		/// <summary>
		/// Returns angle in degrees between x-axis starting at origin and x,y point 
		/// </summary>
		/// <param name="originX">x-axis coordinate of origin</param>
		/// <param name="originY">y-axis coordinate of origin</param>
		/// <param name="x">x-axis coordinate of point</param>
		/// <param name="y">y-axis coordinate of point</param>
		/// <returns>Angle in degrees between 0 and 359</returns>
		public static double GetAngleInDegrees(double originX, double originY, double x, double y)
		{
			double deltaX = x - originX;
			double deltaY = y - originY;
			double angle = 0;

			if (deltaX > 0)
			{
				if (deltaY > 0)
				{
					angle = Math.Atan(deltaY / deltaX) * (180 / Math.PI);
				}
				else
				{
					angle = Math.Atan(deltaX / deltaY) * (180 / Math.PI);
					angle *= -1;
					angle += 270;
				}
			}
			else
			{
				if (deltaY > 0)
				{
					angle = Math.Atan(deltaX / deltaY) * (180 / Math.PI);
					angle *= -1;
					angle += 90;
				}
				else
				{
					angle = Math.Atan(deltaY / deltaX) * (180 / Math.PI);
					angle += 180;
				}
			}

			return angle;
		}

		public static Bitmap GenerateSVSquareBitmap(Resources res, double hue)
		{
			//Stopwatch sw = new Stopwatch();
			//sw.Start();
			Bitmap svBitmap = BitmapFactory.DecodeResource(res, Resource.Drawable.sv_square);
			Bitmap mutableBitmap = svBitmap.Copy(Bitmap.Config.Argb8888, true);
			//Console.WriteLine($"decode: {sw.Elapsed}");
			

			for (int i = 0; i < mutableBitmap.Width; i++)
			{
				for (int n = 0; n < mutableBitmap.Height; n++)
				{
					int r, g, b;
					HsvToRgb2(hue, (double)i / mutableBitmap.Width, (double)n / mutableBitmap.Height, out r, out g, out b);
					
					//int[] rgb = HSVToRGB(hue, (double)i / mutableBitmap.Width, (double) n / mutableBitmap.Height);
					//mutableBitmap.SetPixel(i, n, new Color(Clamp(rgb[0], 0, 255), Clamp(rgb[1], 0, 255), Clamp(rgb[2], 0, 255)));
					mutableBitmap.SetPixel(i, n, new Color(r,g,b));
				}
			}
			//Console.WriteLine($"pixel: {sw.Elapsed}");
			return mutableBitmap;
		}

		static int[] HSVToRGB(double h, double s, double v)
		{
			int[] rgb = new int[3];
			int i;
			double f, p, t, q;
			if (s == 0)
			{
				return new int[] {(int)v, (int)v, (int)v};
			}

			h /= 60;
			i = (int)Math.Floor(h);
			f = h - i;
			p = v * (1 - s);
			q = v * (1 - s * f);
			t = v * (1 - s * (1 - f));

			switch (i)
			{
				case 0:
					rgb[0] = (int)v;
					rgb[1] = (int)t;
					rgb[2] = (int)p;
					break;
				case 1:
					rgb[0] = (int)q;
					rgb[1] = (int)v;
					rgb[2] = (int)p;
					break;
				case 2:
					rgb[0] = (int)p;
					rgb[1] = (int)v;
					rgb[2] = (int)t;
					break;
				case 3:
					rgb[0] = (int)p;
					rgb[1] = (int)q;
					rgb[2] = (int)v;
					break;
				case 4:
					rgb[0] = (int)t;
					rgb[1] = (int)p;
					rgb[2] = (int)v;
					break;
				default:
					rgb[0] = (int)v;
					rgb[1] = (int)p;
					rgb[2] = (int)q;
					break;
			}
			return rgb;
		}

		static void HsvToRgb2(double h, double S, double V, out int r, out int g, out int b)
		{
			// ######################################################################
			// T. Nathan Mundhenk
			// mundhenk@usc.edu
			// C/C++ Macro HSV to RGB

			double H = h;
			while (H < 0) { H += 360; };
			while (H >= 360) { H -= 360; };
			double R, G, B;
			if (V <= 0)
			{ R = G = B = 0; }
			else if (S <= 0)
			{
				R = G = B = V;
			}
			else
			{
				double hf = H / 60.0;
				int i = (int)Math.Floor(hf);
				double f = hf - i;
				double pv = V * (1 - S);
				double qv = V * (1 - S * f);
				double tv = V * (1 - S * (1 - f));
				switch (i)
				{

					// Red is the dominant color

					case 0:
						R = V;
						G = tv;
						B = pv;
						break;

					// Green is the dominant color

					case 1:
						R = qv;
						G = V;
						B = pv;
						break;
					case 2:
						R = pv;
						G = V;
						B = tv;
						break;

					// Blue is the dominant color

					case 3:
						R = pv;
						G = qv;
						B = V;
						break;
					case 4:
						R = tv;
						G = pv;
						B = V;
						break;

					// Red is the dominant color

					case 5:
						R = V;
						G = pv;
						B = qv;
						break;

					// Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

					case 6:
						R = V;
						G = tv;
						B = pv;
						break;
					case -1:
						R = V;
						G = pv;
						B = qv;
						break;

					// The color is not defined, we should throw an error.

					default:
						//LFATAL("i Value error in Pixel conversion, Value is %d", i);
						R = G = B = V; // Just pretend its black/white
						break;
				}
			}
			r = Clamp((int)(R * 255.0));
			g = Clamp((int)(G * 255.0));
			b = Clamp((int)(B * 255.0));
		}

		static int Clamp(double value)
		{
			if (value < 0.0)
			{
				return 0;
			}
			if (value > 255.0)
			{
				return 255;
			}

			return (int)value;
		}
	}
}