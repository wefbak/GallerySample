using DLToolkitControlsSamples.PopUps;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamvvm;
using static DLToolkitControlsSamples.MainPageModel;

namespace DLToolkitControlsSamples
{
    public partial class MainPage : ContentPage, IBasePage<MainPageModel>
	{
        int numPopups = 0;

		public MainPage()
		{
			InitializeComponent();
		}

        async void MyTapRecognizer_Tapped(System.Object sender, DLToolkitControlsSamples.TappedEventArgs e)
        {
            if (!e.LongTap)
            {
                await Navigation.PushAsync(new PhotoPage(e.Item));
            }
            else
            {
                if (numPopups == 0)
                {
                    numPopups++;
                    var popupPage = new OptionsPage();
                    popupPage.Disappearing += PopupPage_Disappearing;
                    await PopupNavigation.Instance.PushAsync(popupPage);
                }
            }
        }

        private void PopupPage_Disappearing(object sender, System.EventArgs e)
        {
            ((BasePopupPage)sender).Disappearing -= PopupPage_Disappearing;
            numPopups--;
        }
    }
}
