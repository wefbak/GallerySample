using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DLToolkitControlsSamples
{
    public partial class App : Application
	{
		public App()
		{
			FlowListView.Init();

			MainPage = new AppShell();
		}

		protected override async void OnStart()
		{
			await Shell.Current.GoToAsync("//main");
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
