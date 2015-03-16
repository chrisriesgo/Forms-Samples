using System;
using Xamarin.Forms;

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
				stack.Children.Add(new Button {
					Text = string.Format("Item {0}", i),
					Command = new Command(() => _scrollView.ScrollToAsync(0, 0, true))
				});
			}

			Content = _scrollView;
		}
	}
}

