using Rg.Plugins.Popup.Services;

namespace DLToolkitControlsSamples.PopUps
{
    public partial class OptionsPage : BasePopupPage
    {
        public OptionsPage() : base()
        {
            InitializeComponent();
        }

        async void Handle_CloseClicked(object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}
