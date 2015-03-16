using DemoForms140.ViewModels;
using Xamarin.Forms;

namespace DemoForms140.Pages
{
	public class ListViewPage : ContentPage
	{
		private readonly ListViewPageViewModel _viewModel;
		private readonly ListView _listView;

		public ListViewPage(ListViewPageViewModel viewModel)
		{
			// Set binding context for MVVM-goodness
			_viewModel = viewModel;
			this.BindingContext = _viewModel;

			this.SetBinding<ListViewPageViewModel>(Page.BackgroundColorProperty, vm => vm.PageBackgroundColor);
			this.SetBinding<ListViewPageViewModel>(Page.TitleProperty, vm => vm.PageTitle);

			var listView = new ListView() { BindingContext = _viewModel };
			listView.SetBinding<ListViewPageViewModel>(ListView.IsPullToRefreshEnabledProperty, vm => vm.PullToRefreshEnabled);
			listView.SetBinding<ListViewPageViewModel>(ListView.IsRefreshingProperty, vm => vm.IsRefreshing);
			listView.SetBinding<ListViewPageViewModel>(ListView.SeparatorVisibilityProperty, vm => vm.SeparatorVisibility);
			listView.SetBinding<ListViewPageViewModel>(ListView.SeparatorColorProperty, vm => vm.SeparatorColor);
			listView.SetBinding<ListViewPageViewModel>(ListView.RefreshCommandProperty, vm => vm.Refresh);
			listView.SetBinding<ListViewPageViewModel>(ListView.ItemsSourceProperty, vm => vm.Source, BindingMode.TwoWay);
			listView.SetBinding<ListViewPageViewModel>(ListView.ItemTemplateProperty, vm => vm.ItemTemplate);
			listView.SetBinding<ListViewPageViewModel>(ListView.RowHeightProperty, vm => vm.RowHeight);
			listView.SetBinding<ListViewPageViewModel>(ListView.HeaderProperty, vm => vm.Header);
			listView.SetBinding<ListViewPageViewModel>(ListView.FooterProperty, vm => vm.Footer);

			_listView = listView;
			Content = _listView;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_listView.ItemAppearing += _viewModel.ItemAppearing;
			_viewModel.LoadItemsAsync();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			_listView.ItemAppearing -= _viewModel.ItemAppearing;
		}
	}
}

