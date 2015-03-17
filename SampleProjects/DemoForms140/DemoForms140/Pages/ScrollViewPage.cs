using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace DemoForms140.Pages
{
	public class ScrollViewPage : ContentPage
	{
		private readonly ScrollView _scrollView;

		public ScrollViewPage()
		{
			Title = "Tap Item To Scroll Up";
			var stack = new StackLayout { Padding = new Thickness(20) };

			_scrollView = new ScrollView();
			_scrollView.Content = stack;
	
			for(var i = 1; i <= 100; i++)
			{
				var button = new Button {
					Text = string.Format("Item {0}", i),
				};

				button.Clicked += (sender, e) => 
				{
					#region Xamarin Insights
					var clickedButton = sender as Button;
					var table = new Dictionary<string, string>();
					table.Add("Tapped Item", clickedButton.Text);
					Xamarin.Insights.Track("ScrollView Item Tapped", table);
					#endregion

					_scrollView.ScrollToAsync(0, 0, true);
				};

				stack.Children.Add(button);
			}

			Content = _scrollView;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			Xamarin.Insights.Track("ScrollView Page");
		}
	}
}

