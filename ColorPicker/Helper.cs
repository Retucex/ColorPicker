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

namespace ColorPicker
{
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
	}
}