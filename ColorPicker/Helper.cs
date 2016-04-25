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

		/// <summary>
		/// Generate a XY gradient bitmap for a given hue value.
		/// </summary>
		/// <param name="res">Resource object</param>
		/// <param name="hue">hue value between 0.0 and 360.0</param>
		/// <returns>Bitmap object</returns>
		public static Bitmap GenerateSVSquareBitmap(Resources res, double hue)
		{
			Bitmap svBitmap = BitmapFactory.DecodeResource(res, Resource.Drawable.sv_square);
			Bitmap mutableBitmap = svBitmap.Copy(Bitmap.Config.Argb8888, true);

			for (int i = 0; i < mutableBitmap.Width; i++)
			{
				for (int n = 0; n < mutableBitmap.Height; n++)
				{
					float saturation = (float)i / mutableBitmap.Width;
					float value = (float)n / mutableBitmap.Height;
					mutableBitmap.SetPixel(i, n, Color.HSVToColor(new []{(float)hue, saturation, value}));
				}
			}
			return mutableBitmap;
		}

		/// <summary>
		/// Clamps a value between min and max
		/// </summary>
		/// <param name="value">Value to be clamped</param>
		/// <param name="min">minimum value of "value"</param>
		/// <param name="max">maximum value of "value"</param>
		/// <returns>Clamped value</returns>
		public static double Clamp(double value, double min, double max)
		{
			if (value < min) return min;
			if (value > max) return max;
			return value;
		}
	}
}