using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ColorPicker
{
	[Activity(Label = "ColorPicker", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
		float centerX = 550;
		float centerY = 1080;
		ImageView hueRing;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			hueRing = FindViewById<ImageView>(Resource.Id.imageView1);
			hueRing.Touch += HueRingTouchEvent;
			Console.WriteLine(hueRing.GetX());
			Console.WriteLine(hueRing.GetY());
		}

		public void HueRingTouchEvent(object sender, View.TouchEventArgs eventArgs)
		{
			switch (eventArgs.Event.Action)
			{
				case MotionEventActions.Down:
					Console.WriteLine("Begins");
					break;

				case MotionEventActions.Move:
					hueRing.Rotation = (float)Helper.GetAngleInDegrees(centerX, centerY, eventArgs.Event.RawX, eventArgs.Event.RawY);
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

