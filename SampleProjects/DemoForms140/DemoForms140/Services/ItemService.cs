using System;
using System.Collections.Generic;
using DemoForms140.ViewModels;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DemoForms140.Services
{
	public class ItemService
	{
		public ItemService() { }

		public async Task<int> GetCountAsync()
		{
			return await Task.FromResult(100);
		}

		public async Task<IList<ListItemViewModel>> GetPageAsync(int startingItem = 1, int pageSize = 10)
		{
			var list = new List<ListItemViewModel>();
			for(var i = startingItem; i <= startingItem + pageSize - 1; i++)
			{
				list.Add(new ListItemViewModel { Id = i, Text = string.Format("Item {0}", i) });
			}

			return await Task.FromResult(list);
		}

		public async Task<IList<ListItemViewModel>> GetAllAsync()
		{
			var count = await GetCountAsync();
			var list = new List<ListItemViewModel>();
			for(var i = 1; i <= count; i++)
			{
				list.Add(new ListItemViewModel { Id = i, Text = string.Format("Item {0}", i) });
			}

			return await Task.FromResult(list);
		}
	}
}

