using System;
using Xamarin.Forms;
using DemoForms140.Helpers;
using DemoForms140.ViewModels;
using DemoForms140.Converters;
using Xamarin;
using System.Collections.Generic;

namespace DemoForms140.Pages
{
	public class TableOfContentsPage : ContentPage
	{
		private ListViewPageViewModel _footerViewModel, _headerViewModel;

		public TableOfContentsPage()
		{
			Title = "Table of Contents";

			var layout = new StackLayout {
				VerticalOptions = LayoutOptions.Center
			};

			var scrollViewButton = new Button { 
				Text = "ScrollView",
				Command = new Command(() => this.OpenPage(new ScrollViewPage()))
			};

			_headerViewModel = MakeListViewHeaderViewModel();
			var listViewHeaderButton = new Button { 
				Text = "ListView Header",
				Command = new Command(() => this.OpenPage(new ListViewPage(_headerViewModel)))
			};

			_footerViewModel = MakeListViewFooterViewModel();
			var listViewFooterButton = new Button { 
				Text = "ListView Footer",
				Command = new Command(() => this.OpenPage(new ListViewPage(_footerViewModel)))
			};

			var modalButton = new Button { 
				Text = "Cancel Modal Pop Page",
				Command = new Command(() => this.OpenModalPage(new NavigationPage(new ListViewPage(_footerViewModel))))
			};

			var crashButton = new Button { 
				Text = "Force a Crash",
				Command = new Command(() => 
				{
					try 
					{
						throw new Exception("You broke it!");
					} 
					catch (Exception ex) {
						Insights.Report(ex, new Dictionary<string,string>
						{
							{ "Where", "Table of Contents" },
							{ "Issue", "Your guess is as good as mine." }
						});
					}
				})
			};

			layout.Children.Add(scrollViewButton);
			layout.Children.Add(listViewHeaderButton);
			layout.Children.Add(listViewFooterButton);
			layout.Children.Add(modalButton);
			layout.Children.Add(crashButton);

			Content = layout;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			Xamarin.Insights.Track("Table of Contents Page");
		}

		private ListViewPageViewModel MakeListViewHeaderViewModel()
		{
			var template = new DataTemplate(typeof(TextCell));	
			template.SetBinding<ListItemViewModel>(TextCell.TextProperty, bc => bc.Text);
			template.SetBinding<ListItemViewModel>(TextCell.TextColorProperty, bc => bc.TextColor);

			var headerChild = new Label { XAlign = TextAlignment.Center };
			headerChild.SetBinding<ListViewPageViewModel>(Label.TextProperty, bc => bc.PageTitle);
			headerChild.SetBinding<ListViewPageViewModel>(Label.TextColorProperty, bc => bc.PageBackgroundColor);

			var header = new StackLayout { Padding = new Thickness(0, 15), HorizontalOptions = LayoutOptions.FillAndExpand, Children = { headerChild } };
			header.SetBinding<ListViewPageViewModel>(StackLayout.BackgroundColorProperty, bc => bc.SeparatorColor);

			var viewModel = new ListViewPageViewModel
			{
				PageTitle = "ListView Header",
				PageBackgroundColor = Color.White,
				PullToRefreshEnabled = true,
				SeparatorVisibility = SeparatorVisibility.Default,
				SeparatorColor = Color.Blue,
				RowHeight = 65,
				ItemTemplate = template,
				Header = header,
				Refresh = new Command(async () => await _headerViewModel.RefreshItemsAsync()),
			};

			return viewModel;
		}

		private ListViewPageViewModel MakeListViewFooterViewModel()
		{
			var template = new DataTemplate(typeof(TextCell));	
			template.SetBinding<ListItemViewModel>(TextCell.TextProperty, bc => bc.Text);
			template.SetBinding<ListItemViewModel>(TextCell.TextColorProperty, bc => bc.TextColor);

			var footer = new ActivityIndicator { HorizontalOptions = LayoutOptions.Center };
			footer.SetBinding<ListViewPageViewModel>(ActivityIndicator.IsRunningProperty, bc => bc.LoadingMore);
			footer.SetBinding<ListViewPageViewModel>(ActivityIndicator.IsVisibleProperty, bc => bc.LoadingMore);
			footer.SetBinding<ListViewPageViewModel>(ActivityIndicator.HeightRequestProperty, bc => bc.LoadingMore, BindingMode.Default, new BoolToHeightConverter(50));

			var viewModel = new ListViewPageViewModel
			{
				PageTitle = "Scroll To Page",
				PageBackgroundColor = Color.White,
				PullToRefreshEnabled = true,
				SeparatorVisibility = SeparatorVisibility.Default,
				SeparatorColor = Color.Silver,
				RowHeight = 65,
				ItemTemplate = template,
				Footer = footer,
				Refresh = new Command(async () => await _footerViewModel.RefreshItemsAsync()),
			};

			return viewModel;
		}
	}
}

