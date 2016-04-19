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
		int count = 1;
		ImageView hueRing;
		ImageView svSquare;
		Bitmap svBitmap;
		EditText hueValue;
		Button button;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			hueRing = FindViewById<ImageView>(Resource.Id.imageView1);
			hueRing.Touch += HueRingTouchEvent;

			hueValue = FindViewById<EditText>(Resource.Id.hueNum);

			button = FindViewById<Button>(Resource.Id.button1);
			button.Click += ButtonClickEvent;

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
					hueValue.Text = ((int)Helper.GetAngleInDegrees(hueRing.Width / 2, hueRing.Height / 2 + xy[1], eventArgs.Event.RawX, eventArgs.Event.RawY)).ToString();
					break;

				case MotionEventActions.Up:
					Console.WriteLine("Ends");
					break;

				default:
					Console.WriteLine("Nothing");
					break;
			}
		}

		public void ButtonClickEvent(object sender, EventArgs eventArgs)
		{
			svSquare.SetImageBitmap(Helper.GenerateSVSquareBitmap(Resources, 10));
		}
	}
}

