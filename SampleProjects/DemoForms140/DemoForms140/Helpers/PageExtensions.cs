using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq.Expressions;

namespace DemoForms140.Helpers
{
	public static class PageExtensions
	{
		public static void OpenPage(this Page page, Page newPage)
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				await page.Navigation.PushAsync(newPage);
			});
		}

		public static void OpenModalPage(this Page page, Page newPage)
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				await page.Navigation.PushModalAsync(newPage);
			});
		}

		public static void SetBinding<TSource>(this DataTemplate self, BindableProperty targetProperty, Expression<Func<TSource, object>> sourceProperty, BindingMode mode = BindingMode.Default, IValueConverter converter = null, string stringFormat = null)
		{
			if(self == null)
			{
				throw new ArgumentNullException("self");
			}
			if(targetProperty == null)
			{
				throw new ArgumentNullException("targetProperty");
			}
			if(sourceProperty == null)
			{
				throw new ArgumentNullException("sourceProperty");
			}

			Binding binding = Binding.Create<TSource>(sourceProperty, mode, converter, null, stringFormat);

			self.SetBinding(targetProperty, binding);
		}
	}
}

