using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace DLToolkitControlsSamples.PopUps
{
    public class BasePopupPage : PopupPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.Padding = new Thickness(0d, 0d, 0d, Utils.SafeAreaInsets.Bottom * -1d);
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}

