using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ColorPicker
{
	using Android.Graphics;

	[Activity(Label = "ColorPicker", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		ImageView hueRing;
		ImageView svSquare;
		ImageView finalColor;

		double hue = 0;
		double saturation = 0.5;
		double value = 0.5;
		bool insideSVSquare = false;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			hueRing = FindViewById<ImageView>(Resource.Id.imageView1);
			hueRing.Touch += HueRingTouchEvent;

			svSquare = FindViewById<ImageView>(Resource.Id.imageView2);
			svSquare.SetImageBitmap(Helper.GenerateSVSquareBitmap(Resources, 0));

			finalColor = FindViewById<ImageView>(Resource.Id.imageView3);
			finalColor.SetColorFilter(Color.HSVToColor(new[] { (float)hue, (float)saturation, (float)value }), PorterDuff.Mode.SrcAtop);
		}

		public void HueRingTouchEvent(object sender, View.TouchEventArgs eventArgs)
		{
			switch (eventArgs.Event.Action)
			{
				case MotionEventActions.Down:
					// Check if Touch is inside the Saturation/Value Square
					if (eventArgs.Event.RawX >= 240 && eventArgs.Event.RawX <= 840 && eventArgs.Event.RawY >= 660
						&& eventArgs.Event.RawY <= 1260)
					{
						insideSVSquare = true;
					}
					break;

				case MotionEventActions.Move:
					if (insideSVSquare)
					{
						// Bind saturation and value between 0 and 1
						saturation = Helper.Clamp((eventArgs.Event.RawX - 240) / 600, 0, 1);
						value = Helper.Clamp((eventArgs.Event.RawY - 660) / 600, 0, 1);
					}
					else //Touch on hue ring
					{
						int[] xy = new int[2];
						hueRing.GetLocationOnScreen(xy);
						hue = Helper.GetAngleInDegrees(
							hueRing.Width / 2,
							hueRing.Height / 2 + xy[1],
							eventArgs.Event.RawX,
							eventArgs.Event.RawY);
						svSquare.SetImageBitmap(Helper.GenerateSVSquareBitmap(Resources, hue));
					}
					finalColor.SetColorFilter(Color.HSVToColor(new []{(float)hue, (float)saturation, (float)value }), PorterDuff.Mode.SrcAtop);
					break;

				case MotionEventActions.Up:
					insideSVSquare = false;
					break;
			}
		}
	}
}

