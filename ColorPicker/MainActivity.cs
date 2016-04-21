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
		double hue = 0;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			hueRing = FindViewById<ImageView>(Resource.Id.imageView1);
			hueRing.Touch += HueRingTouchEvent;

			svSquare = FindViewById<ImageView>(Resource.Id.imageView2);
		}

		public void HueRingTouchEvent(object sender, View.TouchEventArgs eventArgs)
		{
			switch (eventArgs.Event.Action)
			{
				case MotionEventActions.Down:
					Console.WriteLine("Begins");
					break;

				case MotionEventActions.Move:
					int[] xy = new int[2];
					hueRing.GetLocationOnScreen(xy);
					hue = Helper.GetAngleInDegrees(
						hueRing.Width / 2,
						hueRing.Height / 2 + xy[1],
						eventArgs.Event.RawX,
						eventArgs.Event.RawY);
					Console.WriteLine($"Hue: {hue}");
					svSquare.SetImageBitmap(Helper.GenerateSVSquareBitmap(Resources, hue));
					break;

				case MotionEventActions.Up:
					Console.WriteLine("Ends");
					break;

				default:
					Console.WriteLine("Nothing");
					break;
			}
		}
	}
}

