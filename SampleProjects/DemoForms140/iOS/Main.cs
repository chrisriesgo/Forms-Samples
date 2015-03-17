using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace DemoForms140.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			#region Insights Key
			const string INSIGHTS_KEY = "{insightskey}";
			#endregion

			if (!Xamarin.Insights.IsInitialized)
			{
				Xamarin.Insights.Initialize(INSIGHTS_KEY);
			}

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}

