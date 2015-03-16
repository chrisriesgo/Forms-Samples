using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using DemoForms140.Services;

namespace DemoForms140.ViewModels
{
	public class ListViewPageViewModel : INotifyPropertyChanged
	{
		private readonly ItemService _itemService;

		public ListViewPageViewModel() 
		{
			_itemService = new ItemService();
			Source = new ObservableCollection<ListItemViewModel>();
			ItemAppearing = Appearing;
		}

		public EventHandler<ItemVisibilityEventArgs> ItemAppearing { get; private set; }

		#region Bindable Properties
		private string _pageTitle;
		public string PageTitle 
		{ 
			get { return _pageTitle; } 
			set { SetField(ref _pageTitle, value); } 
		}

		private Color _pageBackgroundColor;
		public Color PageBackgroundColor 
		{ 
			get { return _pageBackgroundColor; } 
			set { SetField(ref _pageBackgroundColor, value); } 
		}

		private bool _pullToRefreshEnabled;
		public bool PullToRefreshEnabled 
		{ 
			get { return _pullToRefreshEnabled && !LoadingMore; } 
			set { SetField(ref _pullToRefreshEnabled, value); } 
		}

		private bool _isRefreshing;
		public bool IsRefreshing 
		{ 
			get { return _isRefreshing; } 
			set { SetField(ref _isRefreshing, value); } 
		}

		private bool _loadingMore;
		public bool LoadingMore 
		{ 
			get { return _loadingMore; } 
			set 
			{ 
				SetField(ref _loadingMore, value);
				OnPropertyChanged("PullToRefreshEnabled");
			} 
		}

		private SeparatorVisibility _separatorVisibility;
		public SeparatorVisibility SeparatorVisibility 
		{ 
			get { return _separatorVisibility; } 
			set { SetField(ref _separatorVisibility, value); } 
		}

		private Color _separatorColor;
		public Color SeparatorColor 
		{ 
			get { return _separatorColor; } 
			set { SetField(ref _separatorColor, value); } 
		}

		private ICommand _refresh;
		public ICommand Refresh
		{
			get { return _refresh; }
			set { SetField(ref _refresh, value); }
		}

		private ObservableCollection<ListItemViewModel> _source;
		public ObservableCollection<ListItemViewModel> Source
		{
			get { return _source; }
			set { SetField(ref _source, value); }
		}

		private double _rowHeight;
		public double RowHeight
		{
			get { return _rowHeight; }
			set { SetField(ref _rowHeight, value); }
		}

		private DataTemplate _itemTemplate;
		public DataTemplate ItemTemplate
		{
			get { return _itemTemplate; }
			set { SetField(ref _itemTemplate, value); }
		}

		private DataTemplate _footerTemplate;
		public DataTemplate FooterTemplate
		{
			get { return _footerTemplate; }
			set { SetField(ref _footerTemplate, value); }
		}

		private View _header;
		public View Header
		{
			get { return _header; }
			set { SetField(ref _header, value); }
		}

		private View _footer;
		public View Footer
		{
			get { return _footer; }
			set { SetField(ref _footer, value); }
		}
		#endregion

		public async void LoadItemsAsync()
		{
			LoadingMore = true;
			await Task.Delay(TimeSpan.FromMilliseconds(2000));

			await GetNextPageOfItems();

			await Task.Delay(TimeSpan.FromMilliseconds(750));
			LoadingMore = false;
		}

		public async Task RefreshItemsAsync()
		{
			Source.Clear();
			await Task.Delay(TimeSpan.FromMilliseconds(2000));
			await GetNextPageOfItems();
			IsRefreshing = false;
		}

		private async void Appearing(object sender, ItemVisibilityEventArgs e)
		{
			var count = await _itemService.GetCountAsync();
			var sourceCount = Source.Count;
			if(count == 0 || sourceCount == count) return;

			var item = e.Item as ListItemViewModel;
			if(item == null) return;

			if(item.Id == sourceCount)
			{
				LoadingMore = true;
				await Task.Delay(TimeSpan.FromMilliseconds(2000));
				await GetNextPageOfItems();
				LoadingMore = false;
			}
		}

		private async Task GetNextPageOfItems()
		{
			var count = await _itemService.GetCountAsync();

			IList<ListItemViewModel> items = new List<ListItemViewModel>();
			if(Source.Count < count)
			{
				items = await _itemService.GetPageAsync(Source.Count + 1, pageSize: 20);
				foreach(var item in items)
				{
					Source.Add(item);
				}
			}
		}

		#region Wire-up the Observable parts
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
		protected bool SetField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, newValue))
			{
				return false;
			}

			field = newValue;

			OnPropertyChanged(propertyName);

			return true;
		}
		#endregion
	}
}

