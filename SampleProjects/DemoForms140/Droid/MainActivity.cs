using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DemoForms140.Droid
{
	[Activity(Label = "Demo Forms 1.4", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			#region Insights Key
			const string INSIGHTS_KEY = "{insightskey}";
			#endregion

			if (!Xamarin.Insights.IsInitialized)
			{
				Xamarin.Insights.Initialize(INSIGHTS_KEY, ApplicationContext);
			}

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}
	}
}

