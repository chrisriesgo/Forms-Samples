
using Xamarin.Forms;
using DemoForms140.Pages;

namespace DemoForms140
{
	public class App : Application
	{
		public App()
		{
			// The root page of your application
			var page = new TableOfContentsPage();
			var navigationPage = new NavigationPage(page);

			MainPage = navigationPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			this.ModalPopping += App_ModalPopping;
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			this.ModalPopping -= App_ModalPopping;
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
			this.ModalPopping += App_ModalPopping;
		}

		private void App_ModalPopping (object sender, ModalPoppingEventArgs e)
		{
			e.Cancel = true;
		}
	}
}

